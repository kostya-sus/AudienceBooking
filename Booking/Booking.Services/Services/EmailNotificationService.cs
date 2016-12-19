using System;
using System.IO;
using System.Net.Mail;
using Booking.Models;
using System.Net;
using System.Security.Policy;
using System.Text;
using Booking.Services.EmailModels;
using Booking.Services.Interfaces;
using RazorEngine.Templating;

namespace Booking.Services.Services
{
    public class EmailNotificationService : IEmailNotificationService
    {
        private readonly MailAddress _emailFromAddress = new MailAddress("audiencebookingtest@gmail.com", "Audience");
        private readonly string _templateFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EmailTemplates");
        private readonly TemplateService _templateService;

        public EmailNotificationService()
        {
            _templateService = new TemplateService();
        }
        private void SendMail(MailMessage email)
        {
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential("audiencebookingtest@gmail.com", "Qwer123!"),
                EnableSsl = true
            };
            try
            {
                smtp.Send(email);
            }
            catch (Exception ex)
            {
                var smtpEx = ex as SmtpException;
                if (smtpEx != null)
                {
                    var error = smtpEx.StatusCode;
                }
            }
        }

        private MailMessage GenerateEmail(string emailHtmlBody, string subject)
        {
            return new MailMessage
            {
                From = _emailFromAddress,
                Body = emailHtmlBody,
                IsBodyHtml = true,
                Subject = subject
            };
        }
        public void AccountRegisteredNotification(ApplicationUser user)
        {
            var emailTemplatePath = Path.Combine(_templateFolderPath.Replace("Web", "Services"), "AccountRegisteredNotificationTemplate.cshtml");
            
            var modelEmail = new Booking.Services.EmailModels.AccountRegisteredRemovedNotificationModel
            {
                Name = user.UserName,
                Email = user.Email
            };

            var emailHtmlBody = _templateService.Parse(File.ReadAllText(emailTemplatePath), modelEmail, null, null);

            var subject = "Registered to softheme-booking.azurewebsites.net";
            var email = GenerateEmail(emailHtmlBody, subject);

            email.To.Add(new MailAddress(modelEmail.Email, modelEmail.Name));    
            
            SendMail(email);
        }


        public void AccountRemovedNotification(ApplicationUser user)
        {
            var emailTemplatePath = Path.Combine(_templateFolderPath.Replace("Web", "Services"), "AccountRemovedNotificationTemplate.cshtml");

            var modelEmail = new Booking.Services.EmailModels.AccountRegisteredRemovedNotificationModel
            {
                Name = user.UserName,
                Email = user.Email
            };

            var emailHtmlBody = _templateService.Parse(File.ReadAllText(emailTemplatePath), modelEmail, null, null);

            var subject = "Account remove from softheme-booking.azurewebsites.net";
            var email = GenerateEmail(emailHtmlBody, subject);

            email.To.Add(new MailAddress(modelEmail.Email, modelEmail.Name));

            SendMail(email);
        }

        public void EventCancelledNotification(Event eventEntity)
        {
            var participants = eventEntity.EventParticipants;
            var emailTemplatePath = Path.Combine(_templateFolderPath.Replace("Web", "Services"), "EventCancelledNotificationTemplate.cshtml");
            
            var subject = "Event cancelled notification";
            var modelEmail = new Booking.Services.EmailModels.EventCancellModel
            {
                EventTitle = eventEntity.Title,
                EventDateTime = eventEntity.EventDateTime.ToLongDateString()
            };

            foreach (var participant in participants)
            {
                modelEmail.Email = participant.ParticipantEmail;
                var emailHtmlBody = _templateService.Parse(File.ReadAllText(emailTemplatePath), modelEmail, null, null);
                var email = GenerateEmail(emailHtmlBody, subject);

                email.To.Add(new MailAddress(modelEmail.Email));

                SendMail(email);
            }      

        }

        public void EventCancelledAuthorNotification(Event eventEntity)
        {
            var emailTemplatePath = Path.Combine(_templateFolderPath.Replace("Web", "Services"), "EventCancelledAuthorNotificationTemplate.cshtml");
            var user = eventEntity.Author;
            var modelEmail = new Booking.Services.EmailModels.EventCancelledAuthorModel
            {
                Name = user.UserName,
                Email = user.Email,
                EventDateTime = eventEntity.EventDateTime.ToLongDateString(),
                EventTitle = eventEntity.Title
            };

            var emailHtmlBody = _templateService.Parse(File.ReadAllText(emailTemplatePath), modelEmail, null, null);

            var subject = "Cancel event notification";
            var email = GenerateEmail(emailHtmlBody, subject);

            email.To.Add(new MailAddress(modelEmail.Email, modelEmail.Name));

            SendMail(email);
        }

        public void RemovedFromParticipantsListNotification(string email, Event eventEntity)
        {
            var emailTemplatePath = Path.Combine(_templateFolderPath.Replace("Web", "Services"), "RemovedFromParticipantsListNotificationTemplate.cshtml");
            var modelEmail = new Booking.Services.EmailModels.RemovedJoinedFromParticipantsListModel
            {
                Email = email,
                EventTitle = eventEntity.Title,
                EventDate = eventEntity.EventDateTime.ToLongDateString()
            };

            var emailHtmlBody = _templateService.Parse(File.ReadAllText(emailTemplatePath), modelEmail, null, null);

            var subject = "Remove from participants list notification";
            var mailMessage = GenerateEmail(emailHtmlBody, subject);

            mailMessage.To.Add(new MailAddress(modelEmail.Email));

            SendMail(mailMessage);
        }

