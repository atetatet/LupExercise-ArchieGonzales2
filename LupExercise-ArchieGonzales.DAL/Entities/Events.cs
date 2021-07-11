using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LupExercise_ArchieGonzales.DAL.Entities
{
    public class Events
    {
        [Key]
        public int EventId { get; set; }
        [Required]
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public string EventTimezone { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
