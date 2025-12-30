using Microsoft.EntityFrameworkCore;
using Streetcode.Auth.Api.Extensions;
using Streetcode.Auth.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddMessaging(builder.Configuration);
builder.Services.AddOtlp(builder.Configuration);


builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSwaggerWithJwt();

builder.Services.AddHttpContextAccessor();


builder.Services.ConfigureSerilog(builder);

var app = builder.Build();


//uncomment to apply migrations on startup

//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
//    db.Database.Migrate();
//}


// Configure the HTTP request pipeline.
if (app.Environment.EnvironmentName == "Development")
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIv5 v1"));
}
else
{
    app.UseHsts();
}

app.UseCors();

//await app.Services.SeedIdentityAsync(); // uncomment for seeding data

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();