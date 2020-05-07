using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ai_Product.Ingredient
{
    public class Item
    {
        public string ItemName
        {
            get;
            set;
        } = "AVI1";
        /// <summary>
        /// Remark
        /// </summary>
        public string Judge
        {
            get;
            set;
        } = "OK";
        /// <summary>
        /// Remark
        /// </summary>
        public string JudgeTotol
        {
            get;
            set;
        } = "OK";
        public string McId
        {
            get;
            set;
        } = "AVI1-001";
        public string OperatorID
        {
            get;
            set;
        } = "99999999";
        public string StartTime
        {
            get;
            set;
        } = "YYYY.MM.DD.HH.MM.SS";
        public string EndTimeTray
        {
            get;
            set;

        } = "YYYY.MM.DD.HH.MM.SS";
         public string Post
        {
            get;
            set;

        } = "YYYY.MM.DD.HH.MM.SS";
    }
}
