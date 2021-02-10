using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkSafeWebSite.Models
{
    public class WSNotice
    {

        [Key]
        public int Id { get; set; }
        public string business_Name { get; set; }
        public int business_Number { get; set; }
        public string location { get; set; }
        public string industry { get; set; }
        public DateTime date_Issued { get; set; }
        public String issued_To { get; set; }
        public string notice_Type { get; set; }
        public string topic { get; set; }
        public string pdf_URL { get; set; }
        
        public DateTime? resolved_date { get; set; }
        public string resolved_pdf_URL { get; set; }
        public int infringements { get; set; }
        public DateTime? last_inspection { get; set; }
        
        public string current_notice_status { get; set; }
        public string current_resolve_status { get; set; }
        public string improvement_notice { get; set; }
        public WSNotice()
        {
               
        }
    }
}
