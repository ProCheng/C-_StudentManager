using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 学员类
    /// </summary>
    [Serializable]
    public class ScoreList:Student
    {
        public int Id { get; set; }
        public new int StudentId { get; set; }
        public int CSharp { get; set; }
        public int SQLServerDB { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
