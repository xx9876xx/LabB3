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
    /// <summary>
    /// 問卷題目定義存取類別
    /// </summary>
    public class QuestionDefineDAO : BaseDAO
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
                        ("SELECT COUNT(Uid) FROM QuestionDefine", cn);
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
        /// 查詢指定問卷之題目定義集合
        /// </summary>
        /// <param name="questId">問卷識別項</param>
        /// <returns> 問卷題目定義集合 </returns>
        public IEnumerable<QuestionDefineDO> GetQuestionEnumer(string questId)
        {
            try
            {
                List<QuestionDefineDO> listDO = new List<QuestionDefineDO>();
                using (SqlConnection cn = GetConnection())
                {
                    SqlCommand cmd = new SqlCommand
                        ("SELECT [Uid],[QuestUid],[QuestionId],[QuestionContent],[NeedAnswer]," +
                        "[AllowNaCondition],[AnswerType],[MinMultipleAnswers],[MaxMultipleAnswers]," +
                        "[SingleAnswerCondition],[CountScoreType],[Memo],[OrderSn]," +
                        "[CreateUserId],[CreateTime],[ModifyUserId],[ModifyTime] " +
                        "FROM QuestionDefine " +
                        "WHERE [QuestUid] = @questUid " +
                        "ORDER BY [OrderSn] ASC", cn);
                    cmd.Parameters.AddWithValue("@questUid", questId);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            QuestionDefineDO questionDefineDO = new QuestionDefineDO();
                            questionDefineDO.Uid = Guid.Parse(Convert.ToString(dr["Uid"]));
                            questionDefineDO.QuestUid = Guid.Parse(Convert.ToString(dr["QuestUid"]));
                            questionDefineDO.QuestionId = Convert.ToString(dr["QuestionId"]);
                            questionDefineDO.QuestionContent = Convert.ToString(dr["QuestionContent"]);
                            questionDefineDO.NeedAnswer = Convert.ToString(dr["NeedAnswer"]);
                            questionDefineDO.AllowNaCondition = Convert.ToString(dr["AllowNaCondition"]);
                            questionDefineDO.AnswerType = Convert.ToString(dr["AnswerType"]);
                            questionDefineDO.MinMultipleAnswers = Convert.IsDBNull(dr["MinMultipleAnswers"]) ? (int?)null : Convert.ToInt32(dr["MinMultipleAnswers"]);
                            questionDefineDO.MaxMultipleAnswers = Convert.IsDBNull(dr["MaxMultipleAnswers"]) ? (int?)null : Convert.ToInt32(dr["MaxMultipleAnswers"]);
                            questionDefineDO.SingleAnswerCondition = Convert.ToString(dr["SingleAnswerCondition"]);
                            questionDefineDO.CountScoreType = Convert.ToString(dr["CountScoreType"]);
                            questionDefineDO.Memo = Convert.ToString(dr["Memo"]);
                            questionDefineDO.OrderSn = Convert.IsDBNull(dr["OrderSn"]) ? (int?)null : Convert.ToInt32(dr["OrderSn"]);
                            questionDefineDO.CreateUserId = dr["CreateUserId"].ToString();
                            questionDefineDO.CreateTime = Convert.IsDBNull(dr["CreateTime"]) ? (DateTime?)null : Convert.ToDateTime(dr["CreateTime"]);
                            questionDefineDO.ModifyUserId = dr["ModifyUserId"].ToString();
                            questionDefineDO.ModifyTime = Convert.IsDBNull(dr["ModifyTime"]) ? (DateTime?)null : Convert.ToDateTime(dr["ModifyTime"]);
                            listDO.Add(questionDefineDO);
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
