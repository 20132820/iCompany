using System.ComponentModel.DataAnnotations;

namespace iCompany.Areas.Shared.Models
{
    public class Config
    {
        [Key, MaxLength(32)]
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
