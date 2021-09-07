using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalAssessment
{

    /// <summary>
    /// Contains information on one lap from a session
    /// </summary>
    public class LapDataRow
    {
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
