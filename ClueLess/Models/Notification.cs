using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClueLess.Models
{
    public class Notification
    {
        public static void sendEmail( NotificationType notification, string addresses)
        {
            throw new NotImplementedException();
        }

        public enum NotificationType
        {
            UserNameReminder=0,
            PasswordReset=1
        }
    }
}