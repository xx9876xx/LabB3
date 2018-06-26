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
using System.Runtime.ExceptionServices;

namespace ThinkPower.LabB3.Domain.Service
{
    //TODO 補interface的公開方法
    public class QuestionnaireService
    {
        /// <summary>
        /// 計算問卷填答得分
        /// </summary>
        /// <param name="answer">問卷填答資料</param>
        /// <returns></returns>
        public QuestionnaireResultEntity Calculate(QuestionnaireAnswerEntity answer)
        {
            return null;
        }

        /// <summary>
        /// 取得有效的問卷資料
        /// </summary>
        /// <param name="id">問卷編號</param>
        /// <returns></returns>
        public QuestionnaireEntity GetActiveQuestionnaire(string id)
        {
            try
            {
                //問卷主檔DAO生資料傳回主檔DO
                QuestionnaireDAO questionnaireDAO = new QuestionnaireDAO();
                QuestionnaireDO questionnaireDO = questionnaireDAO.GetQuestionnaireData(id);
                //將DO資料載入Entity
                QuestionnaireEntity questionnaireEntity = new QuestionnaireEntity(questionnaireDO);

                //題目選項字典
                Dictionary<string, IEnumerable<AnswerDefineEntity>> questAnswerPairs = new Dictionary<string, IEnumerable<AnswerDefineEntity>>();

                //問卷題目DAO生資料傳回題目集合DO
                QuestionDefineDAO questionDefineDAO = new QuestionDefineDAO();
                IEnumerable<QuestionDefineDO> questionEnumer = questionDefineDAO.GetQuestionEnumer(Convert.ToString(questionnaireEntity.Uid));

                //單一問卷的題目集合宣告
                List<QuestDefineEntity> questDefineEntitieList = new List<QuestDefineEntity>();
                //將DO資料載入Entity
                foreach (QuestionDefineDO questDefDO in questionEnumer)
                {
                    QuestDefineEntity questDefineEntity = new QuestDefineEntity(questDefDO);
                    questDefineEntitieList.Add(questDefineEntity);
                }
                //TODO 載入資料的方法要放在Entity裡面 Entity自己呼叫DAO 自己載入

                //選項DAO生資料傳回選項集合DO
                QuestionAnswerDefineDAO questionAnswerDefineDAO = new QuestionAnswerDefineDAO();
                foreach (QuestDefineEntity questDefEntity in questDefineEntitieList)
                {
                    //單一題目的選項集合
                    IEnumerable<QuestionAnswerDefineDO> questionAnswerEnumer = questionAnswerDefineDAO.GetAnswerItems(Convert.ToString(questDefEntity.Uid));
                    //將DO資料載入Entity
                    List<AnswerDefineEntity> answerDefineEntityList = new List<AnswerDefineEntity>();
                    foreach (QuestionAnswerDefineDO questAnswerDO in questionAnswerEnumer)
                    {
                        AnswerDefineEntity answerDefineEntity = new AnswerDefineEntity(questAnswerDO);
                        answerDefineEntityList.Add(answerDefineEntity);
                    }
                    questAnswerPairs.Add(Convert.ToString(questDefEntity.QuestionId), answerDefineEntityList);

                }
                //完成題目選項字典
                questionnaireEntity.QuestAnswerPairs = questAnswerPairs;
                questionnaireEntity.QuestDefEnumer = questDefineEntitieList;


                return questionnaireEntity;
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
                return null;
            }
            
        }

        /// <summary>
        /// 取得問卷資料
        /// </summary>
        /// <param name="uid">問卷識別碼</param>
        /// <returns></returns>
        public QuestionnaireDTO GetQuestionnaire(string uid)
        {
            return null;
        }
    }
}
