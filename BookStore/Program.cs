using BookStore.Data.Abstraction;
using BookStore.Data.MongoDB;
using BookStore.Repositories;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;

internal class Program
{
    private static Assembly[] Assemblies;

    private static void Main(string[] args)
    {
        Assemblies = LoadApplicationDependencies();

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddFluentValidation(options =>
        {
            options.RegisterValidatorsFromAssemblies(Assemblies);
            });
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assemblies));




        // Add services to the container.

        //builder.Services.AddSingleton<IDatabase, Database>();

        //builder.Services.AddSingleton<IDatabaseConfiguration>(builder.Configuration.Get<DatabaseConfiguration>());

        builder.Services.AddSingleton<IDatabaseConfiguration> (_ => new DatabaseConfiguration {ConnectionString=  builder.Configuration.GetValue<string>("DatabaseConfiguration:ConnectionString"), 
            DatabaseName= builder.Configuration.GetValue<string>("DatabaseConfiguration:DatabaseName")
        });


        //builder.Services.Configure<IDatabaseConfiguration>(builder.Configuration.GetSection("DatabaseConfiguration"));

        //builder.Services.AddScoped<IBookRepository, BookRepository>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.Scan(scan => scan.FromAssemblies(Assemblies).
        AddClasses(type => type.AssignableTo(typeof(IRepository<>))).
        AsImplementedInterfaces().
        WithScopedLifetime().
        AddClasses(type => type.AssignableTo(typeof(IDatabase))).
        AsImplementedInterfaces().
        WithSingletonLifetime()
        );    

        var app = builder.Build();

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
    }
    private static Assembly[] LoadApplicationDependencies()
    {
        var context = DependencyContext.Default;
        return context.RuntimeLibraries.SelectMany(library =>
        library.GetDefaultAssemblyNames(context))
            .Where(assembly => assembly.FullName.Contains("BookStore"))
            .Select(Assembly.Load).ToArray();
    }


}