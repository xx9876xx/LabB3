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
    /// 問卷答題明細存取類別
    /// </summary>
    public class QuestionnaireAnswerDetailDAO : BaseDAO
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
                        ("SELECT COUNT(Uid) FROM QuestionnaireAnswerDetail", cn);
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
        public bool SetQuestionnaireData(QuestionnaireAnswerDO questionnaireData)
        {
            try
            {
                using (SqlConnection cn = GetConnection())
                {
                    SqlCommand cmd = new SqlCommand
                        ("INSERT INTO QuestionnaireAnswer" +
                        "([Uid],[QuestUid],[QuestAnswerId],[TesteeId],[QuestScore]," +
                        "[ActualScore],[TesteeSource],[CreateUserId],[CreateTime])" +
                        "VALUES (@Uid,@QuestUid,@QuestAnswerId,@TesteeId," +
                        "@QuestScore,@ActualScore,@TesteeSource,@CreateUserId,@CreateTime);", cn);

                    cmd.Parameters.AddWithValue("@Uid", questionnaireData.Uid);
                    cmd.Parameters.AddWithValue("@QuestUid", questionnaireData.QuestUid);
                    cmd.Parameters.AddWithValue("@QuestAnswerId", questionnaireData.QuestAnswerId);
                    cmd.Parameters.AddWithValue("@TesteeId", questionnaireData.TesteeId);
                    cmd.Parameters.AddWithValue("@QuestScore", questionnaireData.QuestScore);
                    cmd.Parameters.AddWithValue("@ActualScore", questionnaireData.ActualScore);
                    cmd.Parameters.AddWithValue("@TesteeSource", questionnaireData.TesteeSource);
                    cmd.Parameters.AddWithValue("@CreateUserId", questionnaireData.CreateUserId);
                    cmd.Parameters.AddWithValue("@CreateTime", questionnaireData.CreateTime);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                ExceptionDispatchInfo.Capture(ex).Throw();
                return false;
            }
        }

    }
}
