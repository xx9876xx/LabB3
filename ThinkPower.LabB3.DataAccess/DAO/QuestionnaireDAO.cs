using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkPower.LabB3.DataAccess.DAO;
using NLog;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.ExceptionServices;

namespace ThinkPower.LabB3.DataAccess.DAO
{
    /// <summary>
    /// 問卷資料存取類別
    /// </summary>
    public class QuestionnaireDAO : BaseDAO
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
                        ("SELECT * FROM [LabB3].[dbo].[Questionnaire]", cn);
                    dataAdapter.Fill(ds, "Questionnaire");
                    dt = ds.Tables["Questionnaire"];
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
        /// <summary>
        /// 查詢整筆
        /// </summary>
        /// <returns></returns>
        public DataTable ReadAll()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection cn = base.DbConnection)
                {
                    DataSet ds = new DataSet();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter
                        ("SELECT * FROM [LabB3].[dbo].[Questionnaire]", cn);
                    dataAdapter.Fill(ds, "Questionnaire");
                    dt = ds.Tables["Questionnaire"];
                    return dt;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                ExceptionDispatchInfo.Capture(ex).Throw();
                return null;
            }

        }

        /// <summary>
        /// 查詢某
        /// </summary>
        /// <returns></returns>
        public DataTable Read(string id)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection cn = base.DbConnection)
                {
                    DataSet ds = new DataSet();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter
                        ("SELECT * FROM [LabB3].[dbo].[Questionnaire] WHERE [QuestId] = '"+id+"'", cn);
                    dataAdapter.Fill(ds, "Questionnaire");
                    dt = ds.Tables["Questionnaire"];
                    return dt;
                }
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

