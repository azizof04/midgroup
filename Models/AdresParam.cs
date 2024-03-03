using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class AdresParam
    {
        public int Id { get; set; }
        public string? BuildNumber { get; set; }
        public string? EntryNumber { get; set; }
        public string? FloorNumber { get; set; }
        public string? FlatNumber { get; set; }
    }
}