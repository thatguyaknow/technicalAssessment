using System.IO;
using System.Linq;
using technicalAssessment;
using Xunit;

namespace SessionViewer.Tests
{
    public class FileLoadTests
    {
        readonly string basePath;

        public FileLoadTests()
        {
            basePath = Path.GetFullPath("ExampleFiles");
        }

        [Theory]
        [InlineData ("\\Chevrolet Detroit Grand Prix_Practice 2.csv")]
        [InlineData ("\\Chevrolet Detroit Grand Prix_Practice 2_BAD.csv")]
        public async void FileLoadTest(string fileName)
        {
            var fullPath = basePath + fileName;
            
            Assert.True(File.Exists(fullPath));

            var sessionDataList = await SessionFileLoading.LoadSessionCSV(fullPath);

            var sessionData = sessionDataList.First();

            Assert.True(sessionData.Laps.Any());
            Assert.False(string.IsNullOrEmpty(sessionData.DriverName));
            Assert.True(sessionData.FastLapTime > 0.0);

        }
    }
}