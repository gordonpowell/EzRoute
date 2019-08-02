using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace EzRoute
{
    public class EmailUtility
    {

        public static void sendMail(String Toemail, String body, String subjectline = "New Account")
        {
            // Command line argument must the the SMTP host.
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.UseDefaultCredentials = true;
            client.Credentials = new System.Net.NetworkCredential("easyroute100@gmail.com", "vkyjgfincyodxzcq");

            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            MailAddress from = new MailAddress("easyroute100@gmail.com");
            MailAddress to = new MailAddress(Toemail);
            MailMessage message = new MailMessage(from, to);
            message.Subject = subjectline;
            message.Body = body;

            message.BodyEncoding = System.Text.Encoding.UTF8;
            client.Send(message);

            message.Dispose();
           
        }
    }
}