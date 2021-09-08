using System;
using System.Collections.Generic;

namespace SessionViewer.Models
{
    /// <summary>
    /// Data for a given lap and it's sections
    /// </summary>
    public class LapData : SectionData
    {
        private List<SectionData> Sections;

        /// <summary>
        /// Empty default constructor
        /// </summary>
        public LapData()
        {
            Sections = new List<SectionData>();
        }

        /// <summary>
        /// Constructor with parameters for base class (section)
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
        public LapData(int carNumber, string lastName, string shortName, double time, double entryTime, double exitTime, int lap, Flag flag, DateTime entryTOD) : base(carNumber, lastName, shortName, time, entryTime, exitTime, lap, flag, entryTOD)
        {
            Sections = new List<SectionData>();
        }

        /// <summary>
        /// Constructor with parameters for base class (section) AND section list
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
        /// <param name="sections"></param>
        public LapData(int carNumber, string lastName, string shortName, double time, double entryTime, double exitTime, int lap, Flag flag, DateTime entryTOD, List<SectionData> sections) : base(carNumber, lastName, shortName, time, entryTime, exitTime, lap, flag, entryTOD)
        {
            Sections = sections;
        }

    }
}