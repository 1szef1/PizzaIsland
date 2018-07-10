using MailSender.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Services
{
    internal class SenderService
    {
        public SenderInfo LoadConfiguration()
        {
            using (MSContext context = new MSContext())
            {
                return context.SenderInfos.FirstOrDefault();
            }
        }

        public void SaveConfiguration(SenderInfo senderInfo, string password)
        {
            using (MSContext context = new MSContext())
            {
                var si = context.SenderInfos.FirstOrDefault();
                if (si == null)
                {
                    si = context.SenderInfos.Create();
                    context.SenderInfos.Add(si);
                }
                si.FromEmail = senderInfo.FromEmail;
                si.DisplayName = senderInfo.DisplayName;
                si.User = senderInfo.User;
                si.SetPassword(password);
                si.Smtp = senderInfo.Smtp;
                si.Port = senderInfo.Port;
                si.SecureConnection = senderInfo.SecureConnection;

                context.SaveChanges();
            }
        }
    }
}
