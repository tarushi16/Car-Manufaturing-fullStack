using Car_Manufacturing.Data;
using Car_Manufacturing.Services;
using Car_Manufacturing.Services.CarModels;
using Car_Manufacturing.Services.Customers;
using Car_Manufacturing.Services.Production;
using Car_Manufacturing.Services.Sales;
using Car_Manufacturing.Services.Inventory;
using Car_Manufacturing.Services.Notifications;
using Car_Manufacturing.Services.QualityControl;
using Car_Manufacturing.Services.Supplier;
using Car_Manufacturing.Services.Reporting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Car_Manufacturing.Models;
using Car_Manufacturing.Repositories;
using Car_Manufacturing.Repositories.Sales;
using Car_Manufacturing.Repositories.Suppliers;
using Car_Manufacturing.Repositories.Authentication;
using Car_Manufacturing.Services.Authentication; // Ensure this using directive is included


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register controllers
builder.Services.AddControllers();

// Add JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// Register services for Dependency Injection
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFinancialService, FinancialService>();
builder.Services.AddScoped<ICarModelService, CarModelService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ISalesOrderService, SalesOrderService>();
builder.Services.AddScoped<IProductionService, ProductionService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IQualityControlService, QualityControlService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();  // Register AuthenticationService

// Register repositories
builder.Services.AddScoped<IRepository<CarModel>, CarModelRepository>();
builder.Services.AddScoped<IRepository<Customer>, CustomerRepository>();
builder.Services.AddScoped<IRepository<InventoryModel>, InventoryRepository>();
builder.Services.AddScoped<IRepository<Notification>, NotificationRepository>();
builder.Services.AddScoped<IRepository<ProductionOrder>, ProductionOrderRepository>();
builder.Services.AddScoped<IRepository<SupplierModel>, SupplierRepository>();
builder.Services.AddScoped<IRepository<Finance>, FinanceRepository>();
builder.Services.AddScoped<IRepository<QualityReport>, QualityReportRepository>();
builder.Services.AddScoped<IRepository<SalesOrder>, SalesOrderRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRepository<Report>, ReportRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();  // Register AuthRepository

// Add Authorization
builder.Services.AddAuthorization();

// Enable CORS (Configure based on your frontend URL)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:4200", "https://localhost:4200") // Replace with your frontend URL
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Add Swagger (Optional, but useful during development for testing APIs)
builder.Services.AddSwaggerGen();

// Add Logging (Optional, but recommended)
builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseCors("AllowSpecificOrigin"); // Use the CORS policy
app.UseAuthentication();
app.UseAuthorization();

// Enable Swagger for API documentation (only in development or specific environment)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Map controllers to define the API endpoints
app.MapControllers();

app.Run();
