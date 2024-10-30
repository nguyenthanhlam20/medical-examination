using BusinessObjects.Models;
using Coordinators.Medicals;
using Coordinators.Patients;
using Microsoft.EntityFrameworkCore;
using Services.Doctors;
using Services.Medicals;
using Services.Records;
using WebClient.Helpers;
using WebClient.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<OmsContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DB"));
});

// Register coordinators
builder.Services.AddTransient<IPatientCoordinator, PatientCoordinator>();
builder.Services.AddTransient<IMedicalCoordinator, MedicalCoordinator>();

// Register serivces
builder.Services.AddTransient<IDoctorAvailableService, DoctorAvailableService>();
builder.Services.AddTransient<IMedicalService, Services.Medicals.MedicalService>();
builder.Services.AddTransient<IMedicalRecordService, MedicalRecordService>();

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddSingleton<IEmailHelper, EmailHelper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
