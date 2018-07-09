using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.ExceptionServices;
using ThinkPower.LabB3.DataAccess.DO;

namespace ThinkPower.LabB3.DataAccess.DAO
{
    public class RiskRankDAO : BaseDAO
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// 取得資料筆數
        /// </summary>
        /// <returns> 筆數 </returns>
        public override int Count()
        {
            try
            {
                int count = 0;
                using (SqlConnection cn = DbConnection)
                {
                    SqlCommand cmd = new SqlCommand
                        ("SELECT COUNT(Uid) FROM RiskEvaluation", cn);
                    cn.Open();
                    count = (int)cmd.ExecuteScalar();
                }
                return count;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                ExceptionDispatchInfo.Capture(ex).Throw();
                return 0;
            }
        }

        /// <summary>
        /// 取得指定問卷識別項的問卷資料
        /// </summary>
        /// <param name="uid">問卷識別項</param>
        /// <returns> 問卷主檔DO物件 </returns>
        public IEnumerable<RiskRankDO> GetAll()
        {            
            try
            {
                List<RiskRankDO> riskRankDOs = new List<RiskRankDO>();
                using (SqlConnection cn = DbConnection)
                {
                    SqlCommand cmd = new SqlCommand
                        ("SELECT [Uid],[RiskEvaId],[MinScore],[MaxScore],[RiskRankKind]" +
                        "[CreateUserId],[CreateTime],[ModifyUserId],[ModifyTime]" +
                        "FROM RiskRank " , cn);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            RiskRankDO riskRankDO = new RiskRankDO();
                            riskRankDO.Uid = Guid.Parse(Convert.ToString(dr["Uid"]));
                            riskRankDO.RiskEvaId = Convert.ToString(dr["RiskEvaId"]);
                            riskRankDO.MinScore = Convert.IsDBNull(dr["MinScore"]) ? (int?)null : Convert.ToInt32(dr["MinScore"]);
                            riskRankDO.MaxScore = Convert.IsDBNull(dr["MaxScore"]) ? (int?)null : Convert.ToInt32(dr["MaxScore"]);
                            riskRankDO.RiskRankKind = Convert.ToString(dr["RiskRankKind"]);
                            riskRankDO.CreateUserId = Convert.ToString(dr["CreateUserId"]);
                            riskRankDO.CreateTime = Convert.IsDBNull(dr["CreateTime"]) ? (DateTime?)null : Convert.ToDateTime(dr["CreateTime"]);
                            riskRankDO.ModifyUserId = Convert.ToString(dr["ModifyUserId"]);
                            riskRankDO.ModifyTime = Convert.IsDBNull(dr["ModifyTime"]) ? (DateTime?)null : Convert.ToDateTime(dr["ModifyTime"]);
                            riskRankDOs.Add(riskRankDO);
                        }
                    }
                }
                return riskRankDOs;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                ExceptionDispatchInfo.Capture(ex).Throw();
                return null;
            }
        }

    }
}
