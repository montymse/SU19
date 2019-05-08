using System;
using System.IO;
using NUnit.Framework;
using SpaceTaxi_1;

namespace TestSpaceTaxi.TestLevelParser {
    [TestFixture]
    public class TestOpener {
        
        [Test]
        public void TestFileToString() {

            Assert.AreEqual(
                Opener.FileToString(
                    "/Users/Muse/Desktop/su19-mikaelMuseFrederik/SU19-exercises/TestSpaceTaxi/Levels/abc.txt"),
                "abcdefghijklmnopqrstuvwyz"
                );
        }

        [Test]
        public void TestCutStringLevel() {
            string[] x = new[]
                {"1", "2", "3", "4", "5", "6", "7", "8", "9","1", "2", "3", "4", "5", "6", "7", "8", "9","1", "2", "3", "4", "5",null};
                
            Assert.AreEqual(
                Opener.CutStringLevel(
                    "/Users/Muse/Desktop/su19-mikaelMuseFrederik/SU19-exercises/TestSpaceTaxi/Levels/123.txt"),
                x);


        }
    }
}
