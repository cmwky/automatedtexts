using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace AutomatedTexts
{
    public class Function1
    {
        [FunctionName("SendScheduledMessages")]
        public void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            string accountSid = System.Environment.GetEnvironmentVariable("TwilioAccountSid");
            string authToken = System.Environment.GetEnvironmentVariable("TwilioAuthToken");
            string sender = System.Environment.GetEnvironmentVariable("TwilioSender");
            string recipient = System.Environment.GetEnvironmentVariable("TwilioRecipient");

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "May the Force be with you, Always.",
                from: new Twilio.Types.PhoneNumber(sender),
                to: new Twilio.Types.PhoneNumber(recipient)
            );

            Console.WriteLine(message.Sid);
            log.LogInformation($"Azure function executed at: {DateTime.Now}");
        }
    }
}
