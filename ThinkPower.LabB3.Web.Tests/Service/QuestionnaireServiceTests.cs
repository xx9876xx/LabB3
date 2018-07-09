using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThinkPower.LabB3.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkPower.LabB3.Domain.Entity.Question;

namespace ThinkPower.LabB3.Domain.Service.Tests
{
    [TestClass()]
    public class QuestionnaireServiceTests
    {
        [TestMethod()]
        public void CaculatePointTest()
        {
            //arrange
            QuestionnaireService qs = new QuestionnaireService();
            string questUid = "AF6FFD04-3C32-4AF4-85BC-54A2A008B9BB";
            QuestionnaireEntity questionnaireEntity = qs.GetQuestionnaire(questUid);
            //問卷計分方式 加總:1 取最大:2 取最小:3 平均:4
            questionnaireEntity.ScoreKind = "1";
            //題目計分方式預設為取最大:2  
            //需自己去主程式調整 (加總:1 取最大:2 取最小:3 平均:4)
            QuestionnaireAnswerEntity target = new QuestionnaireAnswerEntity();
            List<AnswerDetailEntity> targets = new List<AnswerDetailEntity>();
            AnswerDetailEntity answer0 = new AnswerDetailEntity()
            {
                QuestionUid = Guid.Parse("F9DD6938-2DBD-48AF-9D44-428DCB2AC5D6"),
                QuestionId = "Q001",
                AnswerCode = "1",
                Score = 1
            };
            targets.Add(answer0);
            AnswerDetailEntity answer1 = new AnswerDetailEntity()
            {
                QuestionUid = Guid.Parse("E33802B8-8FE3-4213-BF25-CCF08EF653D9"),
                QuestionId = "Q002",
                AnswerCode = "1",
                Score = 1
            };
            targets.Add(answer1);
            AnswerDetailEntity answer2 = new AnswerDetailEntity()
            {
                QuestionUid = Guid.Parse("268B8A9D-AC0E-42A2-BEB6-08BC8629DB6B"),
                QuestionId = "Q003",
                AnswerCode = "1",
                Score = 1
            };
            targets.Add(answer2);
            AnswerDetailEntity answer3 = new AnswerDetailEntity()
            {
                QuestionUid = Guid.Parse("268B8A9D-AC0E-42A2-BEB6-08BC8629DB6B"),
                QuestionId = "Q003",
                AnswerCode = "3",
                Score = 3
            };
            targets.Add(answer3);
            AnswerDetailEntity answer4 = new AnswerDetailEntity()
            {
                QuestionUid = Guid.Parse("268B8A9D-AC0E-42A2-BEB6-08BC8629DB6B"),
                QuestionId = "Q003",
                AnswerCode = "5",
                Score = 5
            };
            targets.Add(answer4);
            AnswerDetailEntity answer5 = new AnswerDetailEntity()
            {
                QuestionUid = Guid.Parse("4EB9A231-9C57-4AE6-AA25-C2D586AA9EA4"),
                QuestionId = "Q004",
                AnswerCode = "1",
                Score = 1
            };
            targets.Add(answer5);
            AnswerDetailEntity answer6 = new AnswerDetailEntity()
            {
                QuestionUid = Guid.Parse("43D9FD48-BB37-4A78-A400-BC79A35856C3"),
                QuestionId = "Q005",
                AnswerCode = "1",
                Score = 1
            };
            targets.Add(answer6);
            AnswerDetailEntity answer7 = new AnswerDetailEntity()
            {
                QuestionUid = Guid.Parse("9FD91D96-F9C2-445E-B3CE-CD3EBF24478C"),
                QuestionId = "Q006",
                AnswerCode = "1",
                Score = 1
            };
            targets.Add(answer7);
            AnswerDetailEntity answer8 = new AnswerDetailEntity()
            {
                QuestionUid = Guid.Parse("FC65A8F3-0685-4CBA-A591-EEEA961E00B8"),
                QuestionId = "Q007",
                AnswerCode = "1",
                Score = 1
            };
            targets.Add(answer8);
            target.Questions = targets;
            QuestionnaireAnswerEntity expected = new QuestionnaireAnswerEntity();
            //ScoreKind:1 CountScoreType:2
            expected.ActualScore = 1 + 1 + 5 + 1 + 1 + 1 + 1;

            //ScoreKind:2 CountScoreType:2
            //expected.ActualScore = 5;

            //ScoreKind:3 CountScoreType:2
            //expected.ActualScore = 1;

            //ScoreKind:4 CountScoreType:2
            //expected.ActualScore = (int)(1 + 1 + 5 + 1 + 1 + 1 + 1)/7;

            //ScoreKind:1 CountScoreType:1
            //expected.ActualScore = 1 + 1 + 1 + 3 + 5 + 1 + 1 + 1 + 1;

            //ScoreKind:1 CountScoreType:3
            //expected.ActualScore = 1 + 1 + 1 + 1 + 1 + 1 + 1;

            //ScoreKind:1 CountScoreType:4
            //expected.ActualScore = 1 + 1 + 3 + 1 + 1 + 1 + 1;
            //act

            //要測試時請改為public方法
            //QuestionnaireAnswerEntity actual = qs.CaculatePoint(target, questionnaireEntity);
            //assert
            //Assert.AreEqual(expected.ActualScore, actual.ActualScore);
        }

