var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var config = builder.Configuration;

var apiapp = builder.AddProject<Projects.AspireYouTubeSummariser_ApiApp>("apiapp")
                     .WithExternalHttpEndpoints()
                     .WithEnvironment("OpenAI__Endpoint", config["OpenAI:Endpoint"])
                     .WithEnvironment("OpenAI__ApiKey", config["OpenAI:ApiKey"])
                     .WithEnvironment("OpenAI__DeploymentName", config["OpenAI:DeploymentName"]);

builder.AddProject<Projects.AspireYouTubeSummariser_WebApp>("webapp")
       // 추가 👇
       .WithExternalHttpEndpoints()
       // 추가 👆
       .WithReference(cache)
       .WithReference(apiapp);

builder.Build().Run();
