using System;
using System;
using Doctor_Module.Models.Doctor;
namespace Doctor_Module.Timeslots
{
    public class Timeslot
    {
        public int TimeSlotID { get; set; }
        public string DoctorID { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Start_Time { get; set; }
        public TimeSpan End_Time { get; set; }
        public int count { get; set; }
        public Doctor doctor { get; set; } //Navigation
                                           // timeslot


        // public TimeSpan GetTimeDifference()
        // {
        //     DateTime startTime = DateTime.ParseExact(Start_Time, "HH:mm:ss", null);
        //     DateTime endTime = DateTime.ParseExact(End_Time, "HH:mm:ss", null);

        //     TimeSpan timeDifference = endTime - startTime;
        //     return timeDifference;
        // }
        public TimeSpan GetTimeDifference()
        {
            return End_Time - Start_Time;
        }
    }
}
