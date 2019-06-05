using System;
using NUnit.Framework;
using SpaceTaxi_1;
using SpaceTaxi_1.Customer;

namespace TestSpaceTaxi {
    [TestFixture]

    public class CustomerInfoTest {

        [SetUp]
        [Test]
        public void SplitCustomerInfoTest() {
            string[] x = new[] {"Customer:","Alice","10","1","^J","10","100"};


                           
            Assert.AreEqual(CustomerInfo.SplitCustomerInfo("/Users/Muse/Desktop/"+
        "su19-mikaelMuseFrederik/SU19-exercises/TestSpaceTaxi/Levels/placementtest.txt"),x);
        }
        
    }
}