        [TestMethod()]
        public void ConditionCheckTest()
        {
            //arrange
            QuestionnaireService qs = new QuestionnaireService();
            string target = "{\"Conditions\":[{\"QuestionId\": \"Q001\", \"AnswerCode\": [\"1\"]}, {\"QuestionId\": \"Q002\", \"AnswerCode\": [\"1\"]}]}";
            string target1 = "{\"Conditions\":[{\"QuestionId\": \"Q003\", \"AnswerCode\": [\"1\", \"2\", \"6\"]}]}";

            

            IEnumerable<AnswerDetailEntity> questions;
            List<AnswerDetailEntity> targets = new List<AnswerDetailEntity>();
            AnswerDetailEntity answer0 = new AnswerDetailEntity()
            {
                QuestionUid = Guid.Parse("F9DD6938-2DBD-48AF-9D44-428DCB2AC5D6"),
                QuestionId = "Q001",
                AnswerCode = "1",
                Score = 1
            };
            targets.Add(answer0);
            AnswerDetailEntity answer1 = new AnswerDetailEntity()
            {
                QuestionUid = Guid.Parse("E33802B8-8FE3-4213-BF25-CCF08EF653D9"),
                QuestionId = "Q002",
                AnswerCode = "1",
                Score = 1
            };
            targets.Add(answer1);
            AnswerDetailEntity answer2 = new AnswerDetailEntity()
            {
                QuestionUid = Guid.Parse("268B8A9D-AC0E-42A2-BEB6-08BC8629DB6B"),
                QuestionId = "Q003",
                AnswerCode = "1",
                Score = 1
            };
            targets.Add(answer2);
            AnswerDetailEntity answer3 = new AnswerDetailEntity()
            {
                QuestionUid = Guid.Parse("268B8A9D-AC0E-42A2-BEB6-08BC8629DB6B"),
                QuestionId = "Q003",
                AnswerCode = "2",
                Score = 2
            };
            targets.Add(answer3);
            AnswerDetailEntity answer4 = new AnswerDetailEntity()
            {
                QuestionUid = Guid.Parse("268B8A9D-AC0E-42A2-BEB6-08BC8629DB6B"),
                QuestionId = "Q003",
                AnswerCode = "6",
                Score = 1
            };
            targets.Add(answer4);
            AnswerDetailEntity answer5 = new AnswerDetailEntity()
            {
                QuestionUid = Guid.Parse("4EB9A231-9C57-4AE6-AA25-C2D586AA9EA4"),
                QuestionId = "Q004",
                AnswerCode = "1",
                Score = 1
            };
            targets.Add(answer5);
            AnswerDetailEntity answer6 = new AnswerDetailEntity()
            {
                QuestionUid = Guid.Parse("43D9FD48-BB37-4A78-A400-BC79A35856C3"),
                QuestionId = "Q005",
                AnswerCode = "1",
                Score = 1
            };
            targets.Add(answer6);
            AnswerDetailEntity answer7 = new AnswerDetailEntity()
            {
                QuestionUid = Guid.Parse("9FD91D96-F9C2-445E-B3CE-CD3EBF24478C"),
                QuestionId = "Q006",
                AnswerCode = "1",
                Score = 1
            };
            targets.Add(answer7);
            AnswerDetailEntity answer8 = new AnswerDetailEntity()
            {
                QuestionUid = Guid.Parse("FC65A8F3-0685-4CBA-A591-EEEA961E00B8"),
                QuestionId = "Q007",
                AnswerCode = "1",
                Score = 1
            };
            targets.Add(answer8);
            questions = targets;

            bool actual = qs.ConditionCheck(target1, questions);
            bool expected = true;
            //assert
            Assert.AreEqual(expected, actual);

        }
    }
}