using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Spg.Codechatter.Application.V1.Interfaces.ChatroomService;
using Spg.Codechatter.Application.V1.Interfaces.MessageService;
using Spg.Codechatter.Application.V1.Interfaces.TextChannelService;
using Spg.Codechatter.Application.V1.Interfaces.UserService;
using Spg.Codechatter.Application.V1.Services;
using Spg.Codechatter.Domain.V1.Model;
using Spg.Codechatter.Infrastructure;
using Spg.Codechatter.Repository.V1.Interfaces.ChatroomRepository;
using Spg.Codechatter.Repository.V1.Interfaces.MessageRepository;
using Spg.Codechatter.Repository.V1.Interfaces.TextChannelRepository;
using Spg.Codechatter.Repository.V1.Interfaces.UserRepository;
using Spg.Codechatter.Repository.V1.Repositories;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddSingleton<IMongoClient>(_ =>
{
    var connectionString = configuration.GetConnectionString("MongoDbConnection");
    return new MongoClient(connectionString);
});

builder.Services.AddScoped<CodechatterMongoContext>(_ =>
{
    var serviceProvider = _.GetRequiredService<IServiceProvider>();
    var configurationIntern = serviceProvider.GetRequiredService<IConfiguration>();
    var client = _.GetRequiredService<IMongoClient>();
    return new CodechatterMongoContext(client, configurationIntern);
});

// Register the seeder
builder.Services.AddTransient<CodechatterContextSeeder>(_ =>
{
    var context = _.GetRequiredService<CodechatterMongoContext>();
    return new CodechatterContextSeeder(context);
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// V1

// chatroom
builder.Services.AddTransient<IReadChatroomService, ChatroomService>();
builder.Services.AddTransient<IModifyChatroomService, ChatroomService>();
builder.Services.AddTransient<IReadChatroomRepository, ChatroomRepository>();
builder.Services.AddTransient<IModifyChatroomRepository, ChatroomRepository>();

// user
builder.Services.AddTransient<IReadUserService, UserService>();
builder.Services.AddTransient<IModifyUserService, UserService>();
builder.Services.AddTransient<IReadUserRepository, UserRepository>();
builder.Services.AddTransient<IModifyUserRepository, UserRepository>();

// message
builder.Services.AddTransient<IReadMessageService, MessageService>();
builder.Services.AddTransient<IModifyMessageService, MessageService>();
builder.Services.AddTransient<IReadMessageRepository, MessageRepository>();
builder.Services.AddTransient<IModifyMessageRepository, MessageRepository>();

// textchannel
builder.Services.AddTransient<IReadTextChannelService, TextChannelService>();
builder.Services.AddTransient<IModifyTextChannelService, TextChannelService>();
builder.Services.AddTransient<IReadTextChannelRepository, TextChannelRepository>();
builder.Services.AddTransient<IModifyTextChannelRepository, TextChannelRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Codechatter - v1",
        Description = "Description about Codechatter",
        Contact = new OpenApiContact()
        {
            Name = "Bauer Manuel",
            Email = "bau20219@spengergasse.at",
            Url = new Uri("http://www.spengergasse.at")
        },
        License = new OpenApiLicense()
        {
            Name = "Spenger-Licence",
            Url = new Uri("http://www.spengergasse.at/licence")
        },
        Version = "v1"
    });

    s.SwaggerDoc("v2", new OpenApiInfo()
    {
        Title = "Codechatter - v2",
        Description = "Description about Codechatter - v2",
        Contact = new OpenApiContact()
        {
            Name = "Bauer Manuel",
            Email = "bau20219@spengergasse.at",
            Url = new Uri("http://www.spengergasse.at")
        },
        License = new OpenApiLicense()
        {
            Name = "Spenger-Licence",
            Url = new Uri("http://www.spengergasse.at/licence")
        },
        Version = "v2"
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "allowedOrigins", policy =>
    {
       
          
                policy.AllowAnyOrigin(); // Allow any origin
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                
            
        });
   
});

builder.Services.AddApiVersioning(o =>
{
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    o.ReportApiVersions = true;
    o.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("ver"));
});

builder.Services.AddVersionedApiExplorer(
    options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "API V2");
    });
}

app.UseHttpsRedirection();
app.UseCors("allowedOrigins");
app.MapControllers();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    
    var seeder = scope.ServiceProvider.GetRequiredService<CodechatterContextSeeder>();
    seeder.Seed(chatroomCount: 10, userCount: 100, textChannelCount: 10, messageCount: 100);
}

app.Run();