using SessionViewer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SessionViewer
{
    public static class SessionFileLoading
    {
        public static async Task<List<SessionData>> LoadSessionCSV(string fileName)
        {
            List<string> rawLines = new List<string>();

            try
            {
                using (StreamReader streamReader = new StreamReader(fileName))
                {
                    await streamReader.ReadLineAsync(); //Ignore header

                    string line;
                    while ((line = await streamReader.ReadLineAsync()) != null)
                    {
                        rawLines.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Exception when reading {fileName}. \nException: {e.Message}\nStackTrace: {e.StackTrace}");
            }

            return ParseRawLines(rawLines);
        }

        /// <summary>
        /// Converts raw lines of session data from a csv to a list of session data
        /// </summary>
        /// <param name="rawLines"></param>
        /// <returns></returns>
        private static List<SessionData> ParseRawLines(List<string> rawLines)
        {
            List<SessionData> sessionDataList = new List<SessionData>();

            //return empty list if no raw lines
            if (!rawLines.Any()) return sessionDataList;

            List<SectionData> sectionDataList = new List<SectionData>();
            ///First convert each line to Section Data
            foreach (var line in rawLines)
            {
                SectionData parsedLine = ParseRawLine(line);
                if (parsedLine != null) sectionDataList.Add(parsedLine);
            }

            var lapData = new List<SectionData>();

            foreach (var lapSectionData in sectionDataList)
            {
                //We only want laps
                if (!(lapSectionData.ShortName.ToLower().Trim() == "lap")) continue;

                //Add new in order to create a deep copy
                lapData.Add(new SectionData(lapSectionData.CarNumber, lapSectionData.LastName, lapSectionData.ShortName, lapSectionData.Time, lapSectionData.EntryTime,
                    lapSectionData.ExitTime, lapSectionData.Lap, lapSectionData.Flag, lapSectionData.EntryTOD));
            }

            sectionDataList.RemoveAll(sectionData => sectionData.ShortName.ToLower().Trim() == "lap"); //Remove the laps after extracted

            //Add lap sectionData to Session data for each car number while there are still laps in the sectiondatalist
            foreach (var lapSection in lapData)
            {
                SessionData sessionData;
                //If no session data for this car create one
                if (!sessionDataList.Any(x => x.CarNumber == lapSection.CarNumber))
                {
                    sessionData = new SessionData();
                    sessionDataList.Add(sessionData);
                }
                else //otherwise find it in the list
                {
                    sessionData = sessionDataList.First(section => lapSection.CarNumber == section.CarNumber);
                }

                sessionData.Laps.Add(new LapData(lapSection.CarNumber, lapSection.LastName, lapSection.ShortName, lapSection.Time, lapSection.EntryTime,
                    lapSection.ExitTime, lapSection.Lap, lapSection.Flag, lapSection.EntryTOD));

                //Remove the lap after it has been added to avoid duplicates
                sectionDataList.Remove(lapSection);
            }

            /*
            NOTE:
            Lap-less section data is currently unused as is it is unneeded for the base requirement
            As an addition to the project requirements laps could be filled with their appropriate section data
            and this section data could be used to provide further information for each lap to the user.
            Keeping in line with Agile methodology for the first version of the app I intend to provide the base functionality only
            in order to provide it as quickly as possible but have set myself up well for the future feature addition
             */

            return sessionDataList;
        }

        public static SectionData ParseRawLine(string line)
        {
            //Try block in case of bad line (IE without commas) or any other error
            try
            {
                //Split the line
                var lineSplit = line.Split(',');
                //No need to go further if not the currect number of columns
                if (lineSplit.Count() != 9) return null;


                List<bool> parseResults = new List<bool>();

                //Extract out each item from the line
                parseResults.Add(int.TryParse(lineSplit[0], out int carNum));

                string lastName = lineSplit[1];
                string shortName = lineSplit[2];
                parseResults.Add(double.TryParse(lineSplit[3], out double lapTime));
                parseResults.Add(double.TryParse(lineSplit[4], out double entryTime));
                parseResults.Add(double.TryParse(lineSplit[5], out double exitTime));
                parseResults.Add(int.TryParse(lineSplit[6], out int lapNum));
                if (lapNum < 0) parseResults.Add(false);

                parseResults.Add(ParseFlag(lineSplit[7], out Flag flag));

                var entryTODSplit = lineSplit[8].Split(':');

                //Must have Hour:Minute:Second.Millisecond
                if (entryTODSplit.Count() != 3) parseResults.Add(false);

                parseResults.Add(int.TryParse(entryTODSplit[0], out int hour));
                parseResults.Add(int.TryParse(entryTODSplit[1], out int minute));

                var entryTODSecondSplit = entryTODSplit[2].Split('.');

                //Must have Second.Millisecond
                if (entryTODSecondSplit.Count() != 2) parseResults.Add(false);

                parseResults.Add(int.TryParse(entryTODSecondSplit[0], out int second));
                parseResults.Add(int.TryParse(entryTODSecondSplit[1].Substring(0, 3), out int millisecond)); //substring of the first 3 digits as DateTime takes only up to milliseconds

                if (parseResults.Contains(false)) return null; //If any parses have failed row is bad and should be skipped

                //Datetime must have a year, month, day so we set these to 0, we will only use the hour/minute/second/millisecond as that is what is given
                DateTime entryTOD = new DateTime(1, 1, 1, hour, minute, second, millisecond);

                return new SectionData(carNum, lastName, shortName, lapTime, entryTime, exitTime, lapNum, flag, entryTOD);
            }
            catch (Exception ex)
            {
                //If the app had a logger like it should I'd write the exception to the logger
                return null;
            }
        }

        /// <summary>
        /// Parses a flag as a string
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="result">Flag enum of the flag string</param>
        /// <returns>True is parsed successfully</returns>
        public static bool ParseFlag(string flag, out Flag result)
        {
            result = default;

            switch (flag.ToLower().Trim())
            {
                case "green":
                    result = Flag.Green;
                    break;

                case "red":
                    result = Flag.Red;
                    break;

                case "yellow":
                    result = Flag.Yellow;
                    break;

                case "black":
                    result = Flag.Black;
                    break;

                case "checker":
                    result = Flag.Checker;
                    break;

                case "greenwhitechecker":
                    result = Flag.GreenWhiteChecker;
                    break;

                default: //if flag string is not regonized parse fails
                    return false;
            }

            return true;
        }
    }
}