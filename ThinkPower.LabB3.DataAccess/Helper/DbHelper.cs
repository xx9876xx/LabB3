using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using static System.Net.Mime.MediaTypeNames;

namespace ThinkPower.LabB3.DataAccess.Helper
{
    /// <summary>
    /// 指定連線資料庫類別
    /// </summary>
    public class DbHelper
    {
        /// <summary>
        /// 指定連線資料庫
        /// </summary>
        /// <param name="connKey">指定連線資料庫鍵值</param>
        /// <returns>回傳資料庫連線物件</returns>
        public static SqlConnection GetConnection(string connKey)
        {

            if (String.IsNullOrEmpty(connKey))
            {
                throw new ArgumentNullException("connKey參數不存在");
            }
            
            try
            {
                string connString;
                if (HttpRuntime.AppDomainAppId != null)
                {
                    //is web app
                    //connString = WebConfigurationManager.ConnectionStrings[connKey].ConnectionString;
                }
                else
                {
                    // not web app
                    // Configuration config = ConfigurationManager.OpenMappedExeConfiguration(System.AppDomain.CurrentDomain.BaseDirectory+ "\\Web.config");
                    // connString = config.ConnectionStrings.ConnectionStrings[connKey].ConnectionString;
                }
                //WebContext webContext = (WebContext)config.EvaluationContext.HostingContext;
                //TODO 要連到Web專案的Web.config，先讓DLL知道自己在哪才能連Manager
                connString = @"Server=(LocalDB)\MSSQLLocalDB;Initial Catalog=LabB3;User ID=labap;Password=q1w1e1r1t1;;Persist Security Info=False";
                //string connString = WebConfigurationManager.ConnectionStrings[connKey].ConnectionString;
               
                SqlConnection conn = new SqlConnection(connString);

                return conn;
            }
            catch (Exception ex)
            {                
                ExceptionDispatchInfo.Capture(ex).Throw();
                return null;
            }

        }
    }
}
