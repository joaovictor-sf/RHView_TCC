using System.ComponentModel.DataAnnotations.Schema;

namespace RHView_TCC.Models {
    public class InactivityLog {
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public TimeSpan TotalInactivity { get; set; }
        public TimeSpan MaxInactivity { get; set; }

        public int UserId { get; set; }
        public UserModel User { get; set; }
    }
}
