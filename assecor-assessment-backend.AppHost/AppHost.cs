var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.assecor_assessment_backend>("assecor-assessment-backend");

builder.Build().Run();
