using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using Mikroportal.MODEL.RequestResponseClasses.ACS.Admin;
using Mikroportal.MODEL.ServiceContracts.ACS.Admin;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mikroportal.ACS.Services.Admin
{
    public class AdminCallCenterService : IAdminCallCenterService
    {
        IConfiguration _config;
        private readonly IHostingEnvironment _hostingEnvironment;
        public AdminCallCenterService(IConfiguration config, IHostingEnvironment hostingEnvironment)
        {
            _config = config;
            _hostingEnvironment = hostingEnvironment;
        }

        public GetCallCenterListResponse GetCallCenterList(CallCenterFilter CallCenterRequest)
        {
            var request = new RestRequest("api/AdminCallCenter/GetMachineHistoryList", Method.POST, DataFormat.Json)
               .AddJsonBody(CallCenterRequest);

            var resp = Globals.ApiClient.Execute<GetCallCenterListResponse>(request);
            return resp.Data;
        }

        public InboxShowByIdResponse InboxShowById(string machineryServiceId)
        {
            var request = new RestRequest("api/AdminCallCenter/InboxShowById/" + machineryServiceId, Method.GET, DataFormat.Json)
            .AddUrlSegment("machineryServiceId", machineryServiceId);

            var resp = Globals.ApiClient.Execute<InboxShowByIdResponse>(request);
            return resp.Data;
        }

        public GetDashboardMenuResponse GetACSAdminMenu(int UserId)
        {
            var request = new RestRequest("api/AdminHome/GetACSAdminMenu/" + UserId, Method.GET, DataFormat.Json)
                .AddUrlSegment("UserId", UserId);

            var resp = Globals.ApiClient.Execute<GetDashboardMenuResponse>(request);
            return resp.Data;
        }



        public AddFileUploadCallCenterResponse AddFileUploadCallCenter(FileView FileViewRequest)
        {
            string fileName = "";
            string uniqueFileName = null;
            if (FileViewRequest.files != null && FileViewRequest.files.Count > 0)
            {

                foreach (IFormFile photo in FileViewRequest.files)
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


                    if (FileViewRequest.files.IndexOf(photo) == FileViewRequest.files.Count - 1)
                    {
                        fileName += uniqueFileName;
                    }
                    else
                    {
                        fileName += uniqueFileName + "||";
                    }
                }

            }
            FileViewRequest.files = null;

            FileViewRequest.FileNames = fileName;

            var request = new RestRequest("api/AdminCallCenter/AddFileUploadCallCenter", Method.POST, DataFormat.Json)
                 .AddJsonBody(FileViewRequest);

            var resp = Globals.ApiClient.Execute<AddFileUploadCallCenterResponse>(request);
            return resp.Data;
        }

    }
}
