using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Mikroportal.MODEL.RequestResponseClasses
{
    public class Tools
    {
        public string SendMail(string toMail, string body, string subject)//SendMail("wee@sd.com;sdf@dfsdf.com","wee@sd.com;sdf@dfsdf.com")
        {
            string sonuc = "";
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("mes-admin@mikropor.com.tr", "Oz1976*er");
            client.Host = "mail.mikropor.com.tr";
            client.Port = 587;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.Timeout = 600000;
            client.EnableSsl = true;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("mes-admin@mikropor.com.tr");

            mailMessage.To.Add(toMail);



            mailMessage.IsBodyHtml = true;
            mailMessage.Body = body;


            mailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html));
            //mailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString("This is some plain text2", null, MediaTypeNames.Text.Plain));
            mailMessage.Subject = subject;

            try
            {
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                sonuc = ex.ToString();
            }
            return sonuc;
        }

    }
}
