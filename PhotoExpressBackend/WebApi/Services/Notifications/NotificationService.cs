using WebApi.Services.Notifications;

public class NotificationService
{
    private readonly IMessage _emailService;

    public NotificationService(IMessage emailService)
    {
        _emailService = emailService;
    }

    public void NotifyUser(string userEmail,string subject,string body)
    {
        _emailService.SendMessage(userEmail, subject,body);
    }
}
