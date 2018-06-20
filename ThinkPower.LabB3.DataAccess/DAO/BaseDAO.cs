using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkPower.LabB3.DataAccess.Helper;
using NLog;

namespace ThinkPower.LabB3.DataAccess.DAO
{
    /// <summary>
    /// 資料存取物件基底類別
    /// </summary>
    public abstract class BaseDAO
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// SQL資料庫連線物件(private)
        /// </summary>
        private SqlConnection _dbConnection;
        /// <summary>
        /// SQL資料庫連線物件
        /// </summary>
        public SqlConnection DbConnection
        {
            get
            {
                try
                {
                    //TODO getConnection  GetConnection開過之後要丟給DbConnection 開的工作交給別人
                    _dbConnection.Open();
                    _dbConnection.Close();
                    //_dbConnection.Dispose();
                    return _dbConnection;
                }
                catch(Exception ex)
                {
                    logger.Info("非有效連線由GetConnection()取得回傳" + ex);
                    //TODO 放這會檢查不到GetConnection()的Excpetion 導致Excpetion迴圈
                    return GetConnection();
                }
            }
            set
            {
                _dbConnection = value;
            }
        }     
        /// <summary>
        /// 取得資料筆數
        /// </summary>
        /// <returns> 筆數 </returns>
        public abstract int Count();
        /// <summary>
        /// 呼叫DbHelper取得資料庫連線物件
        /// </summary>
        /// <returns></returns>
        protected SqlConnection GetConnection()
        {
            string connKey = "LabB3"; 
            return DbHelper.GetConnection(connKey);
        }
    }
}
