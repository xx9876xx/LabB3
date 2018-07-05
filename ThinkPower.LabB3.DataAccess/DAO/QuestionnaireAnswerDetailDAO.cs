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
        /// 儲存問卷答題明細資料
        /// </summary>
        /// <param name="answerDetail"> 問卷答題明細DO </param>
        public void Insert(QuestionnaireAnswerDetailDO answerDetail)
        {
            try
            {
                using (SqlConnection cn = DbConnection)
                {
                    SqlCommand cmd = new SqlCommand
                        ("INSERT INTO QuestionnaireAnswerDetail" +
                        "([Uid],[AnswerUid],[QuestionUid],[AnswerCode],[OtherAnswer]," +
                        "[Score],[CreateUserId],[CreateTime])" +
                        "VALUES (@Uid,@AnswerUid,@QuestionUid,@AnswerCode," +
                        "@OtherAnswer,@Score,@CreateUserId,@CreateTime);", cn);
                    //TODO 要明確指定DB型別
                    //TODO DataAccece資料進來或出去要有檢核方法
                    //TODO (object)??DBNull.value
                    //TODO 檢核方法為資料本身是否null,長度多少,是否為Guid,是否非數字之類的
                    cmd.Parameters.Add("@Uid", SqlDbType.UniqueIdentifier);
                    cmd.Parameters["@Uid"].Value = Guid.NewGuid();

                    cmd.Parameters.Add("@AnswerUid", SqlDbType.UniqueIdentifier);
                    cmd.Parameters["@AnswerUid"].Value = answerDetail.AnswerUid;

                    cmd.Parameters.Add("@QuestionUid", SqlDbType.UniqueIdentifier);
                    cmd.Parameters["@QuestionUid"].Value = answerDetail.QuestionUid;

                    cmd.Parameters.Add("@AnswerCode", SqlDbType.VarChar);
                    cmd.Parameters["@AnswerCode"].Value = answerDetail.AnswerCode;

                    cmd.Parameters.Add("@OtherAnswer", SqlDbType.NVarChar);
                    cmd.Parameters["@OtherAnswer"].Value = answerDetail.OtherAnswer ?? (object)DBNull.Value;

                    cmd.Parameters.Add("@Score", SqlDbType.Int);
                    cmd.Parameters["@Score"].Value = answerDetail.Score ?? (object)DBNull.Value;

                    cmd.Parameters.Add("@CreateUserId", SqlDbType.VarChar);
                    cmd.Parameters["@CreateUserId"].Value = answerDetail.CreateUserId ?? (object)DBNull.Value;

                    cmd.Parameters.Add("@CreateTime", SqlDbType.DateTime);
                    cmd.Parameters["@CreateTime"].Value = answerDetail.CreateTime ?? (object)DBNull.Value;

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
