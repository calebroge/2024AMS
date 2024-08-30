using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _2024AMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using _2024AMS.Pages.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks.Dataflow;

namespace _2024AMS.Pages.Reports;

[BindProperties]
public class ViewAssetReportModel : PageModel
{
    private readonly _2024AMSContext _2024AMSContext;
    private readonly IEmailService IEmailService;
    private readonly IWebHostEnvironment IWebHostEnvironment;
    public ViewAssetReportModel(_2024AMSContext AMS, IEmailService IES, IWebHostEnvironment iWebHostEnvironment)
    {
        _2024AMSContext = AMS;
        IEmailService = IES;
        IWebHostEnvironment = iWebHostEnvironment;
    }

    public class JoinResult
    {
        public int? AssetID;
        public int? AssetCategoryID;       
        public int? ManufacturerID;
        public int? UserID;
        public string? AssetCategory1;
        public string? Manufacturer1;        
        public string? Asset1;
        public string? Model;
        public string? ServiceTag;
        public string? SerialNumber;
        public string? MACAddress;
        public string? Barcode;
        public DateTime? WarrantyDate;
        public DateTime? PurchaseDate;
        public string? Notes;
        public string? LastName;
        public string? FirstName;
    }
    public string Filter { get; set; }
    public string RecipientEmailAddress { get; set; }
    public int AssetCategoryID { get; set; }
    public int OperatingSystemID { get; set; }
    public int ManufacturerID { get; set; }

    public SelectList AssetCategorySelectList;
    public SelectList ManufacturerSelectList;
    public SelectList OperatingSystemSelectList;
    private IQueryable<JoinResult> JoinResultIQueryable; 
    private IQueryable<JoinResult> JoinResultIQueryable2; 
    public IList<JoinResult> JoinResultIList;

    public async Task OnGetAsync()
    {
        // Set the message.
        ViewData["Title"] = "View Asset Report";
        if (TempData["MessageColor"] == null)
        {
            TempData["MessageColor"] = "Green";
            TempData["Message"] = "This shows all the assets currently in the system and their information.";
        }
        else
        {
            ViewData["MessageColor"] =
            HttpContext.Session.GetString("MessageColor");
            ViewData["Message"] =
                HttpContext.Session.GetString("Message");
        }

        // Populate the category select list.
        AssetCategorySelectList = new SelectList(_2024AMSContext.AssetCategory
            .OrderBy(c => c.AssetCategory1), "AssetCategoryID", "AssetCategory1");

        // Populate the manufacturer select list.
        ManufacturerSelectList = new SelectList(_2024AMSContext.Manufacturer
            .OrderBy(s => s.Manufacturer1), "ManufacturerID", "Manufacturer1");

        //// Populate the operating system select list.
        //OperatingSystemSelectList = new SelectList(_2024AMSContext.OperatingSystem
        //    .OrderBy(o => o.OperatingSystem1), "OperatingSystemID", "OperatingSystem1");

        await RetrieveRowsForDisplay();
    }
    public async Task OnPostFilterAsync()
    {
        // Populate the category select list.
        AssetCategorySelectList = new SelectList(_2024AMSContext.AssetCategory
            .OrderBy(c => c.AssetCategory1), "AssetCategoryID", "AssetCategory1");

        // Populate the manufacturer select list.
        ManufacturerSelectList = new SelectList(_2024AMSContext.Manufacturer
            .OrderBy(s => s.Manufacturer1), "ManufacturerID", "Manufacturer1");

        //// Populate the operating system select list.
        //OperatingSystemSelectList = new SelectList(_2024AMSContext.OperatingSystem
        //    .OrderBy(o => o.OperatingSystem1), "OperatingSystemID", "OperatingSystem1");

        await RetrieveRowsForDisplay();

        ViewData["Title"] = "View Asset Report";
        TempData["MessageColor"] = "Green";
        TempData["Message"] = "The assets has been filtered.";
    }

    private async Task RetrieveRowsForDisplay()
    {
        // Define the database query.
        JoinResultIQueryable = (
            from a in _2024AMSContext.Asset
            join c in _2024AMSContext.AssetCategory on a.AssetCategoryID equals c.AssetCategoryID
            join m in _2024AMSContext.Manufacturer on a.ManufacturerID equals m.ManufacturerID
            join ua in _2024AMSContext.UserAsset on a.AssetID equals ua.AssetID
            join u in _2024AMSContext.User on ua.UserID equals u.UserID
            orderby c.AssetCategory1, m.Manufacturer1, a.Asset1
            select new JoinResult
            {
                AssetID = a.AssetID,
                AssetCategoryID = c.AssetCategoryID,                
                ManufacturerID = m.ManufacturerID,
                UserID = ua.UserID,
                Asset1 = a.Asset1,
                AssetCategory1 = c.AssetCategory1,
                Manufacturer1 = m.Manufacturer1,                
                Model = a.Model,
                ServiceTag = a.ServiceTag,
                SerialNumber = a.SerialNumber,
                MACAddress = a.MACAddress,
                Barcode = a.Barcode,
                WarrantyDate = a.WarrantyDate,
                PurchaseDate = a.PurchaseDate,
                Notes = a.Notes,
                LastName = u.LastName,
                FirstName = u.FirstName
            });

        // If a filter option was selected, modify the database query.
        if (Filter != null)
        {
            JoinResultIQueryable = JoinResultIQueryable
                .Where(jr => jr.Asset1.Contains(Filter) || jr.Model.Contains(Filter) || jr.SerialNumber.Contains(Filter)
                || jr.Barcode.Contains(Filter) || jr.WarrantyDate.Equals(Filter) || jr.PurchaseDate.Equals(Filter));
        }

        if (AssetCategoryID != 0)
        {
            JoinResultIQueryable = JoinResultIQueryable
                .Where(jr => jr.AssetCategoryID == AssetCategoryID);
        }

        if (ManufacturerID != 0)
        {
            JoinResultIQueryable = JoinResultIQueryable
                .Where(jr => jr.ManufacturerID == ManufacturerID);
        }

        //if (OperatingSystemID != 0)
        //{
        //    JoinResultIQueryable = JoinResultIQueryable
        //        .Where(jr => jr.OperatingSystemID == OperatingSystemID);
        //}
        JoinResultIList = await JoinResultIQueryable.ToListAsync();
       
    }

    public async Task<IActionResult> OnPostSendAsync()
    {
        // Configure the email address and send it.
        string strToName = HttpContext.Session.GetString("FirstName") + " " +  HttpContext.Session.GetString("LastName");
        string strToAddress = HttpContext.Session.GetString("EmailAddress");
        string strSubject = "2024 Asset Report";
        string strBody = "Dear " + strToName + "," +
            "<br /><br /> Attached is the asset report generated by the asset management system. " +
            "This report contains information regarding each asset. " +
            "<br /><br />Sincerely, <br /><br />The AMS System";

        await IEmailService.SendEmail(strToName, strToAddress, strSubject, strBody);
        // Set the message.
        TempData["MessageColor"] = "Green";
        TempData["Message"] = "You have successfully sent an email to " + strToName + ".";
        return Redirect("ViewAssetReport");
    }
}