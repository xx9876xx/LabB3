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
    public class RiskRankDetailDAO : BaseDAO
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
        /// 取得指定投資風險等級識別項的投資風險標的等級明細集合
        /// </summary>
        /// <param name="riskRankUid">投資風險等級識別項</param>
        /// <returns> 投資風險標的等級明細DO物件 </returns>
        public IEnumerable<string> GetRiskRankDetails(Guid riskRankUid)
        {
            if (riskRankUid == null)
            {
                throw new ArgumentNullException(nameof(riskRankUid));
            }
            try
            {
                List<string> profitRiskRanks = new List<string>();
                using (SqlConnection cn = DbConnection)
                {
                    SqlCommand cmd = new SqlCommand
                        ("SELECT [Uid],[RiskRankUid],[ProfitRiskRank],[IsEffective]" +
                        "[CreateUserId],[CreateTime],[ModifyUserId],[ModifyTime] " +
                        "FROM RiskRankDetail " +
                        "WHERE [RiskRankUid] = @RiskRankUid ", cn);
                    cmd.Parameters.AddWithValue("@RiskRankUid", riskRankUid.ToString());
                    cn.Open();
                    
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string profitRiskRank = Convert.ToString(dr["ProfitRiskRank"]);
                            profitRiskRanks.Add(profitRiskRank);
                        }
                    }
                }
                return profitRiskRanks;
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
