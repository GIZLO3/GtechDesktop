using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtechDesktop.WPF.Models
{
    public class Subcategory
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CategoryId { get; set;}
        public List<string>? ParametersPattern { get; set; }
    }
}
