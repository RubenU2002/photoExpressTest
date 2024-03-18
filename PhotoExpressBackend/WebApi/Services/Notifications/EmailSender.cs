using System.Net;
using System.Net.Mail;
using WebApi.Services.Notifications;

public class EmailSender : IMessage
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _originEmail;
    private readonly string _password;

    public EmailSender(string smtpServer, int smtpPort, string originEmail, string password)
    {
        _smtpServer = smtpServer;
        _smtpPort = smtpPort;
        _originEmail = originEmail;
        _password = password;
    }
    public void SendMessage(string destination, string subject, string body)
    {
        MailMessage message = new MailMessage(_originEmail, destination, subject, body);
        message.IsBodyHtml = true;
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
        smtpClient.EnableSsl = true;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Port = 587;
        smtpClient.Credentials = new System.Net.NetworkCredential(_originEmail, _password);
        smtpClient.Send(message);
        smtpClient.Dispose();
    }
}
