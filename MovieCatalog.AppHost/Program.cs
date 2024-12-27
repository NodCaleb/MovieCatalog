var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.MovieCatalog_Api>("moviecatalog-api");

builder.Build().Run();
