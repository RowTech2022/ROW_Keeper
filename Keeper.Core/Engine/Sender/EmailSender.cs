using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;

namespace Keeper.Core
{
    public class EmailSender : ISender
    {
        SenderSetting m_settings;
        FileEngine m_fileEngine;

        public EmailSender(SenderSetting settings, FileEngine fileEngine)
        {
            m_settings = settings;
            m_fileEngine = fileEngine;
        }

        public async Task SendRequest(MailRequest request, bool corruption = false) //MailMessage
        {
            using (var emailMessage = new MimeMessage())
            {
                emailMessage.From.Add(new MailboxAddress(m_settings.From.DisplayName, m_settings.From.Address));

                if (corruption)
                    emailMessage.To.Add(new MailboxAddress(m_settings.ToCoruption.DisplayName, m_settings.ToCoruption.Address));
                else
                    emailMessage.To.Add(new MailboxAddress(m_settings.ToRequest.DisplayName, m_settings.ToRequest.Address));

                emailMessage.Subject = String.Format(m_settings.ToTemplate, request.ByParams);

                var multipart = new Multipart("mixed");
                var textPart = new TextPart("plain") { Text = request.Body };
                multipart.Add(textPart);
                try
                {
                    if (request.Files != null)
                    {
                        foreach (var file in request.Files)
                        {
                            var filePath = m_fileEngine.GetFilePathOrigin(file);
                            var attachment = new MimePart("application", "octet-stream")
                            {
                                ContentDisposition = new MimeKit.ContentDisposition(ContentDisposition.Attachment),
                                ContentTransferEncoding = ContentEncoding.Base64,
                                FileName = System.IO.Path.GetFileName(filePath)
                            };
                            attachment.Content = new MimeContent(System.IO.File.OpenRead(filePath));

                            multipart.Add(attachment);

                        }
                    }

                    emailMessage.Body = multipart;

                    using (var client = new SmtpClient())
                    {
                        await client.ConnectAsync(m_settings.SmtpAddress, m_settings.Port, false);
                        await client.AuthenticateAsync(m_settings.From.Address, m_settings.FromPassword);
                        var res = await client.SendAsync(emailMessage);

                        await client.DisconnectAsync(true);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Ошибка при отправке сообщение {ex.Message}");
                }
            }
        }

        public class SenderSetting
        {
            public string? SmtpAddress { get; set; }
            public int Port { get; set; }
            public bool Ssl { get; set; }

            public MailAddress? From { get; set; }
            public string? FromPassword { get; set; }
            public MailAddress? ToRequest { get; set; }
            public string? ToTemplate { get; set; }
            public MailAddress? ToCoruption { get; set; }
            public string? ToCoruptionTemplate { get; set; }

            public SenderSetting() { }
            public SenderSetting Load(List<IConfigurationSection>? allValues)
            {
                var smtp = allValues?.FirstOrDefault(x => x.Key == "Sender.Smtp.Address")?.Value;
                if (string.IsNullOrWhiteSpace(smtp))
                    return this;

                SmtpAddress = smtp;
                var port = allValues?.FirstOrDefault(x => x.Key == "Sender.Smtp.Port")?.Value;
                Port = int.Parse(port);

                var ssl = allValues?.FirstOrDefault(x => x.Key == "Sender.Smtp.Ssl")?.Value;
                Ssl = bool.Parse(ssl);

                var fromMail = allValues?.FirstOrDefault(x => x.Key == "Sender.From.Mail")?.Value;
                var fromName = allValues?.FirstOrDefault(x => x.Key == "Sender.From.Name")?.Value;
                if (fromMail == null)
                    return this;

                FromPassword = allValues?.FirstOrDefault(x => x.Key == "Sender.From.Password")?.Value;

                From = new MailAddress(fromMail, fromName ?? fromMail);

                var toMain = allValues?.FirstOrDefault(x => x.Key == "Sender.To.Mail")?.Value;
                var toName = allValues?.FirstOrDefault(x => x.Key == "Sender.To.Name")?.Value;
                if (toMain != null)
                    ToRequest = new MailAddress(toMain, toName ?? toMain);

                var toCorupotionMain = allValues?.FirstOrDefault(x => x.Key == "Sender.ToCoruption.Mail")?.Value;
                var toCorupotionName = allValues?.FirstOrDefault(x => x.Key == "Sender.ToCoruption.Name")?.Value;
                if (toCorupotionMain != null)
                    ToCoruption = new MailAddress(toCorupotionMain, toCorupotionName ?? toCorupotionMain);

                ToTemplate = allValues?.FirstOrDefault(x => x.Key == "Sender.To.TitleTemplate")?.Value;
                ToCoruptionTemplate = allValues?.FirstOrDefault(x => x.Key == "Sender.ToCoruption.TitleTemplate")?.Value;

                return this;
            }

            public bool NotAvailableForSend(bool coruption = false)
            {
                return String.IsNullOrWhiteSpace(SmtpAddress) ||
                    (coruption ? ToCoruption == null : ToRequest == null);
            }

        }
        public class MailAddress
        {
            public MailAddress(string address, string name)
            {
                Address = address;
                DisplayName = name;
            }
            public string Address { get; set; }
            public string DisplayName { get; set; }
        }
    }

}
