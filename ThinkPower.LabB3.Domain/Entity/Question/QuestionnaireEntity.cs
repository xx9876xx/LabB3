using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkPower.LabB3.DataAccess.DAO;
using ThinkPower.LabB3.DataAccess.DO;

namespace ThinkPower.LabB3.Domain.Entity.Question
{
    /// <summary>
    /// 問卷Entity類別
    /// </summary>
    public class QuestionnaireEntity : BaseEntity
    {
        /// <summary>
        /// 指定問卷識別項生成問卷Entity的建構式
        /// </summary>
        /// <param name="uid">問卷識別項</param>
        public QuestionnaireEntity(Guid uid)
        {
            if (uid == null)
            {
                throw new ArgumentNullException();
            }
            QuestionnaireDAO questionnaireDAO = new QuestionnaireDAO();
            QuestionnaireDO questionnaireDO = questionnaireDAO.GetQuestionnaireData(uid);
            GenerateEntity(questionnaireDO);
            //載入問卷Uid取得題目集合DOs
            QuestionDefineDAO questionDefineDAO = new QuestionDefineDAO();
            IEnumerable<QuestionDefineDO> QuestionDefineDOs = questionDefineDAO.GetQuestions(Convert.ToString(Uid));
            List<QuestDefineEntity> questDefineEntitys = new List<QuestDefineEntity>();
            foreach (QuestionDefineDO questionDefineDO in QuestionDefineDOs)
            {
                QuestDefineEntity questDefineEntity = new QuestDefineEntity(questionDefineDO);
                questDefineEntitys.Add(questDefineEntity);
            }
            QuestDefineEntitys = questDefineEntitys;

        }

        /// <summary>
        /// 指定問卷編號生成問卷Entity的建構式
        /// </summary>
        /// <param name="id"> 問卷編號 </param>
        public QuestionnaireEntity(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            QuestionnaireDAO questionnaireDAO = new QuestionnaireDAO();
            QuestionnaireDO questionnaireDO = questionnaireDAO.GetQuestionnaireData(id);
            GenerateEntity(questionnaireDO);
            //載入問卷Uid取得題目集合DOs
            QuestionDefineDAO questionDefineDAO = new QuestionDefineDAO();
            IEnumerable<QuestionDefineDO> QuestionDefineDOs = questionDefineDAO.GetQuestions(Convert.ToString(Uid));
            List<QuestDefineEntity> questDefineEntitys = new List<QuestDefineEntity>();
            foreach (QuestionDefineDO questionDefineDO in QuestionDefineDOs)
            {
                QuestDefineEntity questDefineEntity = new QuestDefineEntity(questionDefineDO);
                questDefineEntitys.Add(questDefineEntity);
            }
            QuestDefineEntitys = questDefineEntitys;

        }

        /// <summary>
        /// 問卷編號
        /// </summary>
        public string QuestId { get; set; }
        /// <summary>
        /// 問卷版本
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 問卷種類
        /// </summary>
        public string Kind { get; set; }
        /// <summary>
        /// 問卷名稱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 備註說明
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime? Ondate { get; set; }
        /// <summary>
        /// 生效截止日期
        /// </summary>
        public DateTime? Offdate { get; set; }
        /// <summary>
        /// 是否計分
        /// </summary>
        public string NeedScore { get; set; }
        /// <summary>
        /// 問卷總分數
        /// </summary>
        public int? QuestScore { get; set; }
        /// <summary>
        /// 計分方式
        /// </summary>
        public string ScoreKind { get; set; }
        /// <summary>
        /// 問卷頁首底圖網址
        /// </summary>
        public string HeadBackgroundImg { get; set; }
        /// <summary>
        /// 問卷頁首描敍內容
        /// </summary>
        public string HeadDescription { get; set; }
        /// <summary>
        /// 問卷頁尾描敍內容
        /// </summary>
        public string FooterDescription { get; set; }
        /// <summary>
        /// 題目集合
        /// </summary>
        public IEnumerable<QuestDefineEntity> QuestDefineEntitys { get; set; }
        
        /// <summary>
        /// 將DO物件載入Entity物件
        /// </summary>
        /// <param name="dataObject">問卷主檔DO物件</param>
        /// <returns>載入成功/失敗</returns>
        private void GenerateEntity(QuestionnaireDO dataObject)
        {
            Uid = dataObject.Uid;
            CreateUserId = dataObject.CreateUserId;
            CreateTime = dataObject.CreateTime;
            ModifyUserId = dataObject.ModifyUserId;
            ModifyTime = dataObject.ModifyTime;

            QuestId = dataObject.QuestId;
            Version = dataObject.Version;
            Kind = dataObject.Kind;
            Name = dataObject.Name;
            Memo = dataObject.Memo;
            Ondate = dataObject.Ondate;
            Offdate = dataObject.Offdate;
            NeedScore = dataObject.NeedScore;
            QuestScore = dataObject.QuestScore;
            ScoreKind = dataObject.ScoreKind;
            HeadBackgroundImg = dataObject.HeadBackgroundImg;
            HeadDescription = dataObject.HeadDescription;
            FooterDescription = dataObject.FooterDescription;
        }
    }
}
