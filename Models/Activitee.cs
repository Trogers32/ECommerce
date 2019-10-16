using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    public class Activitee
    {
///////////////////////////////////////////////////////////////////////
        [Key]
        public int ActivityId { get; set; }
///////////////////////////////////////////////////////////////////////
        [Required(ErrorMessage = "Activity title is required.")]
        public string ActivityTitle { get; set; }
///////////////////////////////////////////////////////////////////////
        [Required(ErrorMessage = "Activity date is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Activity date is required.")]
        public DateTime ActivityDate { get; set; }
///////////////////////////////////////////////////////////////////////
        [Required(ErrorMessage = "Activity duration is required.")]
        [Range(1,99999999999999,ErrorMessage="Activity duration must be more than 0.")]
        public int ActivityDuration {get;set;}
///////////////////////////////////////////////////////////////////////
        [Required(ErrorMessage = "Activity timeframe is required.")]
        public string ActivityTimeType { get; set; }
///////////////////////////////////////////////////////////////////////
        [Required(ErrorMessage = "Activity description is required.")]
        public string ActivityDescription { get; set; }
///////////////////////////////////////////////////////////////////////
        public int GuestCount {get;set;}
///////////////////////////////////////////////////////////////////////
        public string Coordinator {get;set;}
    
        // We can provide some hardcoded default values like so:
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
///////////////////////////////////////////////////////////////////////
        public List<Associations> Users {get;set;}
///////////////////////////////////////////////////////////////////////

    }
} 