        public void EventJoinedNotification(string email, Event eventEntity)
        {
            var emailTemplatePath = Path.Combine(_templateFolderPath.Replace("Web", "Services"), "JoinedToParticipantsListNotificationTemplate.cshtml");
            var modelEmail = new Booking.Services.EmailModels.RemovedJoinedFromParticipantsListModel
            {
                Email = email,
                EventTitle = eventEntity.Title,
                EventDate = eventEntity.EventDateTime.ToLongDateString()
            };

            var emailHtmlBody = _templateService.Parse(File.ReadAllText(emailTemplatePath), modelEmail, null, null);

            var subject = "Joined to participants list notification";
            var mailMessage = GenerateEmail(emailHtmlBody, subject);

            mailMessage.To.Add(new MailAddress(modelEmail.Email));

            SendMail(mailMessage);
        }

        public void EventEditedNotification(Event newEvent, Event oldEvent)
        {
            var participants = oldEvent.EventParticipants;
            var emailTemplatePath = Path.Combine(_templateFolderPath.Replace("Web", "Services"), "EventEditedNotificationTemplate.cshtml");

            var subject = "Event edit notification";
            var modelEmail = new Booking.Services.EmailModels.EventEditedModel
            {
                Title = oldEvent.Title,
                OldDate = oldEvent.EventDateTime.ToLongDateString(),
                NewDate = newEvent.EventDateTime.ToLongDateString()
            };

            foreach (var participant in participants)
            {
                modelEmail.Email = participant.ParticipantEmail;
                var emailHtmlBody = _templateService.Parse(File.ReadAllText(emailTemplatePath), modelEmail, null, null);
                var email = GenerateEmail(emailHtmlBody, subject);

                email.To.Add(new MailAddress(modelEmail.Email));

                SendMail(email);
            }
        }

        public void EventEditedAuthorNotification(Event newEvent, Event oldEvent)
        {
            var emailTemplatePath = Path.Combine(_templateFolderPath.Replace("Web", "Services"), "EventEditedAuthorNotificationTemplate.cshtml");
            var user = oldEvent.Author;
            var modelEmail = new Booking.Services.EmailModels.EventEditedAuthorNotificationModel
            {
                Name = user.UserName,
                Email = user.Email,
                OldDate = oldEvent.EventDateTime.ToLongDateString(),
                NewDate = newEvent.Title
            };

            var emailHtmlBody = _templateService.Parse(File.ReadAllText(emailTemplatePath), modelEmail, null, null);

            var subject = "Edit event notification";
            var email = GenerateEmail(emailHtmlBody, subject);

            email.To.Add(new MailAddress(modelEmail.Email, modelEmail.Name));

            SendMail(email);
        }

        public void SendFeedbackToAdmins(string name, string surname, string email, string message)
        {
            var service = new UsersService();
            var adminsEmails = service.GetAdminsEmails();

            var emailTemplatePath = Path.Combine(_templateFolderPath.Replace("Web", "Services"), "SendFeedbackToAdminsNotificationTemplate.cshtml");

            var subject = "Feedback from user";
            var modelEmail = new Booking.Services.EmailModels.SendFeedbackToAdminsModel
            {
                Message = message,
                Name = name + " " + surname,
                UserEmail = email
            };

            foreach (var adminEmail in adminsEmails)
            {
                modelEmail.Email = adminEmail;
                var emailHtmlBody = _templateService.Parse(File.ReadAllText(emailTemplatePath), modelEmail, null, null);
                var mail = GenerateEmail(emailHtmlBody, subject);

                mail.To.Add(new MailAddress(modelEmail.Email));

                SendMail(mail);
            }
        }

        public void ConfirmEmailAddress(ApplicationUser user, string  emailBody)
        {
            ////var emailTemplatePath = Path.Combine(_templateFolderPath.Replace("Web", "Services"), "AccountConfirmTemplate.cshtml");

            var modelEmail = new Booking.Services.EmailModels.AccountRegisteredRemovedNotificationModel
            {
                Name = user.UserName,
                Email = user.Email
            };

            ////var emailHtmlBody = _templateService.Parse(File.ReadAllText(emailTemplatePath), modelEmail, null, null);
            
            var subject = "Confirm account for softheme-booking.azurewebsites.net";
            var email = GenerateEmail(emailBody, subject);

            email.To.Add(new MailAddress(modelEmail.Email, modelEmail.Name));

            SendMail(email);

            //MailAddress from = new MailAddress("somemail@yandex.ru", "Web Registration");
            //// кому отправляем
            //MailAddress to = new MailAddress(user.Email);
            //// создаем объект сообщения
            //MailMessage m = new MailMessage(from, to);
            //// тема письма
            //m.Subject = "Email confirmation";
            //// текст письма - включаем в него ссылку
            
            //m.IsBodyHtml = true;
            //// адрес smtp-сервера, с которого мы и будем отправлять письмо
            //SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.yandex.ru", 25);
            //// логин и пароль
            //smtp.Credentials = new System.Net.NetworkCredential("somemail@yandex.ru", "password");
            //smtp.Send(m);
        }
    }
}