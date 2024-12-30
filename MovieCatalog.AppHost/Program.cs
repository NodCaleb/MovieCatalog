var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.MovieCatalog_Api>("moviecatalog-api")
	.WithExternalHttpEndpoints();

builder.Build().Run();
