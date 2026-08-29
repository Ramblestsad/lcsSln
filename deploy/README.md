# Local microservices deployment

These manifests expect Kubernetes Services named `postgres`, `redis`, and `rabbitmq` in the
same namespace. All applications use one PostgreSQL database named `axes`.

```sh
docker build -f src/services/api/Todo.WebApi/Dockerfile -t todo-api:dev .
docker build -f src/services/orders/Todo.Orders.Api/Dockerfile -t todo-orders-api:dev .
docker build -f src/services/orders/Todo.Order.Worker/Dockerfile -t todo-orders-worker:dev .
docker build -f src/services/inventory/Todo.Inventory.Worker/Dockerfile -t todo-inventory-worker:dev .

cp deploy/secret.example.yaml deploy/secret.local.yaml
kubectl apply -f deploy/secret.local.yaml
kubectl apply -k deploy
```

The Ingress routes `/api/orders` to `todo-orders`. It routes all other HTTP paths to
`todo-api`. Order and Inventory communicate through RabbitMQ, not direct HTTP calls.

Each runtime unit can be applied and scaled independently:

```sh
kubectl apply -k deploy/api
kubectl apply -k deploy/orders
kubectl apply -k deploy/inventory

kubectl scale deployment/todo-api --replicas=2
kubectl scale deployment/todo-orders-api --replicas=3
kubectl scale deployment/todo-orders-worker --replicas=2
kubectl scale deployment/todo-inventory-worker --replicas=2
```

Map `todo.local` to `127.0.0.1`, then call `http://todo.local/api/orders` or the other
API routes under `http://todo.local`.

## Database migrations

All services use the same physical PostgreSQL database, `axes`, but each service owns its
tables through a separate `DbContext` and migration history table:

| Owner | DbContext | Migration history table |
| --- | --- | --- |
| Web API | `ApplicationIdentityDbContext` | `__EFMigrationsHistory_webapi` |
| Orders API and worker | `OrderDbContext` | `__EFMigrationsHistory_orders` |
| Inventory worker | `InventoryDbContext` | `__EFMigrationsHistory_inventory` |

The current migration files establish a new baseline. For the first run after this
microservice split, delete and recreate the local `axes` database. Do not run the new
initial migrations over a database created by the old combined migration history.

After this one-time reset, create only the migration owned by the changed service:

```sh
# Identity or Todo schema
dotnet ef migrations add <MigrationName> \
  --project src/services/api/Todo.DAL \
  --startup-project src/services/api/Todo.WebApi \
  --context ApplicationIdentityDbContext

# Order schema
dotnet ef migrations add <MigrationName> \
  --project src/services/orders/Todo.Orders \
  --startup-project src/services/orders/Todo.Orders.Api \
  --context OrderDbContext

# Inventory schema
dotnet ef migrations add <MigrationName> \
  --project src/services/inventory/Todo.Inventory.Worker \
  --startup-project src/services/inventory/Todo.Inventory.Worker \
  --context InventoryDbContext
```

Commit the generated migration and rebuild the affected image. In this local learning
setup, each service applies its own pending migrations during startup. The Orders API and
Orders worker share `OrderDbContext`, so either can apply an Orders migration.

For a production-style deployment, run each required migration once in a migration Job
before starting the new application version. Do not let every replica perform deployment
migrations.
