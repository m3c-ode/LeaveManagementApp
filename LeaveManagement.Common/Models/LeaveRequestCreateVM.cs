using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Common.Models
{
    public class LeaveRequestCreateVM : IValidatableObject
    {

        [Required]
        [DisplayName("Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        [Required]
        [DisplayName("End Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        [Required]
        [DisplayName("Leave Type")]
        public int LeaveTypeId { get; set; }
        public SelectList? LeaveTypes { get; set; }
        [DisplayName("Comments")]
        public string? RequestComments { get; set; }
        public string? RequestingEmployeeId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate > EndDate)
            {
                //Error message will appear on both fields indicated
                yield return new ValidationResult("Start Date must be before end date", new[] { nameof(StartDate), nameof(EndDate) });
            }
            if (RequestComments?.Length > 300)
            {
                yield return new ValidationResult("Comment is too long", new[] { nameof(RequestComments) });
            }
        }
    }
}
