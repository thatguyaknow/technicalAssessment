﻿using System;

namespace SessionViewer.Models
{
    /// <summary>
    /// Contains information on one section or lap from a session
    /// </summary>
    public class SectionData
    {
        /// <summary>
        /// Empty constructor
        /// </summary>
        public SectionData()
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
        public SectionData(int carNumber, string lastName, string shortName, double time, double entryTime, double exitTime, int lap, Flag flag, DateTime entryTOD)
        {
            CarNumber = carNumber;
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            ShortName = shortName ?? throw new ArgumentNullException(nameof(shortName));
            Time = time;
            EntryTime = entryTime;
            ExitTime = exitTime;
            Lap = lap;
            Flag = flag;
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
        public Flag Flag { get; set; }

        /// <summary>
        /// Time of Day when car entered the section
        /// </summary>
        public DateTime EntryTOD { get; set; }
    }

    /// <summary>
    /// Enum for the flag the section was recorded under
    /// </summary>
    public enum Flag
    {
        Green,
        Yellow,
        Red,
        Black,
        Checker,
        GreenWhiteChecker
    }
}