using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using System.Net;
using System.ComponentModel;
using MailSender.Extensions;
using System.Security;

namespace MailSender.Model
{
    [Table("SenderInfo")]
    public class SenderInfo
    {
        [Key]
        [Column("Id", TypeName = "INTEGER")]
        public int Id { get; set; }

        [Column("FromEmail", TypeName = "VARCHAR")]
        [MaxLength(50)]
        [Required]
        public string FromEmail { get; set; }

        [Column("DisplayName", TypeName = "VARCHAR")]
        [MaxLength(50)]
        public string DisplayName { get; set; }

        [Column("User", TypeName = "VARCHAR")]
        [MaxLength(50)]
        [Required]
        public string User { get; set; }

        [Column("Password", TypeName = "VARCHAR")]
        [Required]
        public string Password { get; private set; }

        [Column("Smtp", TypeName = "VARCHAR")]
        [MaxLength(50)]
        [Required]
        public string Smtp { get; set; }

        public int Port { get; set; }

        public bool SecureConnection { get; set; }

        public void SetPassword(string text)
        {
            Password = text.Encode();
        }
    }
}
