using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThinkPower.LabB3.Web.ActionModels
{
    public class EvaluationRankActionModel
    {
        public EvaluationRankActionModel()
        {
            //this.UserId = GetRandomString("EvaUser_",6);
            this.QuestionnaireId = "LabB3";
        }

        /// <summary>
        /// 問卷版本
        /// </summary>
        public string QuestId { get; set; }

        ///// <summary>
        ///// 填寫人員編號 (每次登入隨機產生)
        ///// </summary>
        //public string UserId { get; set; }

        /// <summary>
        /// 問卷填寫來源代號
        /// </summary>
        public string QuestionnaireId { get; set; }

        /// <summary>
        /// 產生亂數UserId
        /// </summary>
        /// <param name="fixedCode"> 固定前綴ID字串</param>
        /// <param name="length"> 亂數英數字串長度</param>
        /// <returns>亂數英數字串</returns>
        private string GetRandomString(string fixedCode ,int length)
        {
            Random r = new Random();
            string code = fixedCode;
            for (int i = 0; i < length; ++i)
            {
                switch (r.Next(0, 3))
                {
                    case 0: code += r.Next(0, 10); break;
                    case 1: code += (char)r.Next(65, 91); break;
                    case 2: code += (char)r.Next(97, 122); break;
                }
            }

            return code;
        }

    }
}