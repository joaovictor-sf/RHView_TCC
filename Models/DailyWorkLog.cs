using System.ComponentModel.DataAnnotations.Schema;

namespace RHView_TCC.Models {
    public class DailyWorkLog {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public TimeSpan TimeWorked { get; set; }

        public UserModel User { get; set; }
    }
}
