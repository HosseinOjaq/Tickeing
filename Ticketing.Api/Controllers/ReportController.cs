using Stimulsoft.Report;
using Stimulsoft.Drawing;
using Ticketing.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Stimulsoft.Report.Export;
using Ticketing.Domain.Models.Reports;

namespace Ticketing.Api.Controllers;

/// <summary>
/// Report
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    [HttpPost("education/pdf")]
    public IActionResult GenerateEducationReport(ReportExportType exportType)
    {
        var reportPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Reports", "EducationReport.mrt");

        var report = new StiReport();
        report.Load(reportPath);

        report.RegBusinessObject("Person", StaticEducationData.Participants);

        void SetVariable(string name, string value)
        {
            if (report.Dictionary.Variables.Contains(name))
                report.Dictionary.Variables[name].Value = value;
            else
                report.Dictionary.Variables.Add(name, value);
        }

        var reportData = StaticEducationData.EducationReport;

        SetVariable(nameof(reportData.ClassRoomType), reportData.ClassRoomType);
        SetVariable(nameof(reportData.CourseTitle), reportData.CourseTitle);
        SetVariable(nameof(reportData.OrganizationName), reportData.OrganizationName);
        SetVariable(nameof(reportData.ClassDurationHours), reportData.ClassDurationHours);
        SetVariable(nameof(reportData.Venue), reportData.Venue);
        SetVariable(nameof(reportData.StartClassDate), reportData.StartClassDate);
        SetVariable(nameof(reportData.EndClassDate), reportData.EndClassDate);
        SetVariable(nameof(reportData.StartClassTime), reportData.StartClassTime);
        SetVariable(nameof(reportData.EndClassTime), reportData.EndClassTime);
        SetVariable(nameof(reportData.Teachername), reportData.Teachername);
        SetVariable(nameof(reportData.Teachername), reportData.Teachername);

        var imageBytes = System.IO.File.ReadAllBytes("wwwroot/Images/download.Png");
        Image logo;
        using (var ms = new MemoryStream(imageBytes))
        {
            logo = Image.FromStream(ms);
        }

        report.Dictionary.Variables.Add("OrganizationLogo", logo);

        report.Compile();
        report.Render(false);

        var stream = new MemoryStream();
        string fileName = $"EducationReport_{DateTime.Now:yyyy-MM-dd-HH-mm-ss}";
        string mimeType = "application/pdf";

        switch (exportType)
        {
            case ReportExportType.Pdf:
                new StiPdfExportService().ExportTo(report, stream, new StiPdfExportSettings());
                fileName += ".pdf";
                mimeType = "application/pdf";
                break;
            case ReportExportType.Word:
                new StiWordExportService().ExportTo(report, stream, new StiWordExportSettings());
                fileName += ".docx";
                mimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                break;
            case ReportExportType.Excell:
                new StiExcelExportService().ExportTo(report, stream, new StiExcelExportSettings());
                fileName += ".xlsx";
                mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                break;
            default:
                new StiPdfExportService().ExportTo(report, stream, new StiPdfExportSettings());
                fileName += ".pdf";
                mimeType = "application/pdf";
                break;
        }

        stream.Position = 0;

        return File(stream.ToArray(), mimeType, fileName);
    }
}