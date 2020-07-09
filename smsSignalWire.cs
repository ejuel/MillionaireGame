namespace Millionaire
{
    using SignalWire.Relay;
    using SignalWire.Relay.Messaging;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Twilio;
    using Twilio.Rest.Api.V2010.Account;


    internal class smsSignalWire
    {
        //Resources: https://docs.signalwire.com/topics/relay-sdk-dotnet/v2/#relay-sdk-for-net-using-the-sdk-relay-consumer

        //ToDo: Receive SMS messages

        internal class IncomingMessageConsumer : Consumer
        {
            protected override void Setup()
            {
                Project = Environment.GetEnvironmentVariable("SIGNALWIRE_PROJECT_ID");
                Token = Environment.GetEnvironmentVariable("SIGNALWIRE_AUTH_TOKEN");
                Contexts = new List<string> { "test" };
            }

            protected override void OnIncomingMessage(Message message)
            {
                if (message.Body?.StartsWith("Hello") == true)
                {
                    // ...
                    Console.WriteLine("Received 'Hello...'");
                }
                else
                {
                    Console.WriteLine("Received: " + message.Body);
                }
            }
        }

        internal class SignalWireIncoming
        {
            public static void Incoming()
            {
                new IncomingMessageConsumer().Run();
            }
        }



        public void PublicSendSms(ref int pStatus, string pFrom, string pTo, string pBodyText)
        {
            SendSms(pFrom, pTo, pBodyText).Wait();
        }
        public void testSms()
        {
            SendTestSms().Wait();
        }

        private static async Task SendTestSms()
        {
            // Find your Account Sid and Token at twilio.com/console
            // Environment Variables (Windows): System (right click windows, settings) -> Advanced System Settings -> Environment Variables
            // Environment Variables (Linux): https://linuxize.com/post/how-to-set-and-list-environment-variables-in-linux/
            string projectId = Environment.GetEnvironmentVariable("SIGNALWIRE_PROJECT_ID");
            string authToken = Environment.GetEnvironmentVariable("SIGNALWIRE_AUTH_TOKEN");
            string signalwireSpaceUrl = Environment.GetEnvironmentVariable("SIGNALWIRE_DOMAIN");

            if (projectId is null || authToken is null || signalwireSpaceUrl is null)
            {
                Console.WriteLine("SignalWire: Environment Variables are missing");
            }

            TwilioClient.Init(projectId, authToken, new Dictionary<string, object> { ["signalwireSpaceUrl"] = signalwireSpaceUrl });

            MessageResource message = await MessageResource.CreateAsync(
                body: "Test SignalWire?",
                from: new Twilio.Types.PhoneNumber("+17152548445"),
                  to: new Twilio.Types.PhoneNumber("+14253459249")
            );

            Console.WriteLine(message.Sid);
        }

        private static async Task SendSms(string pFrom, string pTo, string pBodyText)
        {
            // Find your Account Sid and Token at twilio.com/console
            string projectId = Environment.GetEnvironmentVariable("SIGNALWIRE_PROJECT_ID");
            string authToken = Environment.GetEnvironmentVariable("SIGNALWIRE_AUTH_TOKEN");
            string signalwireSpaceUrl = Environment.GetEnvironmentVariable("SIGNALWIRE_DOMAIN");

            TwilioClient.Init(projectId, authToken, new Dictionary<string, object> { ["signalwireSpaceUrl"] = signalwireSpaceUrl });

            MessageResource message = await MessageResource.CreateAsync(
                body: pBodyText,
                from: new Twilio.Types.PhoneNumber("+1" + pFrom),
                  to: new Twilio.Types.PhoneNumber("+1" + pTo)
            );

            Console.WriteLine(message.Sid);
        }
    }
}
