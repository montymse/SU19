using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using SpaceTaxi_1;
namespace TestSpaceTaxi.TestLevelParser {
    [TestFixture]

    public class TestImageList {
        [Test]
        

        public void TestStringToImageList() {
            List<Tuple<string,string>> x = new List<Tuple<string, string>>();
            x.Add(new Tuple<string, string>("%)"," white-square.png"));

            x.Add(new Tuple<string, string>("#)"," ironstone-square.png"));
            x.Add(new Tuple<string, string>("1)"," neptune-square.png"));
            x.Add(new Tuple<string, string>("2)"," green-square.png"));
            x.Add(new Tuple<string, string>("3)"," yellow-stick.png"));
            x.Add(new Tuple<string, string>("o)"," purple-circle.png"));


            Assert.AreEqual(
                ImageList.StringToImageList(Opener.FileToString(
                "/Users/Muse/Desktop/su19-mikaelMuseFrederik/SU19-exercises/TestSpaceTaxi/Levels/imagetest.txt")
                    ),x);

            
        }
    }
}


