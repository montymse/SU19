using System;
using NUnit.Framework;
using SpaceTaxi_1;
using SpaceTaxi_1.Collisions;

namespace TestSpaceTaxi {
    [TestFixture]

    public class CollisionPlatformsTest {


        [Test]
        public void PlatformsTest() {

            string[] x = new []{string.Empty,"1"};
            
            Assert.AreEqual(
                CollisionPlatforms.Platforms("/Users/Muse/Desktop/" +
                                             "su19-mikaelMuseFrederik/SU19-exercises/" +
                                             "TestSpaceTaxi/Levels/placementtest.txt"),
                x);
        }
    }
}