using IdlegharDotnetBackend.Providers;
using IdlegharDotnetDomain.Providers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var JWTProvider = new JWTProvider("los gatitos son lo mejor");
builder.Services.AddSingleton<ICryptoProvider, CryptoProvider>();
builder.Services.AddSingleton<IAuthProvider>(JWTProvider);
builder.Services.AddSingleton<IEmailsProvider, EmailsProvider>();

builder.Services.AddSingleton<IRepositoryAggregator, RepositoryAggregator>();


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
