using System.Net.Mail;
using System.Net;
using HiddenVillaServer;
using HiddenVilla_Client.Model;
using Microsoft.AspNetCore.Identity;
namespace HiddenVilla_Web_Api.Helper
{
    public static class EmailSender
    {
        public static void Sender(RoomOrderDetails obj)
        {
            try
            {
                // Credentials
                var credentials = new NetworkCredential(SD.AdminEmail, SD.AdminPassword);
                // Mail message
                var mail = new MailMessage()
                {
                    From = new MailAddress(SD.AdminEmail),
                    Subject = " Booking Payment Successfully Confirmed",
                    Body = $" Dear Mr {obj.Email.ToString().Split("@")[0]} Your Room is {obj.hotelRoomDTO.Name} has been succesfully paid for with. Be Sure To Meet You soon"
                };

                mail.To.Add(new MailAddress(obj.Email));

                // Smtp client
                var client = new SmtpClient()
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Credentials = credentials
                };
                var meth = "hey";
                // Send it...         
                client.Send(mail);
                Console.WriteLine($"Email for {obj.Email.ToString().Split("@")[0]} sccessfully sent to {obj.Email}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in sending email: " + ex.Message);
            }
           
            // Console.ReadKey();
        }
    }
}
