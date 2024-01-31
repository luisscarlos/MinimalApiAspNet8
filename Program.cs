using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/users", () => User.Users);

app.MapGet("/user/{id}", (int id) => User.Users.FirstOrDefault(user => user.Id == id));

app.MapPost("/user", (User user) => User.Users.Add(user));

app.MapPut("/user/{id}", (int id, User user) =>
{
    User currentUser = User.Users.FirstOrDefault(user => user.Id == id);
    if (currentUser != null)
    {
        currentUser.FirstName = user.FirstName;
        currentUser.LastName = user.LastName;
        currentUser.BirthDate = user.BirthDate;
    }
    
});

app.MapDelete("/user/{id}", (int id) =>
{
    var userForDeletion = User.Users.FirstOrDefault(user => user.Id == id);

    if (userForDeletion != null)
    {
        User.Users.Remove(userForDeletion);
    }
});

app.Run();

public class User
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateOnly BirthDate { get; set; }

    public static List<User> Users =
    [
        new User()
        {
            Id = 1,
            FirstName = "Callie",
            LastName = "Hackforth",
            BirthDate = new DateOnly(1995, 10, 3)
        },
        new User()
        {
            Id = 2,
            FirstName = "Odell",
            LastName = "Blowes",
            BirthDate = new DateOnly(1984, 4, 7)
        },
        new User()
        {
            Id = 3,
            FirstName = "Callie",
            LastName = "Corrett",
            BirthDate = new DateOnly(1991, 3, 4)
        }
    ];
}
