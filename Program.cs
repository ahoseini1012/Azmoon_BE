using Agricaltech;
using Agricaltech.DL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile("appsettings.json",
                       optional: true,
                       reloadOnChange: true);

});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy1",
                  policy =>
                  {
                      policy
                    //   .WithOrigins("http://localhost:4200", "http://ui.h-khademin.ir/", "https://ui.h-khademin.ir/")
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                  });
});

builder.Services.AddConfig(builder.Configuration);
builder.Services.AddSingleton<DbContext>();
builder.Services.AddSignalR();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

 var loggerFactory = app.Services.GetService<ILoggerFactory>();
loggerFactory.AddFile(builder.Configuration["Logging:LogFilePath"]!.ToString()); 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();
app.MapHub<MyHub>("/azmoon");
app.Run();
