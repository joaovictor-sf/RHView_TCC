using System.ComponentModel.DataAnnotations;
using RHView_TCC.Models.Enuns;

namespace RHView_TCC.Models {
    public class UserModel {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public UserRole Role { get; set; }

        public WorkHours WorkHours { get; set; }

        public bool IsActive { get; set; } = true;

        public bool MustChangePassword { get; set; } = false;

        public ICollection<ProcessLog> ProcessLogs { get; set; } = new List<ProcessLog>();
        public ICollection<InactivityLog> InactivityLogs { get; set; } = new List<InactivityLog>();
    }
}
