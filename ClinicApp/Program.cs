using ClinicApp.Extensions;
using ClinicApp.Filters.ActionFilter;
using ClinicApp.Middleware;
using ClinicApp.Security;

var builder = WebApplication.CreateBuilder(args);

// Config dbcontext and Identity
builder.Services.AddConfig(builder.Configuration);
builder.Services.AddJWTAuthentication(builder.Configuration);

//Register My Services
builder.Services.AddMyDependencyGroup();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ValidateModelAttribute));
}); 

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.AddGlobalErrorHandler();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
