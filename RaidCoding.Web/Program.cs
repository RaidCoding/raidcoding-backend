using RaidCoding.Extensions;
using RaidCoding.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuth(builder.Configuration);
builder.Services.AddBusinessServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policyBuilder =>
            policyBuilder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseAuthorization();
app.UseRcExceptionHandling();
app.MapControllers();

app.Run();