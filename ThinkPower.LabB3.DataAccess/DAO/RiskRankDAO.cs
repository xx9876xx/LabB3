using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.ExceptionServices;

namespace ThinkPower.LabB3.DataAccess.DAO
{
    class RiskRankDAO : BaseDAO
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
                    SqlCommand sqlcmd = new SqlCommand("SELECT COUNT(Uid) FROM LabB3.dbo.RiskRank", cn);
                    cn.Open();
                    SqlDataReader reader = sqlcmd.ExecuteReader();
                    while ((reader.Read()))
                    {
                        if (!reader[0].Equals(DBNull.Value))
                        {
                            if (Int32.TryParse(reader[0].ToString(), out count))
                            {
                            }
                            else
                            {
                                logger.Error("執行Count方法時，無法轉換讀取資料為數字！");
                                throw new InvalidCastException("執行Count方法時，無法轉換讀取資料為數字！");
                            }
                        }
                    }
                    cn.Close();
                    return count;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                ExceptionDispatchInfo.Capture(ex).Throw();
                return 0;
            }

        }
    }
}
