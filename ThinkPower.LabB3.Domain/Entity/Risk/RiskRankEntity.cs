using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkPower.LabB3.DataAccess.DAO;
using ThinkPower.LabB3.DataAccess.DO;

namespace ThinkPower.LabB3.Domain.Entity.Risk
{

    /// <summary>
    /// 投資風險等級Entity類別
    /// </summary>
    public class RiskRankEntity : BaseEntity
    {
        /// <summary>
        /// 取得全部投資風險等級Entity的建構式
        /// </summary>
        public RiskRankEntity()
        {

            RiskRankDAO riskRankDAO = new RiskRankDAO();
            IEnumerable<RiskRankDO> riskRankDOs = riskRankDAO.GetAll();

            //foreach (RiskRankDO riskRankDO in riskRankDOs)
            //{
            //    riskRankDO
            //}

            //    //載入問卷Uid取得題目集合DOs
            //    RiskRankDetailDAO riskRankDetailDAO = new RiskRankDetailDAO();
            //IEnumerable<RiskRankDetailDO> riskRankDetailDOs = new 
            //    riskRankDetailDAO.GetRiskRankDetail(Uid);

            //List<QuestDefineEntity> questDefineEntitys = new List<QuestDefineEntity>();
            //foreach (RiskRankDO riskRankDO in riskRankDOs)
            //{
            //    QuestDefineEntity questDefineEntity = new QuestDefineEntity(questionDefineDO);
            //    questDefineEntitys.Add(questDefineEntity);
            //}
            //QuestDefineEntitys = questDefineEntitys;

        }
        /// <summary>
        /// 風險評估項目代號
        /// </summary>
        public string RiskEvaId { get; set; }
        /// <summary>
        /// 起始分數
        /// </summary>
        public int MinScore { get; set; }
        /// <summary>
        /// 截止分數
        /// </summary>
        public int MaxScore { get; set; }
        /// <summary>
        /// 投資屬性類型
        /// </summary>
        public string RiskRankKind { get; set; }
    }
}
