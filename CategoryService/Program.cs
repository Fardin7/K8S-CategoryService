using CategoryService.Client;
using CategoryService.Data;
using CategoryService.NewsClient;
using GreenPipes;
using MassTransit;
using MassTransit.Definition;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDBContext>(opt => opt.UseInMemoryDatabase("InMem"));

//builder.Services.AddDbContext<AppDBContext>(option =>
//option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionStrings")));

builder.Services.AddMassTransit(configure =>
{
    configure.UsingRabbitMq((context, configurator) =>
    {
        // var configuration = context.GetService<IConfiguration>();
        configurator.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter("Category", false));
        configurator.UseMessageRetry(retry =>
        {
            retry.Interval(4, TimeSpan.FromSeconds(4));
        });
    });
});

builder.Services.AddMassTransitHostedService();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHttpClient("NewsClientPlicy")
    .AddPolicyHandler(new ClientPolicy().linearRetryPolicy)
    .AddPolicyHandler(new ClientPolicy().circutBreakerPolicy);

builder.Services.AddControllers();

builder.Services.AddScoped<IRepository,NewsCategoryRepository>();
builder.Services.AddScoped<IClientUpdate, NewsService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
//{
//    var db = scope.ServiceProvider.GetService<AppDBContext>();
//    db.Database.Migrate();

//}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.Run();
