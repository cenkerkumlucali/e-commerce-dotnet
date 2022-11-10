using System.Net;
using System.Net.Mail;
using System.Text;
using ECommerce.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Infrastructure.Services;

public class MailService : IMailService
{
    private readonly IConfiguration _configuration;

    public MailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
    {
        await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
    }

    public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
    {
        try
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                foreach (string to in tos) mailMessage.To.Add(to);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.From = new MailAddress(_configuration["Mail:Username"], "CK E-Ticaret",
                    System.Text.Encoding.UTF8);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = isBodyHtml;

                using (SmtpClient smtp = new SmtpClient(_configuration["Mail:Host"],
                           Convert.ToInt32(_configuration["Mail:Port"])))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials =
                        new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mailMessage);
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
    {
        StringBuilder mail = new();
        mail.AppendLine(
            "Merhaba<br>Eğer yeni şifre talebinde bulunduysanız aşağıdaki linkten şifrenizi yenileyebilirsiniz.<br><strong><a target=\"_blank\" href=\"");
        mail.AppendLine(_configuration["AngularClientUrl"]);
        mail.AppendLine("/update-password/");
        mail.AppendLine(userId);
        mail.AppendLine("/");
        mail.AppendLine(resetToken);
        mail.AppendLine(
            "\">Yeni şifre talebi için tıklayınız...</a></strong><br><br><span style=\"font-size:12px;\">NOT : Eğer ki bu talep tarafınızca gerçekleştirilmemişse lütfen bu maili ciddiye almayınız.</span><br>Saygılarımızla...<br><br><br>NG - Mini|E-Ticaret");
        await SendMailAsync(to, "Şifre Yenileme Talebi", mail.ToString(), true);
    }

    public async Task SendCompletedOrderMailAsync(string to, string userName, string orderCode,
        DateTime orderDate)
    {
        string mail = $"Sayın {userName} Merhaba<br>" +
                      $"{orderDate} tarihinde vermiş olduğunuz {orderCode} kodlu siparişiniz tamamlanmış ve kargo firmasına verilmiştir.<br>" +
                      $"Hayırlı günler...";
        await SendMailAsync(to, $"{orderCode} Sipariş numaralı Siparişiniz Tamamlandı", mail);
    }
}