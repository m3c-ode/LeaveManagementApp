﻿using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Common.Models
{
    public class LeaveAllocationVM
    {
        [Required]
        public int Id { get; set; }
        [Display(Name = "Number of Days")]
        [Required]
        [Range(1, 21, ErrorMessage = "Invalid number entered")]
        public int NumberOfDays { get; set; }
        [Required]
        [Display(Name = "Allocation Period")]
        public int Period { get; set; }
        [Display(Name = "Leave Type")]

        public LeaveTypeVM? LeaveType { get; set; }
    }
}