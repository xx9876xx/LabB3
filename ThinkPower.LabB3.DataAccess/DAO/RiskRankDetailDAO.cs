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
    class RiskRankDetailDAO : BaseDAO
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
                DataTable dt = new DataTable();
                using (SqlConnection cn = base.DbConnection)
                {
                    DataSet ds = new DataSet();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter
                        ("SELECT * FROM [LabB3].[dbo].[RiskRankDetail]", cn);
                    dataAdapter.Fill(ds, "RiskRankDetail");
                    dt = ds.Tables["RiskRankDetail"];
                    return dt.Rows.Count;
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
