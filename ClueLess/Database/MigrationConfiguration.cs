using ClueLess.Database.DataModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace ClueLess.Database
{
    internal sealed class MigrationConfiguration:DbMigrationsConfiguration <ClueLessContext>
    {
        public MigrationConfiguration()
        {

            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;


        }

        protected override void Seed(ClueLessContext context)
        {
            //Add the system as a user
            context.Users.AddOrUpdate(x => x.Username,
                new User { ID = 1, FirstName = "System", LastName = "System", Username = "System" , EmailAddress="test@gmail.com", Password="123qwer"});
            context.SaveChanges();
            //Create the record for the default configuration
            context.Configurations.AddOrUpdate(x => x.Name,
                new Configuration() { ID=1, UserID= 1, Name = "Default", isShared = true });
            context.SaveChanges();

            //Create the default locations
            context.Locations.AddOrUpdate(x => x.LocationName,
                //Add the rooms
                new Location() { ID=1, LocationName = "Study", LocationType = LocationType.Room }, //1
                new Location() { ID=2, LocationName = "Hall", LocationType = LocationType.Room }, //2
                new Location() { ID=3, LocationName = "Lounge", LocationType = LocationType.Room }, //3
                new Location() { ID=4, LocationName = "Library", LocationType = LocationType.Room }, //4
                new Location() { ID=5, LocationName = "Billiard Room", LocationType = LocationType.Room }, //5
                new Location() { ID=6, LocationName="Dining Room", LocationType=LocationType.Room}, //6
                new Location() { ID=7, LocationName = "Conservatory", LocationType = LocationType.Room }, //7
                new Location() { ID=8, LocationName = "Ballroom", LocationType = LocationType.Room }, //8
                new Location() { ID=9, LocationName = "Kitchen", LocationType = LocationType.Room }, //9

                //Add the connecting Hallways
                new Location() { ID=10, LocationName = "StudyToHall", LocationType = LocationType.Hallway }, //10
                new Location() { ID=11, LocationName = "HallToLounge", LocationType = LocationType.Hallway }, //11
                new Location() { ID=12, LocationName = "StudyToLibrary", LocationType = LocationType.Hallway }, //12
                new Location() { ID=13, LocationName = "HallToBilliardRoom", LocationType = LocationType.Hallway }, //13
                new Location() { ID=14, LocationName = "LoungeToDiningRoom", LocationType = LocationType.Hallway }, //14
                new Location() { ID=15, LocationName = "LibraryToBillardRoom", LocationType = LocationType.Hallway }, //15
                new Location() { ID=16, LocationName = "BilliardRoomToDiningRoom", LocationType = LocationType.Hallway }, //16
                new Location() { ID=17, LocationName = "LibraryToConservatory", LocationType = LocationType.Hallway }, //17
                new Location() { ID=18, LocationName = "BillardRoomToBallroom", LocationType = LocationType.Hallway }, //18
                new Location() { ID=19, LocationName = "DiningRoomToKitchen", LocationType = LocationType.Hallway }, //19
                new Location() { ID=20, LocationName = "ConservatoryToBallroom", LocationType = LocationType.Hallway }, //20
                new Location() { ID=21, LocationName = "BallroomToKitchen", LocationType = LocationType.Hallway }//21

                );
            context.SaveChanges();

            //Create the default character
            context.Characters.AddOrUpdate(x => x.Name,
                new Character() { ID=1, Name = "Miss Scarlet" },
                new Character() { ID=2, Name = "Prof. Plum" },
                new Character() { ID=3, Name="Col. Mustard"},
                new Character() { ID=4, Name="Mrs. Peacock"},
                new Character() { ID=5, Name= "Mr. Green"},
                new Character() { ID=6, Name="Mrs. White"}
                );
            context.SaveChanges();

            //Set up the location positions
            context.Positions.AddOrUpdate(x => x.ID,
                //Start top row
                new Position() { ID=1, ConfigurationID = 1, LocationID = 1, RowPosition = 0, ColumnPosition = 0 }, //Study, ID=1
                new Position() { ID=2, ConfigurationID = 1, LocationID = 10, RowPosition = 0, ColumnPosition = 1 }, //StudyToHall ID=2
                new Position() { ID=3, ConfigurationID = 1, LocationID = 2, RowPosition = 0, ColumnPosition = 2 }, //Hall ID=3
                new Position() { ID=4, ConfigurationID = 1, LocationID = 11, RowPosition = 0, ColumnPosition = 3 }, //HallToLounge ID=4
                new Position() { ID=5, ConfigurationID = 1, LocationID = 3, RowPosition = 0, ColumnPosition = 4 }, //Lounge ID=5
                //End Row

                //Start Second Row
                new Position() { ID=6, ConfigurationID = 1, LocationID = 12, RowPosition = 1, ColumnPosition = 0 }, //StudyToLibrary ID=6
                new Position() { ID=7, ConfigurationID = 1, LocationID = 13, RowPosition = 1, ColumnPosition = 2 }, //HallToBilliardRoom ID=7
                new Position() { ID=8, ConfigurationID = 1, LocationID = 14, RowPosition = 1, ColumnPosition = 4 }, //LoungeToDiningRoom ID=8
                //End Row

                //Start Third Row
                new Position() { ID=9, ConfigurationID = 1, LocationID = 4, RowPosition = 2, ColumnPosition = 0 }, //Library ID=9
                new Position() { ID=10, ConfigurationID = 1, LocationID = 15, RowPosition = 2, ColumnPosition = 1 }, //LibraryToBilliardRoom ID=10
                new Position() { ID=11, ConfigurationID = 1, LocationID = 5, RowPosition = 2, ColumnPosition = 2 }, //BillardRoom ID=11
                new Position() { ID=12, ConfigurationID = 1, LocationID = 16, RowPosition = 2, ColumnPosition = 3 }, //BilliardRoomToDiningRoom ID=12
                new Position() { ID=13, ConfigurationID = 1, LocationID = 6, RowPosition = 2, ColumnPosition = 4 }, //DiningRoom ID=13
                //End Row

                //Start Fourth Row
                new Position() { ID=14, ConfigurationID = 1, LocationID = 17, RowPosition = 3, ColumnPosition = 0 }, //LibraryToConservatory ID=14
                new Position() { ID=15, ConfigurationID = 1, LocationID = 18, RowPosition = 3, ColumnPosition = 2 }, //BilliardRoomToBallroom ID=15
                new Position() { ID=16, ConfigurationID = 1, LocationID = 19, RowPosition = 3, ColumnPosition = 4 }, //DiningRoomToKitchen ID=16
                //End Row

                //Start Fifth Row
                new Position() { ID=17, ConfigurationID = 1, LocationID = 7, RowPosition = 4, ColumnPosition = 0 }, //Conservatory ID=17
                new Position() { ID=18, ConfigurationID = 1, LocationID = 20, RowPosition = 4, ColumnPosition = 1 }, //ConservatoryToBallroom ID=18
                new Position() { ID=19, ConfigurationID = 1, LocationID = 8, RowPosition = 4, ColumnPosition = 2 }, //Ballroom ID=19
                new Position() { ID=20, ConfigurationID = 1, LocationID = 21, RowPosition = 4, ColumnPosition = 3 }, //BallroomToKitchen ID=20
                new Position() { ID=21, ConfigurationID = 1, LocationID = 9, RowPosition = 4, ColumnPosition = 4 }  //Kitchen ID=21
                );
                //End Row

            context.SaveChanges();

            //Create the Secret Room connections
            context.SecretPassages.AddOrUpdate(x => x.ID,
                new SecretPassages() { ID=1, PositionID_1=1, PositionID_2=21}, //Study to Kitchen
                new SecretPassages() { ID=2, PositionID_1=5, PositionID_2=17} //Lounge to Conservatory
                );

            context.SaveChanges();
            //Create the Character Configurations
            context.CharacterConfigurations.AddOrUpdate(x => x.ID,
                
                new CharacterConfiguration() { ID = 1, CharacterID = 1, Color = Color.Red.ToArgb().ToString("X"), StartingPositionID = 3, ConfigurationID = 1 },// Miss Scarlet starts in the Hall
                new CharacterConfiguration() { ID = 2, CharacterID = 2, Color = Color.Purple.ToArgb().ToString("X"), StartingPositionID = 9, ConfigurationID = 1 }, //Prof. Plum start in the Library
                new CharacterConfiguration() { ID = 3, CharacterID = 3, Color = Color.Yellow.ToArgb().ToString("X"), StartingPositionID = 5, ConfigurationID = 1 }, //Col. Mustard starts in the Lounge
                new CharacterConfiguration() { ID = 4, CharacterID = 4, Color = Color.Blue.ToArgb().ToString("X"), StartingPositionID=17, ConfigurationID = 1 }, //Mrs. Peacock starts in the Conservatory
                new CharacterConfiguration() { ID = 5, CharacterID=5, Color=Color.Green.ToArgb().ToString("X"), StartingPositionID=19, ConfigurationID = 1 }, // Mr. Green starts in the Ballroom
                new CharacterConfiguration() { ID = 6, CharacterID=6, Color=Color.White.ToArgb().ToString("X"), StartingPositionID=21, ConfigurationID = 1 } //Mrs. White starts in the Kitchen      
                );
            context.SaveChanges();

            //Create the Weapon Records
            context.Weapons.AddOrUpdate(x => x.Name,
                new Weapon() { ID=1, Name = "Rope" },
                new Weapon() { ID=2, Name="Lead Pipe"},
                new Weapon() { ID=3, Name="Revolver"},
                new Weapon() { ID=4, Name="Wrench"},
                new Weapon() { ID=5, Name="Candelstick"},
                new Weapon() { ID=6, Name="Dagger"}
                );
            context.SaveChanges();

            
            base.Seed(context);
        }
    }
}