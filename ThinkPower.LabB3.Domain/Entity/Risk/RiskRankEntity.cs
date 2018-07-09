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
        /// 指定投資屬性類型將投資風險等級DO載入Entity
        /// </summary>
        /// <param name="riskRankKind">投資屬性類型(H:高,M:中,L:低)</param>
        public RiskRankEntity(string riskRankKind)
        {
            if (riskRankKind == null)
            {
                throw new ArgumentNullException(nameof(riskRankKind));
            }
            RiskRankDAO riskRankDAO = new RiskRankDAO();
            RiskRankDO riskRankDO = riskRankDAO.GetRiskRank(riskRankKind);
            Uid = riskRankDO.Uid;
            RiskEvaId = riskRankDO.RiskEvaId;
            MinScore = riskRankDO.MinScore;
            MaxScore = riskRankDO.MaxScore;
            RiskRankKind = riskRankDO.RiskRankKind;

            RiskRankDetailDAO riskRankDetailDAO = new RiskRankDetailDAO();
            RiskRankDetails = riskRankDetailDAO.GetRiskRankDetails(Uid);
        }


        /// <summary>
        /// 將投資風險等級DO資料載入Entity物件
        /// </summary>
        /// <param name="riskRankDO"> 投資風險等級DO </param>
        public RiskRankEntity(RiskRankDO riskRankDO)
        {
            if (riskRankDO == null)
            {
                throw new ArgumentNullException(nameof(riskRankDO));
            }
            Uid = riskRankDO.Uid;
            RiskEvaId = riskRankDO.RiskEvaId;
            MinScore = riskRankDO.MinScore;
            MaxScore = riskRankDO.MaxScore;
            RiskRankKind = riskRankDO.RiskRankKind;

            RiskRankDetailDAO riskRankDetailDAO = new RiskRankDetailDAO();
            RiskRankDetails = riskRankDetailDAO.GetRiskRankDetails(Uid);
        }
        /// <summary>
        /// 風險評估項目代號
        /// </summary>
        public string RiskEvaId { get; set; }

        /// <summary>
        /// 起始分數
        /// </summary>
        public int? MinScore { get; set; }

        /// <summary>
        /// 截止分數
        /// </summary>
        public int? MaxScore { get; set; }

        /// <summary>
        /// 投資屬性類型
        /// </summary>
        public string RiskRankKind { get; set; }

        //TODO 改成用string 不需要自創一個Entity
        /// <summary>
        /// 投資風險標的等級明細集合
        /// </summary>
        public IEnumerable<string> RiskRankDetails { get; set; }
    }
}
