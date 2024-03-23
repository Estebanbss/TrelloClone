using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TrelloClone.Data;
using TrelloClone.Services;
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("https://www.trelloclone.somee.com/",
                                              "https://www.trelloclone.somee.com/",
                                              "https://estebanbss.github.io/"
                                              ).AllowAnyHeader()
                                             .AllowAnyMethod();
                      });
});
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
    app.UseSwaggerUI(c=>
{
     c.SwaggerEndpoint("/myApi/swagger/v1/swagger.json", "V1 Docs");

});
}


app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
