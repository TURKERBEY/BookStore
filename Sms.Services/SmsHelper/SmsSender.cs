 

namespace Sms.Services.SmsHelper;


public class SmsSender : ISmsSender
{
    private readonly IMessageQueueProducer _queueProducer;

    public SmsSender(IMessageQueueProducer queueProducer)
    {
        _queueProducer = queueProducer;
    }

    public async Task<bool> SendSmsAsync(string phoneNumber, string message)
    {
        // SMS mesajını kuyruk sistemine gönderiyoruz (Kafka/RabbitMQ)
        var smsPayload = new SmsMessage
        {
            PhoneNumber = phoneNumber,
            Message = message,
            SentAt = DateTime.UtcNow
        };

        bool enqueued = await _queueProducer.EnqueueAsync(smsPayload);

        return enqueued;
    }
}

// SMS mesaj modeli
public class SmsMessage
{
    public string PhoneNumber { get; set; } = default!;
    public string Message { get; set; } = default!;
    public DateTime SentAt { get; set; }
}

// Kuyruk üretici arayüzü (Kafka, RabbitMQ vs. için)
public interface IMessageQueueProducer
{
    Task<bool> EnqueueAsync<T>(T message);
}
