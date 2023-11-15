using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));


var app = builder.Build();



app.MapGet("/", () => "Hello World!");

app.MapGet("/todoItems", async (TodoDb db) =>
        await db.Todos.ToListAsync());

app.MapGet("/todoItems/{id}", async (int id, TodoDb db) => 
                    await db.Todos.FindAsync(id)
        is Todo todo
            ? Results.Ok(todo)
            : Results.NoContent()
        );

app.Run();
