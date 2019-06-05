using DIKUArcade.Math;
using NUnit.Framework;
using SpaceTaxi_1;

namespace TestSpaceTaxi {
    [TestFixture]
    public class TestPhysics {
        private SpaceTaxi_1.Physics physics;
        [SetUp]
        public void Init() {
            physics = new SpaceTaxi_1.Physics(1000);
        }

        [Test]
        public void TestGravity() {
            Vec2F oldVelocity = physics.GetRawVelocity();
            physics.UpdateVelocity();
            Vec2F newVelocity = physics.GetRawVelocity();
            
            Assert.Greater(oldVelocity.Y, newVelocity.Y);
        }
        
        [Test]
        public void TestApplyForceLeft() {
            Vec2F oldVelocity = physics.GetRawVelocity();
            physics.ApplyForce(Physics.ForceDirection.Left, 4000);
            Vec2F newVelocity = physics.GetRawVelocity();
            
            Assert.Greater(oldVelocity.X, newVelocity.X);
        }
        
        [Test]
        public void TestApplyForceRight() {
            Vec2F oldVelocity = physics.GetRawVelocity();
            physics.ApplyForce(Physics.ForceDirection.Right, 4000);
            Vec2F newVelocity = physics.GetRawVelocity();
            
            Assert.Greater(newVelocity.X, oldVelocity.X);
        }
        
        [Test]
        public void TestApplyForceDown() {
            Vec2F oldVelocity = physics.GetRawVelocity();
            physics.ApplyForce(Physics.ForceDirection.Down, 4000);
            Vec2F newVelocity = physics.GetRawVelocity();
            
            Assert.Greater(oldVelocity.Y, newVelocity.Y);
        }
        
        [Test]
        public void TestApplyForceUp() {
            Vec2F oldVelocity = physics.GetRawVelocity();
            physics.ApplyForce(Physics.ForceDirection.Up, 4000);
            Vec2F newVelocity = physics.GetRawVelocity();
            
            Assert.Greater(newVelocity.Y, oldVelocity.Y);
        }

        [Test]
        public void TestGrounded() {
            Vec2F oldVelocity = physics.GetRawVelocity();
            physics.IsGrounded = true;
            physics.UpdateVelocity();
            Vec2F newVelocity = physics.GetRawVelocity();
            
            Assert.AreEqual(newVelocity.Y, oldVelocity.Y);
        }
    }
}