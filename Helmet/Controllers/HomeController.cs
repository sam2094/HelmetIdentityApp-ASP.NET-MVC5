using DataAccess.Database;
using Models;
using Models.Enums;
using Models.ViewModels;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;
using ZXing;
using ZXing.QrCode;
using System.Net.Mail;
using System.Net;
using Common.ConfigManager.Helpers;

namespace Helmet.Controllers
{
    public class HomeController : Controller
    {
        DataContext context = new DataContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Request()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Request(UserViewModel request)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.Name = request.Name.Trim();
                user.Surname = request.Surname.Trim();
                user.Email = request.Email.Trim();
                user.Phone = request.Phone.Trim();
                user.UserType = (byte)UserType.Client;
                user.AddedDate = DateTime.Now;

                string qrText = string.Join("\n", "Name: " + user.Name, "Surname: " + user.Surname,
                    "Phone number: " + user.Phone, "Email: " + user.Email, "Request date: " + user.AddedDate);

                var options = new QrCodeEncodingOptions
                {
                    DisableECI = true,
                    CharacterSet = "UTF-8",
                    Width = 200,
                    Height = 200,
                };

                var QCwriter = new BarcodeWriter();
                QCwriter.Format = BarcodeFormat.QR_CODE;
                QCwriter.Options = options;
                var result = QCwriter.Write(qrText);
                var barcodeBitmap = new Bitmap(result);
                byte[] bytes = null;
                string photoName = string.Concat(Guid.NewGuid().ToString("N"), ".png");

                using (MemoryStream memory = new MemoryStream())
                {
                    barcodeBitmap.Save(memory, ImageFormat.Png);
                    bytes = memory.ToArray();
                }

                try
                {
                    System.IO.File.WriteAllBytes(Path.Combine(ConfigHelper.GetAppSetting("Files"), photoName), bytes);
                }
                catch (Exception ex)
                {
                    return View(new UserViewModelResult { Message = "Unhandled exception", UserViewModel = request });
                }

                user.QRCode = photoName;

                try
                {
                    MailAddress sender = new MailAddress(ConfigHelper.GetAppSetting("smtpUser"));
                    SmtpClient smtp = new SmtpClient(ConfigHelper.GetAppSetting("smtpServer"), Convert.ToInt32(ConfigHelper.GetAppSetting("smtpPort")));
                    smtp.Credentials = new NetworkCredential(ConfigHelper.GetAppSetting("smtpUser"), ConfigHelper.GetAppSetting("smtpPass"));
                    smtp.EnableSsl = Convert.ToBoolean(ConfigHelper.GetAppSetting("EnableSsl"));
                    MailAddress addressee = new MailAddress(user.Email);
                    MailMessage message = new MailMessage(sender, addressee);
                    message.Subject = "Тест";
                    Attachment attachment = new Attachment(Path.Combine(ConfigHelper.GetAppSetting("Files"), photoName));
                    message.Attachments.Add(attachment);
                    smtp.Send(message);
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return View(new UserViewModelResult { Message = "Your e-mail is incorrect", UserViewModel = request });
                }

                context.Users.Add(user);
                context.SaveChanges();
                context.Dispose();
                return View(new UserViewModelResult { Message = "Your request sended successfully", UserViewModel = request });
            }
            return View(new UserViewModelResult { Message = "Please, check your inputs", UserViewModel = request });
        }
    }
}