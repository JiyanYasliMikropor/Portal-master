using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Mikroportal.ACS.Views;
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
    public class ReplacementService : IReplacementService
    {
        IConfiguration _config;
        private readonly IHostingEnvironment _hostingEnvironment;
        public ReplacementService(IConfiguration config, IHostingEnvironment hostingEnvironment)
        {
            _config = config;
            _hostingEnvironment = hostingEnvironment;
        }
        public SerialNoCheckAndHistoryResponse SerialNoCheckAndHistory(string serialNo)
        {
            var request = new RestRequest("api/Replacement/SerialNoCheckAndHistory/" + serialNo, Method.GET, DataFormat.Json)
            .AddUrlSegment("serialNo", serialNo);

            var resp = Globals.ApiClient.Execute<SerialNoCheckAndHistoryResponse>(request);
            return resp.Data;
        }
        public GetDashboardMenuResponse GetDashboardMenu(int UserId)
        {
            var request = new RestRequest("api/Replacement/GetDashboardMenu/" + UserId, Method.GET, DataFormat.Json)
                .AddUrlSegment("UserId", UserId);

            var resp = Globals.ApiClient.Execute<GetDashboardMenuResponse>(request);
            return resp.Data;
        }

        public TblSshMachineryServicesResponse AddReplacement(ReplacementView ReplacementView)
        {
            string fileName = "";
            string uniqueFileName = null;

            if (ReplacementView.files != null && ReplacementView.files.Count > 0)
            {
                foreach (IFormFile photo in ReplacementView.files)
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


                    if (ReplacementView.files.IndexOf(photo) == ReplacementView.files.Count - 1)
                    {
                        fileName += uniqueFileName;
                    }
                    else
                    {
                        fileName += uniqueFileName + "||";
                    }
                }
            }

            ReplacementView.files = null;
            ReplacementView.FileNames = fileName;

            var request = new RestRequest("api/Replacement/AddReplacement/", Method.POST, DataFormat.Json)
        .AddJsonBody(ReplacementView);

            var resp = Globals.ApiClient.Execute<TblSshMachineryServicesResponse>(request);
            return resp.Data;
        }
    }
}
