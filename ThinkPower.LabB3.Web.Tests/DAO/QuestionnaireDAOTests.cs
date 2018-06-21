using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThinkPower.LabB3.DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkPower.LabB3.DataAccess.DO;

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

        [TestMethod()]
        public void ReadAllTest()
        {
            //arrange
            QuestionnaireDAO target = new QuestionnaireDAO();
            Guid expected = Guid.Parse("91800195-25B9-40A5-AA66-0C66F9363A79");
            //act
            List<QuestionnaireDO> actual;
            actual = target.ReadAll();
            //assert
            Assert.AreEqual(expected, actual[0].Uid);
        }
    }
}