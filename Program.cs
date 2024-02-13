using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TrelloClone.Data;
using TrelloClone.Services;
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the DbContext to the container
builder.Services.AddSqlServer<TrelloCloneContext>(builder.Configuration.GetConnectionString("TrelloConnection"));

builder.Services.AddScoped<AccountService>(); // Service Layer
builder.Services.AddScoped<BoardService>(); // Service Layer
builder.Services.AddScoped<ListService>(); // Service Layer
builder.Services.AddScoped<CardService>(); // Service Layer
builder.Services.AddScoped<LoginService>(); // Service Layer

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options => {

#pragma warning disable CS8604 // Possible null reference argument.
     options.TokenValidationParameters = new TokenValidationParameters
     {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
          ValidateIssuer = false,
          ValidateAudience = false
     };
#pragma warning restore CS8604 // Possible null reference argument.
});

builder.Services.AddAuthorization(options =>
{
     options.AddPolicy("Authenticated", policy => policy.RequireClaim("Atype"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
