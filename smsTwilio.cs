

namespace Millionaire
{
    using System;
    using System.Threading.Tasks;
    using Twilio;
    using Twilio.Rest.Api.V2010.Account;

    internal class smsTwilio
    {
        //ToDo: Receive SMS messages
        public void PublicSendSms(ref int pStatus, string pFrom, string pTo, string pBodyText)
        {
            SendSms(pFrom, pTo, pBodyText).Wait();
        }

        private static async Task SendTestSms()
        {
            // Find your Account Sid and Token at twilio.com/console
            // Environment Variables (Windows): System (right click windows, settings) -> Advanced System Settings -> Environment Variables
            // Environment Variables (Linux): https://linuxize.com/post/how-to-set-and-list-environment-variables-in-linux/
            string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
            string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");

            TwilioClient.Init(accountSid, authToken);

            MessageResource message = await MessageResource.CreateAsync(
                body: "This is the ship that made the Kessel Run in fourteen parsecs?",
                from: new Twilio.Types.PhoneNumber("+12029461187"),
                  to: new Twilio.Types.PhoneNumber("+14253459249")
            );

            Console.WriteLine(message.Sid);
        }

        private static async Task SendSms(string pFrom, string pTo, string pBodyText)
        {
            // Find your Account Sid and Token at twilio.com/console
            string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
            string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");

            TwilioClient.Init(accountSid, authToken);

            MessageResource message = await MessageResource.CreateAsync(
                body: pBodyText,
                from: new Twilio.Types.PhoneNumber("+1" + pFrom),
                  to: new Twilio.Types.PhoneNumber("+1" + pTo)
            );

            Console.WriteLine(message.Sid);
        }
    }
}
