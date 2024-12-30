var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
				 .AddDatabase("MovieCatalog");

builder.AddProject<Projects.MovieCatalog_Api>("moviecatalog-api")
	.WithExternalHttpEndpoints()
	.WithReference(sql)
	.WaitFor(sql);

builder.Build().Run();
