using FH_Api_Demo.Web;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddMemoryCache();

//Invalid Model Handling
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
{
    return new BadRequestObjectResult(new
    {
        status = 400,
        message = "Invalid request body."
    });
};

});

#region DbContext Initialization , Services And Repositories Registration
var connectionString = builder.Configuration.GetConnectionString("TatvasoftFhContext");
DependencyInjection.RegisterServices(builder.Configuration, builder.Services, connectionString!);
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngular");
app.UseHttpsRedirection();
app.MapControllers();
app.Run();