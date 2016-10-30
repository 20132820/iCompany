using System;
using System.ComponentModel.DataAnnotations;

namespace iCompany.Areas.Design.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        [Required, MaxLength(128)]
        public string Name { get; set; }
        [Required, MaxLength(128)]
        public string No { get; set; }
    }
}
