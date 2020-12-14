using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using Mikroportal.MODEL.ServiceContracts.ACS;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mikroportal.ACS.Services
{
    public class RepairAndMaintenanceService: IRepairAndMaintenanceService
    {
        IConfiguration _config;
        private readonly IHostingEnvironment _hostingEnvironment;
        public RepairAndMaintenanceService(IConfiguration config, IHostingEnvironment hostingEnvironment)
        {
            _config = config;
            _hostingEnvironment = hostingEnvironment;
        }

        public GetDashboardMenuResponse GetDashboardMenu(int UserId)
        {
            var request = new RestRequest("api/RepairAndMaintenance/GetDashboardMenu/" + UserId, Method.GET, DataFormat.Json)
                .AddUrlSegment("UserId", UserId);

            var resp = Globals.ApiClient.Execute<GetDashboardMenuResponse>(request);
            return resp.Data;
        }
        public SerialNoCheckAndHistoryResponse SerialNoCheckAndHistory(string serialNo)
        {
            var request = new RestRequest("api/RepairAndMaintenance/SerialNoCheckAndHistory/" + serialNo, Method.GET, DataFormat.Json)
            .AddUrlSegment("serialNo", serialNo);

            var resp = Globals.ApiClient.Execute<SerialNoCheckAndHistoryResponse>(request);
            return resp.Data;
        }

        public TblSshMachineryServicesResponse AddRepairAndMaintenance(RepairAndMaintenanceView RepairAndMaintenanceView)
        {
            string fileName = "";
            string uniqueFileName = null;

            if (RepairAndMaintenanceView.files != null && RepairAndMaintenanceView.files.Count > 0)
            {
                foreach (IFormFile photo in RepairAndMaintenanceView.files)
                {
                    string[] paths = { @"C:\", "Files", "ACS", "Services" };
                    string uploadsFolder = Path.Combine(paths);
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    if (photo.ContentType == "image/jpeg")
                    {
                        Image image = Image.FromStream(photo.OpenReadStream(), true, true);
                        int origWidth = image.Width;
                        int origHeight = image.Height;
                        int sngRatio = origWidth / origHeight;

                        int bannerWidth = origWidth / 2;
                        int bannerHeight = origHeight / 2;


                        if (image.Width > 1024)
                        {
                            Bitmap bannerBMP = new Bitmap(image, bannerWidth, bannerHeight);
                            Graphics oGraphics = Graphics.FromImage(bannerBMP);

                            oGraphics = Graphics.FromImage(bannerBMP);
                            // Set the properties for the new graphic file
                            oGraphics.SmoothingMode = SmoothingMode.AntiAlias; oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                            // Draw the new graphic based on the resized bitmap
                            oGraphics.DrawImage(image, 0, 0, bannerWidth, bannerHeight);
                            // Save the new graphic file to the server
                            bannerBMP.Save(uploadsFolder + "/" + uniqueFileName);

                            bannerBMP.Dispose();
                            oGraphics.Dispose();
                        }
                        else
                        {
                            image.Save(uploadsFolder + "/" + uniqueFileName);
                        }


                    }
                    else
                    {
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }


                    if (RepairAndMaintenanceView.files.IndexOf(photo) == RepairAndMaintenanceView.files.Count - 1)
                    {
                        fileName += uniqueFileName;
                    }
                    else
                    {
                        fileName += uniqueFileName + "||";
                    }
                }
            }
            RepairAndMaintenanceView.files = null;
            RepairAndMaintenanceView.FileNames = fileName;

            var request = new RestRequest("api/RepairAndMaintenance/AddRepairAndMaintenance/", Method.POST, DataFormat.Json)
        .AddJsonBody(RepairAndMaintenanceView);

            var resp = Globals.ApiClient.Execute<TblSshMachineryServicesResponse>(request);
            return resp.Data;
        }
    }
}
