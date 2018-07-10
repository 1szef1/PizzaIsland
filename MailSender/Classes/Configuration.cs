using MailSender.Model;
using MailSender.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Classes
{
    public class Configuration
    {
        public static bool Exist()
        {
            return File.Exists(MSContext.DbName);
        }

        public static void Configure(SenderInfo senderInfo, string password)
        {
            new SenderService().SaveConfiguration(senderInfo, password);
        }

        public static SenderInfo Get()
        {
            return new SenderService().LoadConfiguration();
        }
    }
}
