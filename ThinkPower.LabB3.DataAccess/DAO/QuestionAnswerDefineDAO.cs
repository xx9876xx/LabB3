using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using ThinkPower.LabB3.DataAccess.DO;
using NLog;

namespace ThinkPower.LabB3.DataAccess.DAO
{
    /// <summary>
    /// 問卷答案項目定義存取類別
    /// </summary>
    public class QuestionAnswerDefineDAO : BaseDAO
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
                        ("SELECT COUNT(Uid) FROM QuestionAnswerDefine", cn);
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
        /// 查詢指定的題目之選項定義集合
        /// </summary>
        /// <param name="questionUid">問題識別項</param>
        /// <returns> 單題題目選項定義集合 </returns>
        public IEnumerable<QuestionAnswerDefineDO> GetAnswerItems(string questionUid)
        {
            try
            {
                List<QuestionAnswerDefineDO> listDO = new List<QuestionAnswerDefineDO>();
                using (SqlConnection cn = GetConnection())
                {
                    SqlCommand cmd = new SqlCommand
                        (" SELECT [Uid],[QuestionUid],[AnswerCode],[AnswerContent]," +
                        "[Memo],[HaveOtherAnswer],[NeedOtherAnswer],[Score],[OrderSn]," +
                        "[CreateUserId],[CreateTime],[ModifyUserId],[ModifyTime] " +
                        "FROM QuestionAnswerDefine " +
                        "WHERE [QuestionUid] = @questionUid " +
                        "ORDER BY [OrderSn] ASC", cn);
                    cmd.Parameters.AddWithValue("@questionUid", questionUid);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            QuestionAnswerDefineDO questionAnswerDefineDO = new QuestionAnswerDefineDO();
                            questionAnswerDefineDO.Uid = Guid.Parse(Convert.ToString(dr["Uid"]));
                            questionAnswerDefineDO.QuestionUid = Guid.Parse(Convert.ToString(dr["QuestionUid"]));
                            questionAnswerDefineDO.AnswerCode = Convert.ToString(dr["AnswerCode"]);
                            questionAnswerDefineDO.AnswerContent = Convert.ToString(dr["AnswerContent"]);
                            questionAnswerDefineDO.Memo = Convert.ToString(dr["Memo"]);
                            questionAnswerDefineDO.HaveOtherAnswer = Convert.ToString(dr["HaveOtherAnswer"]);
                            questionAnswerDefineDO.NeedOtherAnswer = Convert.ToString(dr["NeedOtherAnswer"]);
                            questionAnswerDefineDO.Score = Convert.IsDBNull(dr["Score"]) ? (int?)null : Convert.ToInt32(dr["Score"]);
                            questionAnswerDefineDO.OrderSn = Convert.IsDBNull(dr["OrderSn"]) ? (int?)null : Convert.ToInt32(dr["OrderSn"]);
                            questionAnswerDefineDO.CreateUserId = Convert.ToString(dr["CreateUserId"]);
                            questionAnswerDefineDO.CreateTime = Convert.IsDBNull(dr["CreateTime"]) ? (DateTime?)null : Convert.ToDateTime(dr["CreateTime"]);
                            questionAnswerDefineDO.ModifyUserId = Convert.ToString(dr["ModifyUserId"]);
                            questionAnswerDefineDO.ModifyTime = Convert.IsDBNull(dr["ModifyTime"]) ? (DateTime?)null : Convert.ToDateTime(dr["ModifyTime"]);
                            listDO.Add(questionAnswerDefineDO);
                        }
                    }
                }
                return listDO;
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
