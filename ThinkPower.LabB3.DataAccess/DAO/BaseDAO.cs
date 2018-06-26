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
        /// 私有的資料庫連線物件
        /// </summary>
        private SqlConnection _dbConnection;

        /// <summary>
        /// 資料庫連線物件
        /// </summary>
        public SqlConnection DbConnection
        {
            get
            {
                if (_dbConnection == null)
                {
                    _dbConnection = GetConnection();
                }
                return _dbConnection;
            }
            set
            {
                DbConnection = value;
            }
        }
        
        /// <summary>
        /// 呼叫DbHelper取得資料庫連線物件
        /// </summary>
        /// <returns> SqlConnection物件 </returns>
        protected SqlConnection GetConnection()
        {
            string connKey = "LabB3";
            return DbHelper.GetConnection(connKey);
        }

        /// <summary>
        /// 取得資料筆數(抽象方法)
        /// </summary>
        /// <returns> 筆數 </returns>
        public abstract int Count();
    }
}
