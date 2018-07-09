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
    /// 問卷答題主檔存取類別
    /// </summary>
    public class QuestionnaireAnswerDAO : BaseDAO
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
                        ("SELECT COUNT(Uid) FROM QuestionnaireAnswer", cn);
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
        /// 儲存問卷答題主檔資料
        /// </summary>
        /// <param name="answer"> 問卷答題主檔DO </param>
        /// <returns> 問卷填答主檔Uid </returns>
        public string Insert(QuestionnaireAnswerDO answer)
        {
            try
            {
                //問卷答題主檔Uid
                Guid answerUid = Guid.NewGuid();

                using (SqlConnection cn = DbConnection)
                {
                    SqlCommand cmd = new SqlCommand
                        ("INSERT INTO QuestionnaireAnswer" +
                        "([Uid],[QuestUid],[QuestAnswerId],[TesteeId],[QuestScore]," +
                        "[ActualScore],[TesteeSource],[CreateUserId],[CreateTime])" +
                        "VALUES (@Uid,@QuestUid,@QuestAnswerId,@TesteeId," +
                        "@QuestScore,@ActualScore,@TesteeSource,@CreateUserId,@CreateTime);", cn);
                    
                    cmd.Parameters.Add("@Uid", SqlDbType.UniqueIdentifier);
                    cmd.Parameters["@Uid"].Value = answerUid;

                    cmd.Parameters.Add("@QuestUid", SqlDbType.UniqueIdentifier);
                    cmd.Parameters["@QuestUid"].Value = answer.QuestUid;

                    cmd.Parameters.Add("@QuestAnswerId", SqlDbType.VarChar);
                    cmd.Parameters["@QuestAnswerId"].Value = answer.QuestAnswerId ?? (object)DBNull.Value;

                    cmd.Parameters.Add("@TesteeId", SqlDbType.VarChar);
                    cmd.Parameters["@TesteeId"].Value = answer.TesteeId ?? (object)DBNull.Value;

                    cmd.Parameters.Add("@QuestScore", SqlDbType.Int);
                    cmd.Parameters["@QuestScore"].Value = answer.QuestScore ?? (object)DBNull.Value;

                    cmd.Parameters.Add("@ActualScore", SqlDbType.Int);
                    cmd.Parameters["@ActualScore"].Value = answer.ActualScore ?? (object)DBNull.Value;

                    cmd.Parameters.Add("@TesteeSource", SqlDbType.VarChar);
                    cmd.Parameters["@TesteeSource"].Value = answer.TesteeSource ?? (object)DBNull.Value;

                    cmd.Parameters.Add("@CreateUserId", SqlDbType.VarChar);
                    cmd.Parameters["@CreateUserId"].Value = answer.CreateUserId ?? (object)DBNull.Value;

                    cmd.Parameters.Add("@CreateTime", SqlDbType.DateTime);
                    cmd.Parameters["@CreateTime"].Value = answer.CreateTime ?? (object)DBNull.Value;

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    
                }
                return answerUid.ToString();
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
