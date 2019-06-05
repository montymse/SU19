using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using SpaceTaxi_1;
namespace TestSpaceTaxi.TestLevelParser {
    [TestFixture]

    public class TestPlacement {
        [Test]
        
        
        public void FindPlacementAndImage() {
            List<Tuple<Tuple<float, float>,string>> x = new List<Tuple<Tuple<float, float>,
                string>>();
            x.Add(new Tuple<Tuple<float, float>,
                string>(new Tuple<float, float>((float)8/40,1f-(float)6/23), " yellow-stick.png"));
            x.Add(new Tuple<Tuple<float, float>,
                string>(new Tuple<float, float>((float)8/40,1f-(float)7/23), " yellow-stick.png"));
            x.Add(new Tuple<Tuple<float, float>,
                string>(new Tuple<float, float>((float)8/40,1f-(float)8/23), " yellow-stick.png"));
            x.Add(new Tuple<Tuple<float, float>,
                string>(new Tuple<float, float>((float)8/40,1f-(float)9/23), " yellow-stick.png"));
            x.Add(new Tuple<Tuple<float, float>,
                string>(new Tuple<float, float>((float)8/40,1f-(float)10/23), " yellow-stick.png"));
            x.Add(new Tuple<Tuple<float, float>,
                string>(new Tuple<float, float>((float)8/40,1f-(float)11/23), " yellow-stick.png"));

           

            Assert.AreEqual(
                Placement.FindPlacementAndImage(
                    "/Users/Muse/Desktop/su19-mikaelMuseFrederik/SU19-exercises/" +
                    "TestSpaceTaxi/Levels/placementtest.txt"),
               x
                );

        }
    }
}