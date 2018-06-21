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
        //private SqlConnection _dbConnection = DbHelper.GetConnection("LabB3");
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
                    if (DbConnection == null)
                    {
                        return GetConnection();
                    }
                    else
                        return DbConnection;
                }
                catch(Exception ex)
                {
                    throw new ApplicationException(ex.ToString());
                }
            }
            set
            {
                DbConnection = value;
            }
        }
        /// <summary>
        /// 取得資料筆數(抽象方法)
        /// </summary>
        /// <returns> 筆數 </returns>
        public abstract int Count();
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
        /// 幫助資料庫取出的Object轉成可為Null的DateTime物件
        /// </summary>
        /// <param name="dataReaderObj">資料庫取出的Object</param>
        /// <returns> DateTime?物件 </returns>
        protected DateTime? ObjectToNullableDateTime(Object dataReaderObj)
        {
            if (dataReaderObj.Equals(DBNull.Value))
                return null;
            else
            {
                return (DateTime)dataReaderObj;
            }
                    
        }
    }
}
