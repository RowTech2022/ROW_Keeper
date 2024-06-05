//using RowAuth.Client;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Mail;
//using System.Text;

//namespace Auth.Core
//{
//	public class EmailStrategy : ISendStrategy
//	{

//		//static string smtpAddress = "smtp.gmail.com";
//		//static int portNumber = 587;
//		//static bool enableSSL = true;
//		//static string emailFromAddress = "zsulaymon04@gmail.com"; //Sender Email Address  
//		//static string password = "Sulik0704"; //Sender Password  

//		Setting m_setting;

//		public EmailStrategy(Setting setting)
//		{
//			m_setting = setting;
//		}

		
//		public void SendCode(UserLocal.UserCode request, string Code, string UserName)
//		{
//			var TextCode = $"Уважаемый(ая) {UserName},<p>Предоставляем Вам авторизационный код для входа в ИС ЭДО:</p> <p><b>{Code}";
//			using MailMessage mail = new MailMessage();
//			try
//			{
//				mail.From = new MailAddress(m_setting.EmailFromAddress);
//				mail.To.Add(request.PhoneOrMail);
//				mail.Subject = m_setting.Subject;
//				mail.Body = TextCode;
//				mail.IsBodyHtml = true;

//				using (SmtpClient smtp = new SmtpClient(m_setting.SmtpAddress))
//				{
//					smtp.Credentials = new NetworkCredential(m_setting.EmailFromAddress, m_setting.Password);
//					smtp.EnableSsl = m_setting.EnabledSsl;
//					smtp.Send(mail);
//				}
//				Console.WriteLine("Succes");
//			}
//			catch (Exception ex)
//			{
//				Console.WriteLine($"Cant send message \n{ex}");
//			}
//		}

//        public void SendCode(User.UserCode request, string Code, string UserName)
//        {
//            throw new NotImplementedException();
//        }

//        public class Setting
//		 {
//			public string SmtpAddress { get; set; }
// 			public int Port { get; set; }
//			public bool EnabledSsl { get; set; }
//			public string EmailFromAddress { get; set; }
//			public string Password { get; set; }
//			public string Subject { get; set; }

//			public Setting Load(IConfiguration configuration) 	
//			{
//				var allValues = configuration.GetSection("ProjSettings").GetChildren();

//				SmtpAddress = allValues.FirstOrDefault(config => config.Key == "smtpAddress").Value;
//				Port = int.Parse(allValues.FirstOrDefault(config => config.Key == "portNumber").Value);
//				EnabledSsl = bool.Parse(allValues.FirstOrDefault(config => config.Key == "enableSSL").Value);
//				EmailFromAddress =  allValues.FirstOrDefault(config => config.Key == "emailFromAddress").Value;
//				Password = allValues.FirstOrDefault(config => config.Key == "password").Value;
//				Subject = allValues.FirstOrDefault(config => config.Key == "subject").Value;
//				return this;
//			}
//		}
//	}
//}
