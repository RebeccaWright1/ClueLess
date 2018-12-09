using ClueLess.Database;
using ClueLess.Database.DataModels;
using ClueLess.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;

namespace ClueLess.Models
{
    public class Configuration
    {
        public static void CreateConfiguration()
        {
            //Connect to the database
            ClueLessContext db = new ClueLessContext();
            //Get the ID of the Default configurations
            Database.DataModels.Configuration config = db.Configurations.Where(x => x.Name == "Default").FirstOrDefault();


            string ConfigurationDownload = "Attachment; filename=ConfigurationTemplate.txt";
            ////Clear everything from the response object
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ClearContent();

            ////Populate the reponse object
            HttpContext.Current.Response.AddHeader("content-disposition", ConfigurationDownload);
            HttpContext.Current.Response.ContentType = "text/csv";
            HttpContext.Current.Response.AddHeader("Pragma", "public");

            var fileWriter = new StringBuilder();

            //Write out the Name and Share row indicators
            fileWriter.Append("Name:" + config.Name + Environment.NewLine);
            fileWriter.Append("Share with Public:" + (config.isShared==true?"Yes":"No") +Environment.NewLine);
            fileWriter.AppendLine();
            //Write out the "Headers for the locations:
            fileWriter.AppendLine("Location, Row, Column, Secret Door Connection");
            //Pull the information for the locations
            List<Position> locations = db.Positions.Where(x => x.ConfigurationID == config.ID && x.Location.LocationType==LocationType.Room).ToList();
            foreach(Position p in locations)
            {
                SecretPassages connectingPassage = db.SecretPassages.Where(x => x.PositionID_1 == p.ID || x.PositionID_2 == p.ID).FirstOrDefault();
                string connectingRoom = "";
                if (connectingPassage != null)
                {
                    if (connectingPassage.PositionID_1 == p.ID)
                    {
                        Position connectedPosition = db.Positions.Where(x => x.ID == connectingPassage.PositionID_2).FirstOrDefault();
                        connectingRoom = connectedPosition.Location.LocationName;
                    }
                    else
                    {
                        Position connectedPosition = db.Positions.Where(x => x.ID == connectingPassage.PositionID_1).FirstOrDefault();
                        connectingRoom = connectedPosition.Location.LocationName;
                    }
                }
               // connectingRoom = connectingPassage.PositionID_1 == p.ID ? connectingPassage.Room2.Location.LocationName : connectingPassage.Room1.Location.LocationName;
                
                fileWriter.AppendLine(p.Location.LocationName +","+ p.RowPosition + "," + p.ColumnPosition + "," + connectingRoom);
            }
           
            //Write out an empty line
            fileWriter.AppendLine();

            //Write out the "Headers" for the Characters
            fileWriter.AppendLine("Characters, Starting Position, Color(R:G:B)");
            //Pull the information for the characters
            List<Database.DataModels.CharacterConfiguration> characters = db.CharacterConfigurations.Where(x => x.ConfigurationID == config.ID).ToList();
            foreach(Database.DataModels.CharacterConfiguration c in characters)
            {
                Color characterColor = (Color)ColorTranslator.FromHtml("#" + c.Color);//Color.FromName(c.Color);
                fileWriter.AppendLine(c.Character.Name + "," + c.StartingPosition.Location.LocationName + "," + characterColor.R +":"+ characterColor.G + ":" + characterColor.B);
               
            }

            //Pull the information for the Weapons
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\wrigh\OneDrive\Documents\GraduateSchool\FoundationOfSoftwareEngineering\ConfigurationTest2.txt");
            file.WriteLine(fileWriter.ToString());
           
            file.Flush();
            file.Dispose();
            string text = fileWriter.ToString();
            HttpContext.Current.Response.BinaryWrite(Encoding.ASCII.GetBytes(fileWriter.ToString()));
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();

        }

