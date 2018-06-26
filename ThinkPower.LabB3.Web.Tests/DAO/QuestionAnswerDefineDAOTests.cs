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
    public class QuestionAnswerDefineDAOTests
    {
        [TestMethod()]
        public void GetAnswerItemsTest()
        {
            //arrange
            QuestionAnswerDefineDAO target = new QuestionAnswerDefineDAO();
            string expected = "簡訊";
            //act
            IEnumerable<QuestionAnswerDefineDO> actual;
            actual = target.GetAnswerItems("32E385D6-3F93-4EFA-B8FA-A1C5B854E729");
            //assert
            Assert.AreEqual(expected, actual.ElementAt(0).AnswerContent);
        }
    }
}