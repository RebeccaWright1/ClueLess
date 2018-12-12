using ClueLess.Database;
using ClueLess.Database.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ClueLess.Models.Notification;

namespace ClueLess.Models
{
    public class Account
    {
        public Account()
        {

        }

        /// <summary>
        /// This methods allow for the creation and updating of the user's account
        /// on ClueLess with the exception of setting the password for the user
        /// </summary>
        /// <param name="updatedAccount">The account information for a user's account</param>
        public static void SetAccount(Account updatedAccount) {
            try
            {
                User useraccount = new User();
                //Step 1. Connect to the database
                ClueLessContext db = new ClueLessContext();
                //Step 2. Pull the user from the database based on the user's ID
                if (updatedAccount != null && updatedAccount.UserID > 0)
                {
                    useraccount = db.Users.Where(user => user.ID == updatedAccount.UserID).FirstOrDefault();
                }
                
                else //Step 3. If the user is null, create a new user
                {
                    useraccount = new User
                    {
                        FirstName = updatedAccount.FirstName,
                        LastName = updatedAccount.LastName,
                        EmailAddress = updatedAccount.Email,
                        Username = updatedAccount.UserName,
                        Avatar = updatedAccount.Avatar==""?updatedAccount.UserName:updatedAccount.Avatar,
                        IsAdminsitrator=updatedAccount.IsAdministrator,
                        Password="1234qwer"
                    };
                }

                //Step 4. Update the database user object from the UserObject
                db.Users.Add(useraccount);
                //Step 5. Save changes
                db.SaveChanges();

            }catch(Exception e)
            {
                Console.WriteLine(e.Message + "./n" + e.StackTrace);
            }
        }

        /// <summary>
        /// This method allows a user's account to be pulled and updated. 
        /// Note: passwords are not included in the information that is pulled
        /// </summary>
        /// <param name="userID">This is the ID of the user in ClueLess whose account if being pulled</param>
        /// <returns>This return the user's account information</returns>
        public static Account GetAccount(int userID)
        {
            Account userAccount= new Account();
            try
            {

                //Step 1. Connect to the database
                ClueLessContext db = new ClueLessContext();
                //Step 2. Pull the user by the ID
                User user = db.Users.Where(x => x.ID == userID).FirstOrDefault();
                //Step 3. "Map" the user information to the account information\
                userAccount = new Account
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.EmailAddress,
                    Avatar = user.Avatar,
                    UserName = user.Username,
                    IsAdministrator = user.IsAdminsitrator,
                    Password = user.Password
                };

                //Step 4. Return the account
                return userAccount;

            }catch(Exception e)
            {
                Console.WriteLine(e.Message + "/n" + e.StackTrace);
                return userAccount;
            }
        }

        /// <summary>
        /// This method changes the user's password to thier new specified password, and
        /// sends them an email notification that thier password was changed.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="newPassword"></param>
        public static void ResetPassword( int userID, string newPassword)
        {
            ClueLessContext db = new ClueLessContext();
            User user = db.Users.Where(x => x.ID == userID).FirstOrDefault();
            user.Password = newPassword;
            db.SaveChanges();

            Notification.SendEmail(NotificationType.PasswordReset, user.EmailAddress);
        }

        public static void RequestUserName(string emailAddress)
        {
            Notification.SendEmail(NotificationType.UserNameReminder, emailAddress);
        }

        public static int ValidateUser(string username, string password)
        {
            int userID = -1;
            using (ClueLessContext db= new ClueLessContext())
            {
                User user = db.Users.Where(x => x.Username.Equals(username) && x.Password.Equals(password)).FirstOrDefault();
                userID = user == null ? -1 : user.ID;
            }

            //if (userID > 0)
            //{
            //    HttpContext.Current.Session["userID"] = userID;
            //}
            return userID;
        }

        public  int UserID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public  String UserName { get; set; }
        public  String Avatar { get; set; }
        public  bool IsAdministrator { get; set; }
        public  string Email { get; set; }
        private  string Password { get; set; }
    }
}