        public static bool SaveConfiguration(HttpPostedFile newConfiguration, int userID)
        {
            Database.DataModels.Configuration configuration = new Database.DataModels.Configuration();
            configuration.UserID = userID;

            Dictionary<String, String> SecretRoomConnections = new Dictionary<string, string>();
            List<Database.DataModels.Location> locations = new List<Database.DataModels.Location>();
            Dictionary<string, string> positions = new Dictionary<string, string>();
            List<PositionHelper> positionsList = new List<PositionHelper>();
            List<Database.DataModels.Character> characters = new List<Database.DataModels.Character>();
            Dictionary<string, string> characterConfigurationInfo = new Dictionary<string, string>();
            List<CharacterConfiguration> characterConfiguration = new List<CharacterConfiguration>();
            List<Database.DataModels.Weapon> weapons = new List<Database.DataModels.Weapon>();
            

            //Check the extension
            string fileExtension = Path.GetExtension(newConfiguration.FileName);

            if (!fileExtension.Equals(".txt"))
                return false;
            //Save the file to a temporary file
            String tempFolder = Path.GetTempPath();
            String filePath = Path.Combine(tempFolder, newConfiguration.FileName);
            newConfiguration.SaveAs(filePath);
            StreamReader streamReader = new StreamReader(filePath);
            while (!streamReader.EndOfStream)
            {
                //Read a Line
                string line = streamReader.ReadLine();

                //Split the line
                string[] lineSplit = line.Split(',');

                if (lineSplit[0].Contains("Name:")) configuration.Name = lineSplit[0].Substring(lineSplit[0].IndexOf(":")+1);
                if (lineSplit[0].Contains("Share with Public:"))
                {
                    string answer = lineSplit[0].Substring(lineSplit[0].IndexOf(':') + 1);
                    configuration.isShared = answer.Trim().ToLower().Equals("yes")?true:false;

                }

                if (lineSplit[0].ToLower().Trim() == "location")
                {                    
                    for (int i=0; i < 9; i++)
                    {
                        //Read the next line
                        line = streamReader.ReadLine();
                        if (line == "")
                            return false;
                        lineSplit = line.Split(',');

                        //Create the location
                        Database.DataModels.Location newLocation = new Database.DataModels.Location();
                        newLocation.LocationName = lineSplit[0];
                        newLocation.LocationType = Database.DataModels.LocationType.Room;

                        locations.Add(newLocation);

                        //Create the Position
                        positions.Add(lineSplit[0], lineSplit[1] + "," + lineSplit[2]);
                        positionsList.Add(new PositionHelper { Name = lineSplit[0], Row = Convert.ToInt32(lineSplit[1]), Column = Convert.ToInt32(lineSplit[2]) });

                        if (lineSplit[3] != "")
                            SecretRoomConnections.Add(newLocation.LocationName, lineSplit[3]);

                    }

                    //Create and save the location positions
                    PositionHelper pos1;
                    PositionHelper pos2;
                    string locationName = "";
                    for (int i = 0; i <=4; i++)
                    {
                        for (int j = 0; j <=4; j +=2)
                        {

                            if (i % 2 == 0 && j<4)
                            {
                                pos1 = positionsList.Where(x => x.Row == i && x.Column == j).FirstOrDefault();
                                pos2 = positionsList.Where(x => x.Row == i && x.Column == j + 2).FirstOrDefault();
                                locationName = pos1.Name + " to " + pos2.Name;
                                locations.Add(new Database.DataModels.Location
                                {
                                    LocationName = locationName,
                                    LocationType = LocationType.Hallway
                                });

                                PositionHelper hall = new PositionHelper
                                {
                                    Name = locationName,
                                    Row = i,
                                    Column = j
                                };

                                positionsList.Add(hall);
                            }
                            else if (i%2!=0)
                            {
                                pos1 = positionsList.Where(x => x.Row == i - 1 && x.Column == j).FirstOrDefault();
                                pos2 = positionsList.Where(x => x.Row == i + 1 && x.Column == j).FirstOrDefault();
                                locationName = pos1.Name + " to " + pos2.Name;
                                locations.Add(new Database.DataModels.Location
                                {
                                    LocationName = locationName,
                                    LocationType = LocationType.Hallway
                                });
                                PositionHelper hall = new PositionHelper
                                {
                                    Name = locationName,
                                    Row = i,
                                    Column = j
                                };
                                positionsList.Add(hall);
                            }
                           

                        }
                    }

                    //Ensure that the number of secret room configurations is not more than 2
                    if (SecretRoomConnections.Count > 4)
                        return false;
                }

                if(lineSplit[0].ToLower().Trim()== "characters")
                {
                    for(int i=0; i<6; i++)
                    {
                        //Read the next line
                        line = streamReader.ReadLine();
                        if (line == "")
                            return false;
                        lineSplit = line.Split(',');

                        characters.Add(new Database.DataModels.Character { Name = lineSplit[0] });
                        characterConfigurationInfo.Add(lineSplit[0], lineSplit[1] + "," + lineSplit[2]);
                    }
                }

                if (lineSplit[0].Trim().ToLower() == "weapons")
                {
                    for (int i = 0; i< 6; i++)
                    {
                        //Read the next line
                        line = streamReader.ReadLine();
                        if (line == "")
                            return false;
                        lineSplit = line.Split(',');

                        weapons.Add(new Database.DataModels.Weapon { Name = lineSplit[0] });
                    }

                    if (!streamReader.EndOfStream)
                        return false;
                }
            }

            //At this point the data can be saved
            using(ClueLessContext db= new ClueLessContext())
            {
                //Save the configuration
                db.Configurations.Add(configuration);
                db.SaveChanges();

                //Save the locations
                foreach(Database.DataModels.Location l in locations)
                {
                    //add the location if it does not already exist
                    Database.DataModels.Location checkLocation = db.Locations.Where(x => x.LocationName.Equals(l.LocationName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                    if (checkLocation == null)
                    {
                        db.Locations.Add(l);
                        db.SaveChanges();
                    }
                    else
                    {
                        l.ID = checkLocation.ID;
                    }
                }
                
                //Save the characters
                foreach(Database.DataModels.Character c in characters)
                {
                    //add the character if it does not already exist
                    Database.DataModels.Character checkCharacter = db.Characters.Where(x => x.Name.Equals(c.Name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                    if (checkCharacter == null)
                    {
                        db.Characters.Add(c);
                        db.SaveChanges();
                    }
                    else
                    {
                        c.ID = checkCharacter.ID;
                    }
                }

                //Save the weapons
                foreach(Database.DataModels.Weapon w in weapons)
                {
                    //add the weapon if it does not already exist
                    Database.DataModels.Weapon checkWeapon = db.Weapons.Where(x => x.Name.Equals(w.Name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                    if (checkWeapon == null)
                    {
                        db.Weapons.Add(w);
                        db.SaveChanges();
                    }
                }

                //Save the location permissions
                List<Position> dbPositions = new List<Position>();
                foreach (PositionHelper ph in positionsList)
                {
                    int locationID = locations.Where(x => x.LocationName == ph.Name).Select(x => x.ID).FirstOrDefault();
                    dbPositions.Add(new Position
                    {
                        LocationID = locationID,
                        RowPosition = ph.Row,
                        ColumnPosition = ph.Column,
                        ConfigurationID = configuration.ID
                        
                    });
                }


                db.Positions.AddRange(dbPositions);
                db.SaveChanges();

                //Create and save the character configurations
                foreach (var key in characterConfigurationInfo.Keys)
                {
                    int characterID = characters.Where(x => x.Name == key).Select(x => x.ID).FirstOrDefault();
                    string[] characterInfo = characterConfigurationInfo[key].Split(',');
                    string[] characterColor = characterInfo[1].Split(':');
                    string name = characterInfo[0];
                    int startingPositionID = db.Positions.Where(x => x.Location.LocationName == name).Select(x => x.ID).FirstOrDefault();

                    Color color = Color.FromArgb(Convert.ToInt32(characterColor[0]), Convert.ToInt32(characterColor[1]), Convert.ToInt32(characterColor[2]));
                    characterConfiguration.Add(new CharacterConfiguration
                    {
                        CharacterID=characterID,
                        ConfigurationID = configuration.ID,
                        StartingPositionID = startingPositionID,
                        Color=color.Name
                    });


                }

                db.CharacterConfigurations.AddRange(characterConfiguration);
                db.SaveChanges();

            return true;
        }

        
}
        public  int ID { get; set; }
        public  int UserID { get; set; }
        public  string Name { get; set; }
        public  bool IsShared { get; set; }

    }
}