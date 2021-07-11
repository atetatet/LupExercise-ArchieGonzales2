using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LupExercise_ArchieGonzales.ViewModels
{
    public class EventsViewModel
    {
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public string EventTimezone { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
