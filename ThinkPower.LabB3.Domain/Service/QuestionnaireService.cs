using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkPower.LabB3.Domain;
using ThinkPower.LabB3.DataAccess.DAO;
using ThinkPower.LabB3.DataAccess.DO;
using ThinkPower.LabB3.Domain.DTO;
using ThinkPower.LabB3.Domain.Entity.Question;
using NLog;

namespace ThinkPower.LabB3.Domain.Service
{
    public class QuestionnaireService
    {
        /// <summary>
        /// 計算問卷填答得分
        /// </summary>
        /// <param name="answer">問卷填答資料</param>
        /// <returns></returns>
        public QuestionnaireResult Calculate(QuestionnaireAnswer answer)
        {
            return null;
        }
        /// <summary>
        /// 取得有效的問卷資料
        /// </summary>
        /// <param name="id">問卷編號</param>
        /// <returns></returns>
        public Questionnaire GetActiveQuestionnaire(string id)
        {
            QuestionnaireDAO questionnaireDAO = new QuestionnaireDAO();
            QuestionnaireDO questionnaireDO = questionnaireDAO.Read(id);
            //DO傳給Entity
            QuestionnaireEntity questionnaireEntity = new QuestionnaireEntity();
            questionnaireEntity.MappingDO(questionnaireDO);
            //Entity傳給DTO
            Questionnaire questionnaire = new Questionnaire(questionnaireEntity);
            return questionnaire;
        }
        /// <summary>
        /// 取得問卷資料
        /// </summary>
        /// <param name="uid">問卷識別碼</param>
        /// <returns></returns>
        public Questionnaire GetQuestionnaire(string uid)
        {
            return null;
        }
    }
}
