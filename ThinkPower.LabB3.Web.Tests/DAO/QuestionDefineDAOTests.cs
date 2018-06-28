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
    public class QuestionDefineDAOTests
    {
        [TestMethod()]
        public void GetQuestionEnumerTest()
        {
            //arrange
            QuestionDefineDAO target = new QuestionDefineDAO();
            string expected = "請輸入您的手機號碼(格式為0912345678，將作為活動通知使用)";
            //act
            IEnumerable<QuestionDefineDO> actual;
            actual = target.GetQuestions("91800195-25B9-40A5-AA66-0C66F9363A79");
            //assert
            Assert.AreEqual(expected, actual.ElementAt(0).QuestionContent);
        }
    }
}