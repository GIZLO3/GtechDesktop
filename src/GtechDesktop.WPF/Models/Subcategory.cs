using System.Collections.Generic;

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
