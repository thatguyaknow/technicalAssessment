using SessionViewer.Models;
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
        [InlineData("\\Chevrolet Detroit Grand Prix_Practice 2.csv")]
        [InlineData("\\Chevrolet Detroit Grand Prix_Practice 2_BAD.csv")]
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

        [Fact]
        public void FlagParseTest()
        {
            Assert.True(SessionFileLoading.ParseFlag("Red", out Flag redFlag));
            Assert.True(redFlag == Flag.Red);
            Assert.False(SessionFileLoading.ParseFlag("Orange", out _));
            Assert.True(SessionFileLoading.ParseFlag("YELLOW", out _));
            Assert.True(SessionFileLoading.ParseFlag("CHECKER ", out _));
        }

        [Theory]
        [InlineData("16,Servia,S2,10.9677,55943.1685,55954.1362,0,Green,15:32:23.1685")]
        [InlineData("3,Castroneves,S2,110.6065,58107.4273,58118.0338,7,Checker,16:08:27.427")]
        [InlineData("5,Hinchcliffe,S2,10.2636,57153.7719,57164.0355,7,Green,15:52:33.7719")]
        public void LineParseTest(string line)
        {
            Assert.NotNull(SessionFileLoading.ParseRawLine(line));
        }

        [Theory]
        [InlineData("16,Servia,S2,10.9677,55943.1685,55954.1362,0,Green,15:32:231685")]
        [InlineData("16,Servia,S2,10.9677,55943.1685,test.1362,0,Green,15:32:23.1685")]
        [InlineData("16,Servia,S2,10.9677,55943.1685,55954.1362,0,Orange,15:32:23.1685")]
        [InlineData("16,Servia,S2,FAST,55943.1685,55954.1362,0,Green,15:32:23.1685")]
        public void LineParseTestFail(string line) //test to make sure bad lines are properly handled 
        {
            Assert.Null(SessionFileLoading.ParseRawLine(line));
        }


    }
}