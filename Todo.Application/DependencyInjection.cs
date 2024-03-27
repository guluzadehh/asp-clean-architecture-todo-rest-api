using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Common;
using Todo.Application.Mappers;

namespace Todo.Application;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services.AddMediatR(
            config =>
            {
                config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            }
        );
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.RegisterMappers();
        return services;
    }

    public static IServiceCollection RegisterMappers(this IServiceCollection services)
    {
        services.AddSingleton<TodoListMapper>();
        services.AddSingleton<TodoItemMapper>();
        return services;
    }
}
