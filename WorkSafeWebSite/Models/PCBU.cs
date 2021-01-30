using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkSafeWebSite.Models
{
    public class PCBU
    {
        [Key]
        public int Id { get; set; }
        public int business_Number { get; set; }
        public string business_Name { get; set; }
        public string location { get; set; }
        public string industry { get; set; }

        public PCBU()
        {

        }
    }
}
