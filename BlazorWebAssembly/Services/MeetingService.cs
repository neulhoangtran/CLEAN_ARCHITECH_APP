using BlazorWebAssembly.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorWebAssembly.Services
{
    public class MeetingService
    {
        private readonly List<MeetingMinuteModel> _meetingMinutes;

        public MeetingService()
        {
            // Dữ liệu mẫu
            _meetingMinutes = new List<MeetingMinuteModel>
            {
                new MeetingMinuteModel { Id = 1, Issue = "Budget Allocation", Outcome = "Approved", PersonInCharge = "John Doe", Manager = "Jane Smith", Schedule = "2024-10-25", Status = "Completed", Notes = "Approved with modifications" },
                new MeetingMinuteModel { Id = 2, Issue = "Project Timeline", Outcome = "Pending Review", PersonInCharge = "Alice Johnson", Manager = "Mark Brown", Schedule = "2024-11-05", Status = "Pending", Notes = "Waiting for manager approval" },
                new MeetingMinuteModel { Id = 3, Issue = "Resource Planning", Outcome = "Delayed", PersonInCharge = "Michael Lee", Manager = "Sarah White", Schedule = "2024-12-01", Status = "In Progress", Notes = "Delayed due to resource constraints" }
            };
        }

        public Task<List<MeetingMinuteModel>> GetMeetingMinutesAsync()
        {
            // Trả về danh sách dữ liệu mẫu
            return Task.FromResult(_meetingMinutes);
        }
    }
}
