using DESAFIO.BLOG.Application.Services;
using DESAFIO.BLOG.Domain.Repositories;
using DESAFIO.BLOG.Infrastructure.Data;
using DESAFIO.BLOG.Infrastructure.Repositories;
using DESAFIO.BLOG.Presentation.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 21))
    ));

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
builder.Services.AddScoped<IChatService, ChatService>();

// Add Azure AD B2C authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));

// Add authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireClaim("groups", "Admin"));
});

// Add SignalR
builder.Services.AddSignalR();

// Add UserContext
builder.Services.AddScoped<UserContext>((serviceProvider) =>
{
    var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
    var userContext = new UserContext
    {
        UserId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier),
        IsAdmin = httpContextAccessor.HttpContext.User.IsInRole("Admin")
    };
    return userContext;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Define uma rota para a página de login do Azure B2C
app.MapGet("/SignIn", (HttpContext context) =>
{
    context.Response.Redirect("/MicrosoftIdentity/Account/SignIn");
    return Task.CompletedTask;
});

// Define uma rota para a página de logout do Azure B2C
app.MapGet("/SignOut", (HttpContext context) =>
{
    context.Response.Redirect("/MicrosoftIdentity/Account/SignOut");
    return Task.CompletedTask;
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chatHub");

app.Run();
