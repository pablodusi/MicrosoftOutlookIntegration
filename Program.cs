using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using MicrosoftOutlookIntegration.Contexts;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Data Source=PABLONOTEBOOK; Initial Catalog = MSOutlookIntegration; Integrated Security = True;TrustServerCertificate=True;Encrypt=False";

builder.Services.AddDbContext<UserDbContext>(x => x.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//// Configure authentication
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/login";
//        options.LogoutPath = "/logout";
//    });

//builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
//    .AddOpenIdConnect(options =>
//    {
//        options.ClientId = "your-client-id";
//        options.ClientSecret = "your-client-secret";
//        options.Authority = "https://login.microsoftonline.com/common/";
//        options.SignedOutRedirectUri = "https://localhost:5000/";
//    });

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