// using System.Text;
// using static System.Net.Mime.MediaTypeNames; // Production Env Exception Content-Type construct
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.AspNetCore.Diagnostics;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.IdentityModel.Tokens;
// using Serilog;
// using Scalar.AspNetCore;
// using Todo.DAL.Context;
// using Todo.WebApi.Configuration;
// using Todo.WebApi.Response.Pagination;
//
// var builder = WebApplication.CreateBuilder(args);
//
//
// # region Service Configuration
//
// // Add services to the container.
// var services = builder.Services;
//
// // log
// builder.Logging.ClearProviders();
// Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();
// builder.Host.UseSerilog(
//     (context, srv, configuration) =>
//         configuration
//             .ReadFrom.Configuration(context.Configuration)
//             .ReadFrom.Services(srv)
// );
//
// // db
// services.AddDbContext<ApplicationIdentityDbContext>(
//     opt =>
//         opt.UseNpgsql(builder.Configuration.GetConnectionString("postgres")
//                       ?? throw new Exception("No db connection appsettings.json.")
//         )
// );
//
// // AutoMapper
// services.AddAutoMapper(typeof(Program));
//
// // identity authentication
// services.AddIdentity<IdentityUser, IdentityRole>(
//         options =>
//             options.SignIn.RequireConfirmedAccount = true
//     )
//     .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
//     .AddDefaultTokenProviders();
// services.Configure<IdentityOptions>(options =>
// {
//     // Password strength settings.
//     options.Password.RequireDigit = true;
//     options.Password.RequireLowercase = true;
//     options.Password.RequireNonAlphanumeric = false;
//     options.Password.RequireUppercase = false;
//     options.Password.RequiredLength = 6;
//     options.Password.RequiredUniqueChars = 1;
// });
//
// // pagination uri generate service
// services.AddHttpContextAccessor();
// services.AddSingleton<IUriService>(o =>
// {
//     var accessor = o.GetRequiredService<IHttpContextAccessor>();
//     var request = accessor.HttpContext?.Request;
//     var uri = string.Concat(request?.Scheme, "://", request?.Host.ToUriComponent());
//     return new UriService(uri);
// });
//
// // cors
// services.AddCors(options =>
// {
//     options.AddDefaultPolicy(policyBuilder =>
//     {
//         policyBuilder.AllowAnyOrigin().AllowAnyHeader()
//             .AllowAnyMethod();
//     });
// });
//
// // controllers
// services.AddControllers();
//
// // jwt authentication
// var jwtSection = builder.Configuration.GetSection("Jwt");
// services.Configure<JwtSettings>(jwtSection);
// var jwtSettings = jwtSection.Get<JwtSettings>()!;
// var encryptedKey = Encoding.ASCII.GetBytes(jwtSettings.Key!);
// services.AddAuthentication(options =>
//         {
//             options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//             options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//         }
//     )
//     .AddJwtBearer(options =>
//     {
//         options.RequireHttpsMetadata = false;
//         options.SaveToken = true;
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidIssuer = jwtSettings.Issuer,
//             ValidateAudience = true,
//             ValidAudience = jwtSettings.Audience,
//             ValidateIssuerSigningKey = true,
//             IssuerSigningKey = new SymmetricSecurityKey(encryptedKey)
//         };
//     });
// services.AddAuthorization();
//
// // scalar openapi
// services.AddOpenApi();
//
// # endregion
//
//
// var app = builder.Build();
//
//
// # region Pipeline
//
// // Configure the HTTP request pipeline.
//
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
//     app.MapScalarApiReference("/", options =>
//     {
//         options
//             .WithTitle("Todo.WebApi");
//     });
// }
// else
// {
//     app.UseExceptionHandler(exceptionHandlerApp =>
//                                 exceptionHandlerApp.Run(async context =>
//                                     {
//                                         context.Response.StatusCode =
//                                             StatusCodes.Status500InternalServerError;
//
//                                         context.Response.ContentType = Text.Plain;
//
//                                         await context.Response.WriteAsync(
//                                             "An exception was thrown.");
//
//                                         var exceptionHandlerPathFeature =
//                                             context.Features.Get<IExceptionHandlerPathFeature>();
//
//                                         if (exceptionHandlerPathFeature?.Error is
//                                             FileNotFoundException)
//                                         {
//                                             await context.Response.WriteAsync(
//                                                 " The file was not found.");
//                                         }
//
//                                         if (exceptionHandlerPathFeature?.Path == "/")
//                                         {
//                                             await context.Response.WriteAsync(" Page: Home.");
//                                         }
//                                     }
//                                 )
//     );
//     app.UseHsts();
// }
//
// app.UseSerilogRequestLogging();
// Log.Information("Serilog initialized");
// app.UseHttpsRedirection();
// app.UseCors();
// app.UseAuthentication();
// app.UseAuthorization();
// app.MapControllers();
//
// # endregion
//
// await app.RunAsync();


