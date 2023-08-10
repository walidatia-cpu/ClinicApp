using ClinicApp.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Config dbcontext and Identity
builder.Services.AddConfig(builder.Configuration);
builder.Services.AddJWTAuthentication(builder.Configuration);

//Register My Services
builder.Services.AddMyDependencyGroup();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// migrate database 
using (var scope = app.Services.CreateScope())
{
    scope.MigrateDatabase();
    await scope.SeedDefaultData();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
