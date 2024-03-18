namespace WebApi.Services.Notifications
{
    public interface IMessage
    {
        void SendMessage(string to, string subject, string body);
    }
}
