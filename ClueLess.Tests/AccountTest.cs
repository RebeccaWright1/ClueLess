using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using ClueLess.Models;
using ClueLess.Database;
using ClueLess.Database.DataModels;
using System.Linq;

namespace ClueLess.Tests
{
    [TestClass]
    public class AccountTest
    {

        /// <summary>
        /// This test case is to ascertain that an account is created when the user ID 
        /// is greater than or equal to 0, and that the account information updated when
        /// information is recieved on an existing account.
        /// 
        /// The account that is created for this test case is deleted at the end of test.
        /// </summary>
        [TestMethod]
        public void CreateAccount()
        {
            //Connect to the database
            AppDomain current = AppDomain.CurrentDomain;
            var appDataDir = Path.GetFullPath("../../../AppData");
            var test=current.GetData("DataDirectory");
            current.SetData("DataDirectory", appDataDir);

            Account testAccount = new Account
            {
                UserID = 0,
                FirstName = "Test",
                LastName = "User",
                Email = "test.user@gmail.com",
                UserName = "TestUser",
                Avatar = "",
                IsAdministrator = false
            };

            Account.SetAccount(testAccount);
            ClueLessContext db = new ClueLessContext();
            Account TestSave = Account.GetAccount(db.Users.Max(x => x.ID));
            

            Assert.IsNotNull(TestSave, "Account was successfully saved");

            //Update the created accunt
            string oldAvatar = testAccount.Avatar;
            testAccount.Avatar = "IAmAwesome";

            Account.SetAccount(testAccount);

            //Get the updated account
            Account testAccountUpdate = Account.GetAccount(testAccount.UserID);
            Assert.AreNotEqual(oldAvatar, testAccount.Avatar);

            //Remove the account upon completion

            User testUser = db.Users.Where(x => x.ID == testAccount.UserID).FirstOrDefault();
            db.Users.Remove(testUser);
            db.SaveChanges();
        }
    }
}
