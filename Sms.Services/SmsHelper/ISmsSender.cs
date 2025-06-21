 

namespace Sms.Services.SmsHelper;
public interface ISmsSender
{
    Task<bool> SendSmsAsync(string phoneNumber, string message);
}