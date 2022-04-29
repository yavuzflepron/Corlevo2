using DataLayer.CQRS.Handlers.CommandHandlers;
using DataLayer.CQRS.Handlers.QueryHandlers;
using MediatR;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(x => x.AddPolicy("CORS", z => z.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Corlevo API",
        Description = "This is a sample API for `Corlevo` developed to do CRUD operations with CQRS. It uses .Net6.0, MediatR and Google Cloud Datastore as database. Swagger is used as API documentation.",
        Contact = new OpenApiContact
        {
            Name = "Yavuz Cingoz",
            Email = "yavuz@flepron.com"
        }
    });
});

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.GetName().Name == "DataLayer"));
builder.Services.AddTransient<GetProductListQueryHandler>();
builder.Services.AddTransient<GetProductByIdQueryHandler>();
builder.Services.AddTransient<CreateProductCommandHandler>();
builder.Services.AddTransient<UpdateProductCommandHandler>();
builder.Services.AddTransient<DeleteProductCommandHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("CORS");

app.Run();