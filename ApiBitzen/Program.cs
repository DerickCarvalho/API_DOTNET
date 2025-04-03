using ApiBitzen.Data;
using ApiBitzen.Usuarios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Configuracao de rotas
app.AddRotasUsuarios();

Console.Write("API em funcionamento\n\nAcesso: http://localhost:5102/swagger/index.html\n\n\n");

app.Run();