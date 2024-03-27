using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Services;
using Todo.Domain.Interfaces;
using Todo.Infrastructure.Database;
using Todo.Infrastructure.Repositories;
using Todo.Infrastructure.Services;

namespace Todo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
        services.AddSingleton<ITodoListRepository, TodoListRepository>();
        services.AddSingleton<ITodoItemRepository, TodoItemRepository>();
        services.AddSingleton<ITodoListService, TodoListService>();
        return services;
    }
}
