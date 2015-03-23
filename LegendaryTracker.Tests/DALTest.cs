using System;
using LegendaryTracker.Services;
using NUnit.Framework;

namespace LegendaryTracker.Tests
{
    [TestFixture]
    public class DALTest
    {
        public readonly ExcelDAL _dal;

        public DALTest()
        {
            HttpContextFactory.SetCurrentContext(Helpers.GetMockedHttpContext());
            _dal = new ExcelDAL();
        }

        [Test]
        public void GetUserReturnsUsername()
        {
            //Arrange

            //Act
            var result =_dal.GetUser("rc7502");

            //Assert
            Assert.IsNotNull(result);
        }


    }
}
