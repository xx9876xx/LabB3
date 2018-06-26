using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThinkPower.LabB3.Web.ActionModels
{
    public class EvaluationRankActionModel
    {
        public EvaluationRankActionModel() { }
        public EvaluationRankActionModel(string Version)
        {
            this.Version = Version;
        }
        /// <summary>
        /// 問卷版本
        /// </summary>
        public string Version { get; set; }
        
        
    }
}