using System;

namespace iCompany.Areas.Design.Models
{
    public class ProductComponents
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid ComponentsId { get; set; }
        public int Amount { get; set; }

        public virtual Product Product { get; set; }
        public virtual Components Components { get; set; }
    }
}
