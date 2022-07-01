using CategoryService.AsyncConnection;
using CategoryService.Client;
using CategoryService.Data;
using CategoryService.NewsClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDBContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionStrings")));



builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHttpClient("NewsClientPlicy")
    //.AddPolicyHandler(new ClientPolicy().linearRetryPolicy)
    .AddPolicyHandler(new ClientPolicy().circutBreakerPolicy);

builder.Services.AddControllers();

builder.Services.AddScoped<IRepository,NewsCategoryRepository>();
builder.Services.AddScoped<IClientUpdate, NewsService>();
builder.Services.AddSingleton<INotification, NewsServiceNotification>();

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
