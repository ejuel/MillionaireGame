// <copyright file="Program.cs" company="ejuel.net">
//     ejuel.net All rights reserved.
// </copyright>
// <author>Me</author>

namespace Millionaire
{
    using MillionaireTerminal;
    using MillionaireSms;
    using System;

    internal class Program
    {
        private static void Main(string[] args)
        {
            ProgramSms objSms = new ProgramSms();
            int status = 0;
            //objSms.PublicSendSms(ref status, "2029461187", "4253459249", "test message");
            objSms.PublicReadSms();
            //objSms.PublicTestSms();
            objSms.PublicRetrieveAllSms();
            
            Console.WriteLine("waiting");
            Console.ReadKey();
            if (status != 0)
            {
                Console.WriteLine("could not send message due status error: {0}", status);
                throw new System.Exception("Error with SMS send");
            }
            ProgramTerminal objTerminal = new ProgramTerminal();
            objTerminal.TerminalMain();
        }
    }
}