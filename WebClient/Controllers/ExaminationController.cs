using Coordinators.Medicals;
using Coordinators.Patients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ViewModels;
using WebClient.Helpers;

namespace WebClient.Controllers;

[Route("[Controller]/[Action]")]
public class ExaminationController(
    IPatientCoordinator patientCoordinator,
    IMedicalCoordinator medicalCoordinator,
    IEmailHelper emailHelper) : Controller
{
    private readonly IPatientCoordinator _patientCoordinator = patientCoordinator;
    private readonly IMedicalCoordinator _medicalCoordinator = medicalCoordinator;
    private readonly IEmailHelper _emailHelper = emailHelper;

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var records = await _patientCoordinator.ListAsync();
        return View(records);
    }

    [HttpGet]
    public async Task<IActionResult> Register()
    {
        var services = await _medicalCoordinator.ListAsync();
        var selectService = new SelectList(services, "Id", "Name");
        ViewData["selectService"] = selectService;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterExaminationModel request)
    {
        try
        {
            var services = await _medicalCoordinator.ListAsync();
            var selectService = new SelectList(services, "Id", "Name");
            ViewData["selectService"] = selectService;
            if (!ModelState.IsValid) throw new Exception("Incomplete, please re-enter");

            var response = await _patientCoordinator.RegisterAsync(request);
            if (!response.Success) throw new Exception(response.Message);

            ToastHelper.ShowSuccess(TempData, response.Message);
            
            var subject = "Register Medical Examination Confirmation";
            var body = request.ToString();

            await _emailHelper.SendEmailAsync(request.Email, subject, body);
            return RedirectToAction(nameof(List));
        }
        catch (Exception ex)
        {
            ToastHelper.ShowError(TempData, ex.Message);
            return View(request);
        }

    }

    [HttpGet]
    public async Task<IActionResult> Details([FromQuery] int id)
    {
        var record = await _patientCoordinator.GetMedicalRecordAsync(id);
        if (record is null)
        {
            ToastHelper.ShowError(TempData, "No record found.");
            return RedirectToAction(nameof(List));
        }

        return View(record);
    }

}
