using Microsoft.EntityFrameworkCore;
using TechTest.Data;
using TechTest.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddControllers();
services.AddScoped<IRobotService, RobotService>();
services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("techtestdb"));
services.AddSwaggerGen();
services.AddScoped<INotificationService, EngineeringNotificationService>();
services.AddScoped<INotificationService, CustomerNotificationService>();
services.AddScoped<INotificationService, InvoicingNotificationService>();
services.AddScoped<Repository>();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TechTest API V1");
    });

    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetService<DataContext>();
    DataSeeder.SeedData(context);
}
app.Run();
