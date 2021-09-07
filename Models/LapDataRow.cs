using System;

namespace technicalAssessment
{
    /// <summary>
    /// Contains information on one lap from a session
    /// </summary>
    public class LapDataRow
    {
        /// <summary>
        /// Empty constructor
        /// </summary>
        LapDataRow()
        {

        }

        /// <summary>
        /// Constructor with options for all properties and null checks
        /// </summary>
        /// <param name="carNumber"></param>
        /// <param name="lastName"></param>
        /// <param name="shortName"></param>
        /// <param name="time"></param>
        /// <param name="entryTime"></param>
        /// <param name="exitTime"></param>
        /// <param name="lap"></param>
        /// <param name="flag"></param>
        /// <param name="entryTOD"></param>
        public LapDataRow(int carNumber, string lastName, string shortName, double time, double entryTime, double exitTime, int lap, string flag, DateTime entryTOD)
        {
            CarNumber = carNumber;
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            ShortName = shortName ?? throw new ArgumentNullException(nameof(shortName));
            Time = time;
            EntryTime = entryTime;
            ExitTime = exitTime;
            Lap = lap;
            Flag = flag ?? throw new ArgumentNullException(nameof(flag));
            EntryTOD = entryTOD;
        }


        /// <summary>
        /// Car number
        /// </summary>
        public int CarNumber { get; set; }

        /// <summary>
        /// Driver’s last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Track section name (sectors and laps)
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Section time
        /// </summary>
        public double Time { get; set; }

        /// <summary>
        /// Elapsed time the car entered the section
        /// </summary>
        public double EntryTime { get; set; }

        /// <summary>
        /// Elapsed time the car exited the section
        /// </summary>
        public double ExitTime { get; set; }

        /// <summary>
        /// Lap number of the section
        /// </summary>
        public int Lap { get; set; }

        /// <summary>
        /// Flag when the car entered the section
        /// </summary>
        public string Flag { get; set; }

        /// <summary>
        /// Time of Day when car entered the section
        /// </summary>
        public DateTime EntryTOD { get; set; }
    }
}