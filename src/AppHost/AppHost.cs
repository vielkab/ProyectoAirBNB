var builder = DistributedApplication.CreateBuilder(args);
/*var redis = builder
    .AddRedis("servidorredis")
    .WithLifetime(ContainerLifetime.Persistent);*/
var postgres = builder
    .AddPostgres("servidorpostgres")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithPgAdmin(pg => pg.WithLifetime(ContainerLifetime.Persistent).WithHostPort(5555))
    .AddDatabase("BDPostgres");
/*var kafka = builder
    .AddKafka("servidorkafka")
    .WaitFor(postgres)
    .WithLifetime(ContainerLifetime.Persistent);*/
_ = builder
    .AddSqlServer("servidorsqlserver")
    //.WaitFor(kafka)
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase("BDSqlServer");
_ = builder
    .AddProject<Projects.Api>("api")
    .WithExternalHttpEndpoints()
    .WithReference(postgres)
    .WaitFor(postgres);

builder.Build().Run();
