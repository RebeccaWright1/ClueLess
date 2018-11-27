using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
                throw new NotImplementedException();
                //Step 1. Connect to the database
                //Step 2. Pull the user from the database based on the user's ID
                //Step 3. If the user is null, create a new user
                //Step 4. Update the database user object from the UserObject
                //Step 5. Save changes

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
            Account userAccount = new Account();
            try
            {
               
                //Step 1. Connect to the database
                //Step 2. Pull the user by the ID
                //Step 3. "Map" the user information to the account information\
                //Step 4. Return the account
                return userAccount;
            }catch(Exception e)
            {
                Console.WriteLine(e.Message + "/n" + e.StackTrace);
                return userAccount;
            }
        }

        public  int UserID { get; set; }
        private  String UserName { get; set; }
        public  String Avatar { get; set; }
        public  bool IsAdministrator { get; set; }
        public  string Email { get; set; }
        private  string Password { get; set; }
    }
}