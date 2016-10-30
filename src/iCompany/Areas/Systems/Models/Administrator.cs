using System;
using System.ComponentModel.DataAnnotations;

namespace iCompany.Areas.Systems.Models
{
    public class Administrator
    {
        [MaxLength(64)]
        public string Id { get; set; }
        [Required, MaxLength(128)]
        public string Name { get; set; }
        [Required, MaxLength(32)]
        public string Role { get; set; }
        [Required, MaxLength(128)]
        public string Password { get; set; }
        [MaxLength(128)]
        public string Password2 { get; set; }
        [MaxLength(128)]
        public string Password3 { get; set; }
        public DateTime PasswordChangeTime { get; set; }
    }
}
