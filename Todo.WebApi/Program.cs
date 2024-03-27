using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todo.Application;
using Todo.Application.TodoItemFeatures;
using Todo.Application.TodoListFeatures;
using Todo.WebApi.Contracts;
using Todo.WebApi.Middlewares;
using Todo.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

builder.Services
    .RegisterApplication()
    .RegisterInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapGet("/todo-lists", async (IMediator mediator) =>
{
    var res = await mediator.Send(new GetTodoListsQuery());
    return TypedResults.Ok(res);
});

app.MapPost("/todo-lists", async ([FromBody] CreateTodoListForm request, IMediator mediator) =>
{
    var res = await mediator.Send(new CreateTodoListCommand(request.Name));
    return TypedResults.Created($"/todo-lists/{res.TodoList.Id}", res);
});

app.MapDelete("/todo-lists/{id}", async (int id, IMediator mediator) =>
{
    var res = await mediator.Send(new DeleteTodoListCommand(id));
    return TypedResults.NoContent();
});

app.MapPatch("/todo-lists/{id}", async (int id, [FromBody] UpdateTodoListForm request, IMediator mediator) =>
{
    var res = await mediator.Send(new UpdateTodoListCommand(id, request.Name));
    return TypedResults.Ok(res);
});

app.MapPatch("/todo-items/{id}/complete", async (int id, IMediator mediator) =>
{
    var res = await mediator.Send(new CompleteTodoItemCommand(id));
    return TypedResults.Ok(res);
});

app.MapPost("/todo-items", async ([FromBody] CreateTodoItemForm request, IMediator mediator) =>
{
    var res = await mediator.Send(new CreateTodoItemCommand(request.Text, request.TodoListId));
    return TypedResults.Created(
        $"/todo-items/{res.TodoItem.Id}",
        res
    );
});

app.MapPatch("/todo-items/{id}", async (int id, [FromBody] UpdateTodoItemForm request, IMediator mediator) =>
{
    var res = await mediator.Send(new UpdateTodoItemCommand(id, request.Text));
    return res;
});

app.MapDelete("/todo-items/{id}", async (int id, IMediator mediator) =>
{
    var res = await mediator.Send(new DeleteTodoItemCommand(id));
    return TypedResults.NoContent();
});

app.Run();
