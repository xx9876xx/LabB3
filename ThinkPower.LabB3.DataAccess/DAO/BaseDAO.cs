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
        private SqlConnection _DbConnection;
        /// <summary>
        /// SQL資料庫連線物件
        /// </summary>
        protected SqlConnection DbConnection
        {
            get
            {
                try
                {
                    _DbConnection.Open();
                    _DbConnection.Close();
                    _DbConnection.Dispose();
                    return _DbConnection;
                }
                catch(Exception ex)
                {
                    logger.Info("非有效連線由GetConnection()取得回傳" + ex);
                    return GetConnection();
                }
            }
            set
            {
                _DbConnection = value;
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
        public SqlConnection GetConnection()
        {
            string connKey = "LabB3"; 
            return DbHelper.GetConnection(connKey);
        }
    }
}
