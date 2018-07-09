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
        /// 取得指定投資風險等級識別項的投資風險標的等級明細
        /// </summary>
        /// <param name="riskRankUid">投資風險等級識別項</param>
        /// <returns> 投資風險標的等級明細DO物件 </returns>
        public RiskRankDetailDO GetRiskRankDetail(Guid riskRankUid)
        {
            if (riskRankUid == null)
            {
                throw new ArgumentNullException(nameof(riskRankUid));
            }
            try
            {
                RiskRankDetailDO riskRankDetailDO = new RiskRankDetailDO();
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
                            riskRankDetailDO.Uid = Guid.Parse(Convert.ToString(dr["Uid"]));
                            riskRankDetailDO.RiskRankUid = Guid.Parse(Convert.ToString(dr["RiskRankUid"]));
                            riskRankDetailDO.ProfitRiskRank = Convert.ToString(dr["ProfitRiskRank"]);
                            riskRankDetailDO.IsEffective = Convert.ToString(dr["IsEffective"]);
                            riskRankDetailDO.CreateUserId = Convert.ToString(dr["CreateUserId"]);
                            riskRankDetailDO.CreateTime = Convert.IsDBNull(dr["CreateTime"]) ? (DateTime?)null : Convert.ToDateTime(dr["CreateTime"]);
                            riskRankDetailDO.ModifyUserId = Convert.ToString(dr["ModifyUserId"]);
                            riskRankDetailDO.ModifyTime = Convert.IsDBNull(dr["ModifyTime"]) ? (DateTime?)null : Convert.ToDateTime(dr["ModifyTime"]);
                        }
                    }
                }
                return riskRankDetailDO;
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
