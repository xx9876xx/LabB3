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
        /// <returns> true:儲存成功 false:儲存失敗 </returns>
        //TODO 命名在調整不要用Set 有點廣義 處理單一資料 命名可以直接用Insert不用後面的Data名稱
        //TODO 要回傳的東西可以判斷影響列數 用bool有點沒意義 也可以直接用void 失敗直接接Exception
        public void Insert(QuestionnaireAnswerDO answer)
        {
            try
            {
                using (SqlConnection cn = DbConnection)
                {
                    SqlCommand cmd = new SqlCommand
                        ("INSERT INTO QuestionnaireAnswer" +
                        "([Uid],[QuestUid],[QuestAnswerId],[TesteeId],[QuestScore]," +
                        "[ActualScore],[TesteeSource],[CreateUserId],[CreateTime])" +
                        "VALUES (@Uid,@QuestUid,@QuestAnswerId,@TesteeId," +
                        "@QuestScore,@ActualScore,@TesteeSource,@CreateUserId,@CreateTime);", cn);
                    //TODO 剛填答DAO一樣要加檢核 SqlDbType還是DbType
                    cmd.Parameters.Add("@Uid", SqlDbType.UniqueIdentifier);
                    cmd.Parameters["@Uid"].Value = Guid.NewGuid();

                    cmd.Parameters.Add("@QuestUid", SqlDbType.UniqueIdentifier);
                    cmd.Parameters["@QuestUid"].Value = answer.QuestUid;

                    cmd.Parameters.Add("@QuestAnswerId", SqlDbType.NVarChar);
                    cmd.Parameters["@QuestAnswerId"].Value = answer.QuestAnswerId;

                    cmd.Parameters.Add("@TesteeId", SqlDbType.NVarChar);
                    cmd.Parameters["@TesteeId"].Value = answer.TesteeId;

                    cmd.Parameters.Add("@QuestScore", SqlDbType.Int);
                    cmd.Parameters["@QuestScore"].Value = answer.QuestScore;

                    cmd.Parameters.Add("@ActualScore", SqlDbType.Int);
                    cmd.Parameters["@ActualScore"].Value = answer.ActualScore;

                    cmd.Parameters.Add("@TesteeSource", SqlDbType.NVarChar);
                    cmd.Parameters["@TesteeSource"].Value = answer.TesteeSource;

                    cmd.Parameters.Add("@CreateUserId", SqlDbType.NVarChar);
                    cmd.Parameters["@CreateUserId"].Value = answer.CreateUserId;

                    cmd.Parameters.Add("@CreateTime", SqlDbType.DateTime);
                    cmd.Parameters["@CreateTime"].Value = answer.CreateTime;

                    //cmd.Parameters.AddWithValue("@Uid", answer.Uid);
                    //cmd.Parameters.AddWithValue("@QuestUid", answer.QuestUid);
                    //cmd.Parameters.AddWithValue("@QuestAnswerId", answer.QuestAnswerId);
                    //cmd.Parameters.AddWithValue("@TesteeId", answer.TesteeId);
                    //cmd.Parameters.AddWithValue("@QuestScore", answer.QuestScore);
                    //cmd.Parameters.AddWithValue("@ActualScore", answer.ActualScore);
                    //cmd.Parameters.AddWithValue("@TesteeSource", answer.TesteeSource);
                    //cmd.Parameters.AddWithValue("@CreateUserId", answer.CreateUserId);
                    //cmd.Parameters.AddWithValue("@CreateTime", answer.CreateTime);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
        }

    }
}
