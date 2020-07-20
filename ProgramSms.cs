// <copyright file="ProgramTerminal.cs" company="ejuel.net">
//     ejuel.net All rights reserved.
// </copyright>
// <author>Zeke</author>

namespace MillionaireSms
{
    using System;
    using System.Linq;
    using SmsSignalWire;
    internal class ProgramSms
    {
        private readonly int errorFrom = -1;
        private readonly int errorTo = -2;
        private readonly int errorBodyText = -3;

        public void PublicTestSms()
        {

            smsSignalWire objSignalWire = new smsSignalWire();
            objSignalWire.testSms();
            Console.ReadKey();
        }

        public void PublicReadAllSms(){
            smsSignalWire objSignalWire = new smsSignalWire();
            objSignalWire.RetrieveAllSms();
            Console.ReadKey();
        }

        public void PublicSendSms(ref int pStatus, string pFrom, string pTo, string pBodyText)
        {
            if (pFrom.Length != 10 || !pFrom.All(char.IsDigit))
            {
                Console.WriteLine("ERROR: pFrom must be a US phone number of length 10");
                pStatus = errorFrom;
                return;
            }
            else if (pTo.Length != 10 || !pTo.All(char.IsDigit))
            {
                Console.WriteLine("ERROR: pTo must be a US phone number of length 10");
                pStatus = errorTo;
                return;
            }
            else if (pBodyText.Length <= 1)
            {
                Console.WriteLine("ERROR: pBodyText must be longer than 1 character");
                pStatus = errorBodyText;
                return;
            }

            //smsTwilio objTwilio = new smsTwilio();
            //objTwilio.PublicSendSms(ref pStatus, pFrom, pTo, pBodyText);
            smsSignalWire objSignalWire = new smsSignalWire();
            objSignalWire.PublicSendSms(ref pStatus, pFrom, pTo, pBodyText);
            Console.ReadKey();
        }
        public void PublicReadSms()
        {
            smsSignalWire.SignalWireIncoming objSignalWire = new smsSignalWire.SignalWireIncoming();
        }


    }
}
