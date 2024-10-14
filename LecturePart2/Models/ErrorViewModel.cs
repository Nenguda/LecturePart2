

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LecturePart2.Models
{
    // Error View Model
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    // Lecturer Model
    public class Lecturer
    {
        public int LecturerId { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public ICollection<Claim> Claims { get; set; }
    }

    // Claim Model
    public class Claim
    {
        public int ClaimId { get; set; }

        [Required]
        [Display(Name = "Module Name")]
        public string ModuleName { get; set; }

        [Required]
        [Display(Name = "Module Code")]
        public string ModuleCode { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Submission Date")]
        public DateTime SubmissionDate { get; set; }

        [Display(Name = "Claim Status")]
        public string Status { get; set; } // Submitted, Approved, Denied, Paid

        [Display(Name = "Supporting Documents")]
        public string SupportingDocuments { get; set; } // File path to documents

        public int LecturerId { get; set; }
        public Lecturer Lecturer { get; set; }
    }

    // Program Coordinator Model
    public class ProgramCoordinator
    {
        public int CoordinatorId { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    // Program Manager Model
    public class ProgramManager
    {
        public int ManagerId { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}