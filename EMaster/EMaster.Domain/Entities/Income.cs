using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMaster.Domain.Entities
{
    public class Income : BaseEntity
    {
        public int CategoryID { get; set; } 
        public decimal Amount { get; set; } 
        public DateTime Date { get; set; } 
        public string? Description { get; set; } 
        public Category Category { get; set; } = null!;
    }

}
