﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SessionViewer.Models
{
    /// <summary>
    /// Session data for a car number / given driver
    /// </summary>
    public class SessionData 
    {
        /// <summary>
        /// Lap objects
        /// </summary>
        public List<LapData> Laps { get; set; }

        /// <summary>
        /// Time of the last lap
        /// </summary>
        public double LastLapTime => Laps.Last().Time;

        /// <summary>
        /// Time of the fastest lap in seconds
        /// </summary>
        public double FastLapTime => Laps.Select(x => x.Time).ToArray().Min();

        /// <summary>
        /// Lap number of the fastest lap
        /// </summary>
        public double FastLapNum => Laps.Where(x => x.Time == FastLapTime).First().Lap;

        /// <summary>
        /// Total number of laps completed in the session
        /// </summary>
        public int TotalLaps => Laps.Count();

        /// <summary>
        /// Car number
        /// </summary>
        public int CarNumber => Laps.First().CarNumber;

        /// <summary>
        /// Last name of the driver
        /// </summary>
        public string DriverName => Laps.First().LastName;

        /// <summary>
        /// Rank of the car number / driver in the session based on lap time
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// Name of the event
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// Session name
        /// </summary>
        public string SessionName { get; set; }

        public SessionData()
        {
            Laps = new List<LapData>();
        }

    }
}