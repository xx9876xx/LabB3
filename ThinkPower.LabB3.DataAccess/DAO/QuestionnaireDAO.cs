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
                using (SqlConnection cn = GetConnection())
                {
                    SqlCommand cmd = new SqlCommand
                        ("SELECT COUNT(Uid) As Count FROM LabB3.dbo.Questionnaire", cn);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while ((dr.Read()))
                        {
                            if (!dr["Count"].Equals(DBNull.Value))
                            {
                                if (Int32.TryParse(dr[0].ToString(), out count))
                                {
                                }
                                else
                                {
                                    logger.Error("執行Count方法時，無法轉換讀取資料為數字！");
                                    throw new InvalidCastException("執行Count方法時，無法轉換讀取資料為數字！");
                                }
                            }
                        }
                        cmd.Cancel();
                        dr.Close();
                    }                    
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
        /// 查詢問卷
        /// </summary>
        /// <param name="id">問卷Uid</param>
        /// <returns> QuestionnaireDO物件 </returns>
        public QuestionnaireDO Read(string QuestId)
        {
            try
            {
                QuestionnaireDO questionnaireDO = new QuestionnaireDO();
                using (SqlConnection cn = GetConnection())
                {
                    SqlCommand cmd = new SqlCommand
                        ("SELECT TOP 1 [Uid],[QuestId],[Version],[Kind],[Name],[Memo],[Ondate],[Offdate]," +
                        "[NeedScore],[QuestScore],[ScoreKind],[HeadBackgroundImg],[HeadDescription]," +
                        "[FooterDescription],[CreateUserId],[CreateTime],[ModifyUserId],[ModifyTime] " +
                        "FROM[LabB3].[dbo].[Questionnaire] " +
                        "WHERE [QuestId] = @quesrId " +
                        "ORDER BY [Ondate] DESC", cn);
                    cmd.Parameters.AddWithValue("@quesrId", QuestId);
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            questionnaireDO.Uid = dr.GetGuid(dr.GetOrdinal("Uid"));
                            questionnaireDO.Version = dr["Version"].ToString();
                            questionnaireDO.Kind = dr["Kind"].ToString();
                            questionnaireDO.Name = dr["Name"].ToString();
                            questionnaireDO.Memo = dr["Memo"].ToString();
                            questionnaireDO.Ondate = ObjectToNullableDateTime(dr["Ondate"]);
                            questionnaireDO.Offdate = ObjectToNullableDateTime(dr["Offdate"]);
                            questionnaireDO.NeedScore = dr["NeedScore"].ToString();
                            questionnaireDO.ScoreKind = dr["ScoreKind"].ToString();
                            questionnaireDO.HeadBackgroundImg = dr["HeadBackgroundImg"].ToString();
                            questionnaireDO.HeadDescription = dr["HeadDescription"].ToString();
                            questionnaireDO.FooterDescription = dr["FooterDescription"].ToString();
                            questionnaireDO.CreateUserId = dr["CreateUserId"].ToString();
                            questionnaireDO.CreateTime = ObjectToNullableDateTime(dr["CreateTime"]);
                            questionnaireDO.ModifyUserId = dr["ModifyUserId"].ToString();
                            questionnaireDO.ModifyTime = ObjectToNullableDateTime(dr["ModifyTime"]);                            
                        }
                        cmd.Cancel();
                        dr.Close();
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
        /// 查詢問卷
        /// </summary>
        /// <returns></returns>
        public List<QuestionnaireDO> ReadAll()
        {
            try
            {
                List<QuestionnaireDO> listDO = new List<QuestionnaireDO>();
                using (SqlConnection cn = GetConnection())
                {
                    SqlCommand cmd = new SqlCommand
                        ("SELECT [Uid],[QuestId],[Version],[Kind],[Name],[Memo],[Ondate],[Offdate]," +
                        "[NeedScore],[QuestScore],[ScoreKind],[HeadBackgroundImg],[HeadDescription]," +
                        "[FooterDescription],[CreateUserId],[CreateTime],[ModifyUserId],[ModifyTime] " +
                        "FROM[LabB3].[dbo].[Questionnaire]", cn);
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            QuestionnaireDO questionnaireDO = new QuestionnaireDO();
                            questionnaireDO.Uid = dr.GetGuid(dr.GetOrdinal("Uid"));
                            questionnaireDO.Version = dr["Version"].ToString();
                            questionnaireDO.Kind = dr["Kind"].ToString();
                            questionnaireDO.Name = dr["Name"].ToString();
                            questionnaireDO.Memo = dr["Memo"].ToString();
                            questionnaireDO.Ondate = ObjectToNullableDateTime(dr["Ondate"]);
                            questionnaireDO.Offdate = ObjectToNullableDateTime(dr["Offdate"]);
                            questionnaireDO.NeedScore = dr["NeedScore"].ToString();
                            questionnaireDO.ScoreKind = dr["ScoreKind"].ToString();
                            questionnaireDO.HeadBackgroundImg = dr["HeadBackgroundImg"].ToString();
                            questionnaireDO.HeadDescription = dr["HeadDescription"].ToString();
                            questionnaireDO.FooterDescription = dr["FooterDescription"].ToString();
                            questionnaireDO.CreateUserId = dr["CreateUserId"].ToString();
                            questionnaireDO.CreateTime = ObjectToNullableDateTime(dr["CreateTime"]);
                            questionnaireDO.ModifyUserId = dr["ModifyUserId"].ToString();
                            questionnaireDO.ModifyTime = ObjectToNullableDateTime(dr["ModifyTime"]);
                            listDO.Add(questionnaireDO);
                        }
                        cmd.Cancel();
                        dr.Close();
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

