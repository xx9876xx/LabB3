using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThinkPower.LabB3.DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkPower.LabB3.DataAccess.DAO.Tests
{
    [TestClass()]
    public class QuestionnaireDAOTests
    {
        [TestMethod()]
        public void CountTest()
        {
            //arrange
            QuestionnaireDAO target = new QuestionnaireDAO();
            int expected = 3;
            //act
            int actual;
            actual = target.Count();
            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}