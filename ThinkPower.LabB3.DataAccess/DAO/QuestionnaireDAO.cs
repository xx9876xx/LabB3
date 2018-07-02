using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkPower.LabB3.DataAccess.DAO;
using NLog;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.ExceptionServices;
using ThinkPower.LabB3.DataAccess.DO;

namespace ThinkPower.LabB3.DataAccess.DAO
{
    /// <summary>
    /// 問卷資料存取類別
    /// </summary>
    public class QuestionnaireDAO : BaseDAO
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
                        ("SELECT COUNT(Uid) FROM Questionnaire", cn);
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
        /// 取得指定問卷識別項的問卷資料
        /// </summary>
        /// <param name="uid">問卷識別項</param>
        /// <returns> 問卷主檔DO物件 </returns>
        public QuestionnaireDO GetQuestionnaireData(Guid uid)
        {
            if (uid == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                QuestionnaireDO questionnaireDO = new QuestionnaireDO();
                using (SqlConnection cn = GetConnection())
                {
                    SqlCommand cmd = new SqlCommand
                        ("SELECT TOP 1 [Uid],[QuestId],[Version],[Kind],[Name],[Memo],[Ondate],[Offdate]," +
                        "[NeedScore],[QuestScore],[ScoreKind],[HeadBackgroundImg],[HeadDescription]," +
                        "[FooterDescription],[CreateUserId],[CreateTime],[ModifyUserId],[ModifyTime] " +
                        "FROM Questionnaire " +
                        "WHERE [Uid] = @Uid " +
                        "ORDER BY [Ondate] DESC", cn);
                    cmd.Parameters.AddWithValue("@Uid", Convert.ToString(uid));
                    cn.Open();
                    //TODO 如果取不到值該怎麼辦 要訂好檢核 要不到就回傳null;  集合的話吐空集合給他;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            questionnaireDO.Uid = Guid.Parse(Convert.ToString(dr["Uid"]));
                            questionnaireDO.Version = Convert.ToString(dr["Version"]);
                            questionnaireDO.Kind = Convert.ToString(dr["Kind"]);
                            questionnaireDO.Name = Convert.ToString(dr["Name"]);
                            questionnaireDO.Memo = Convert.ToString(dr["Memo"]);
                            questionnaireDO.Ondate = Convert.IsDBNull(dr["Ondate"]) ? (DateTime?)null : Convert.ToDateTime(dr["Ondate"]);
                            questionnaireDO.Offdate = Convert.IsDBNull(dr["Offdate"]) ? (DateTime?)null : Convert.ToDateTime(dr["Offdate"]);
                            questionnaireDO.NeedScore = Convert.ToString(dr["NeedScore"]);
                            questionnaireDO.QuestScore = Convert.IsDBNull(dr["QuestScore"]) ? (int?)null : Convert.ToInt32(dr["QuestScore"]);
                            questionnaireDO.ScoreKind = Convert.ToString(dr["ScoreKind"]);
                            questionnaireDO.HeadBackgroundImg = Convert.ToString(dr["HeadBackgroundImg"]);
                            questionnaireDO.HeadDescription = Convert.ToString(dr["HeadDescription"]);
                            questionnaireDO.FooterDescription = Convert.ToString(dr["FooterDescription"]);
                            questionnaireDO.CreateUserId = Convert.ToString(dr["CreateUserId"]);
                            questionnaireDO.CreateTime = Convert.IsDBNull(dr["CreateTime"]) ? (DateTime?)null : Convert.ToDateTime(dr["CreateTime"]);
                            questionnaireDO.ModifyUserId = Convert.ToString(dr["ModifyUserId"]);
                            questionnaireDO.ModifyTime = Convert.IsDBNull(dr["ModifyTime"]) ? (DateTime?)null : Convert.ToDateTime(dr["ModifyTime"]);
                        }
                    }
                }
                return questionnaireDO;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                ExceptionDispatchInfo.Capture(ex).Throw();
                return null;
            }
        }


        /// <summary>
        /// 取得指定問卷編號的問卷資料
        /// </summary>
        /// <param name="questId">問卷編號</param>
        /// <returns> 問卷主檔DO物件 </returns>
        public QuestionnaireDO GetQuestionnaireData(string questId)
        {
            if (questId == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                QuestionnaireDO questionnaireDO = new QuestionnaireDO();
                using (SqlConnection cn = GetConnection())
                {
                    SqlCommand cmd = new SqlCommand
                        ("SELECT TOP 1 [Uid],[QuestId],[Version],[Kind],[Name],[Memo],[Ondate],[Offdate]," +
                        "[NeedScore],[QuestScore],[ScoreKind],[HeadBackgroundImg],[HeadDescription]," +
                        "[FooterDescription],[CreateUserId],[CreateTime],[ModifyUserId],[ModifyTime] " +
                        "FROM Questionnaire " +
                        "WHERE [QuestId] = @questId " +
                        "ORDER BY [Ondate] DESC", cn);
                    cmd.Parameters.AddWithValue("@questId", questId);
                    cn.Open();
                    //TODO 如果取不到值該怎麼辦 要訂好檢核 要不到就回傳null;  集合的話吐空集合給他;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {                            
                            questionnaireDO.Uid = Guid.Parse(Convert.ToString(dr["Uid"]));
                            questionnaireDO.Version = Convert.ToString(dr["Version"]);
                            questionnaireDO.Kind = Convert.ToString(dr["Kind"]);
                            questionnaireDO.Name = Convert.ToString(dr["Name"]);
                            questionnaireDO.Memo = Convert.ToString(dr["Memo"]);
                            questionnaireDO.Ondate = Convert.IsDBNull(dr["Ondate"]) ? (DateTime?)null : Convert.ToDateTime(dr["Ondate"]);
                            questionnaireDO.Offdate = Convert.IsDBNull(dr["Offdate"]) ? (DateTime?)null : Convert.ToDateTime(dr["Offdate"]);
                            questionnaireDO.NeedScore = Convert.ToString(dr["NeedScore"]);
                            questionnaireDO.QuestScore = Convert.IsDBNull(dr["QuestScore"]) ? (int?)null : Convert.ToInt32(dr["QuestScore"]);
                            questionnaireDO.ScoreKind = Convert.ToString(dr["ScoreKind"]);
                            questionnaireDO.HeadBackgroundImg = Convert.ToString(dr["HeadBackgroundImg"]);
                            questionnaireDO.HeadDescription = Convert.ToString(dr["HeadDescription"]);
                            questionnaireDO.FooterDescription = Convert.ToString(dr["FooterDescription"]);
                            questionnaireDO.CreateUserId = Convert.ToString(dr["CreateUserId"]);
                            questionnaireDO.CreateTime = Convert.IsDBNull(dr["CreateTime"]) ? (DateTime?)null : Convert.ToDateTime(dr["CreateTime"]);
                            questionnaireDO.ModifyUserId = Convert.ToString(dr["ModifyUserId"]);
                            questionnaireDO.ModifyTime = Convert.IsDBNull(dr["ModifyTime"]) ? (DateTime?)null : Convert.ToDateTime(dr["ModifyTime"]);
                        }
                    }
                }
                return questionnaireDO;
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

