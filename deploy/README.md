# Local microservices deployment

## Environment layout

`deploy/base` is the environment-neutral base. Its worker manifests use the Kubernetes
Service DNS name `rabbitmq`. Database and Redis addresses come from each environment's
Secret. A staging or production overlay should patch image tags, Ingress hosts, resource
limits, replicas, and environment-specific settings.

`deploy/overlays/local` is the OrbStack-only overlay. It changes external dependency
addresses to `host.docker.internal`. Never put that address in the base Deployment files.
The local Secret template stays at `deploy/secret.local.example.yaml` because it is
applied separately and is not a Kustomize resource.

The local patch files stay directly under `deploy/overlays/local` while there are only a
few of them. Their file names identify the affected service, and `kustomization.yaml`
lists every patch explicitly. Add service subdirectories only when one environment has
enough patches that the flat directory becomes difficult to scan.

## Local deployment

Start OrbStack Kubernetes, then install Traefik once. The application Ingress uses the
`traefik` ingress class.

```sh
helm repo add traefik https://traefik.github.io/charts
helm repo update traefik
helm upgrade --install traefik traefik/traefik \
  --namespace traefik \
  --create-namespace
```

Confirm that the controller and ingress class are ready:

```sh
kubectl rollout status deployment/traefik --namespace traefik
kubectl get ingressclass traefik
```

Run PostgreSQL, Redis, and RabbitMQ as local Docker containers before deploying the
application. They do not need Kubernetes Deployments or Services. The Kubernetes pods
connect to their published host ports through `host.docker.internal`:

```text
PostgreSQL  host.docker.internal:5432
Redis       host.docker.internal:6379
RabbitMQ    host.docker.internal:5672
```

All applications use the same PostgreSQL database named `axes`. Update the passwords in
`deploy/secret.local.yaml` before applying it. If a container publishes a
different host port, update the matching local Secret or overlay patch.

Then build the application images and deploy them:

```sh
docker build -f src/services/api/Todo.WebApi/Dockerfile -t todo-api:dev .
docker build -f src/services/orders/Todo.Orders.Api/Dockerfile -t todo-orders-api:dev .
docker build -f src/services/orders/Todo.Order.Worker/Dockerfile -t todo-orders-worker:dev .
docker build -f src/services/inventory/Todo.Inventory.Worker/Dockerfile -t todo-inventory-worker:dev .

cp deploy/secret.local.example.yaml deploy/secret.local.yaml
kubectl apply -f deploy/secret.local.yaml
kubectl apply -k deploy/overlays/local
```

Confirm the first deployment:

```sh
kubectl get pods
kubectl rollout status deployment/todo-api
kubectl rollout status deployment/todo-orders-api
kubectl rollout status deployment/todo-orders-worker
kubectl rollout status deployment/todo-inventory-worker
```

The Ingress routes `/api/orders` to `todo-orders`. It routes all other HTTP paths to
`todo-api`. Order and Inventory communicate through RabbitMQ, not direct HTTP calls.

## Update one service locally

Rebuild only the changed image, then restart only its Deployment. For example:

```sh
docker build -f src/services/inventory/Todo.Inventory.Worker/Dockerfile \
  -t todo-inventory-worker:dev .
kubectl rollout restart deployment/todo-inventory-worker
kubectl rollout status deployment/todo-inventory-worker
```

If a Kubernetes manifest or local patch changes, apply the complete local overlay again:

```sh
kubectl apply -k deploy/overlays/local
```

This operation is idempotent. Kubernetes updates changed resources and leaves unchanged
Pod templates running. Applying the complete overlay does not remove independent scaling.

In a cluster that provides the base Service DNS names, each base service group can also be
applied independently:

```sh
kubectl apply -k deploy/base/api
kubectl apply -k deploy/base/orders
kubectl apply -k deploy/base/inventory
```

Deployments created by either the base or an overlay scale independently:

```sh
kubectl scale deployment/todo-api --replicas=2
kubectl scale deployment/todo-orders-api --replicas=3
kubectl scale deployment/todo-orders-worker --replicas=2
kubectl scale deployment/todo-inventory-worker --replicas=2
```

Map the local host name once:

```sh
echo "127.0.0.1 todo.local" | sudo tee -a /etc/hosts
```

Then use:

```text
http://todo.local/api/orders
http://todo.local/api/TodoItems
```

## Future staging and production flow

Keep common resources in `deploy/base`. Add `deploy/overlays/staging` or
`deploy/overlays/production` only when that environment exists. Each environment overlay
references `../../base` and patches only its image tags, Ingress host, replicas, resources,
and environment-specific settings.

Do not copy the local `host.docker.internal` patches into staging or production. Keep
cluster-native dependency names in the base, and put the database and Redis Kubernetes
Service DNS names in the environment Secret. Store real Secrets in the deployment system,
not in Git.

The deployment order is:

```text
build and publish immutable images
-> apply environment Secrets
-> run required database migration Jobs
-> kubectl apply -k deploy/overlays/<environment>
-> wait for each changed Deployment rollout
```

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
