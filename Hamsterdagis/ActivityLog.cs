using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamsterdagis
{
    public enum Activity { Arrival, Caged, Exercise, Departure }
    public class ActivityLog
    {
        public int ActivityLogId { get; set; }
        public DateTime Timestamp { get; set; }
        public Activity Activity { get; set; }
        public Hamster Hamster { get; set; }
        public ActivityLog(DateTime timestamp, Activity activity)
        {
            this.Timestamp = timestamp;
            this.Activity = activity;
        }
        
        
    }
}
