using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLyPhongGym.Application.Features.Admin.Queries
{
    public class StaffAndPtDto
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Role { get; set; } // PT hoặc Staff
        public string? Specialization { get; set; } // Chỉ có giá trị nếu là PT
    }
}
