﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.infrastructure.DTO
{
    public class EmployeeDTO
    {
        //data annotation
        public int EmployeeId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string FullName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string Mobile { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string Address { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public DateTime JoinDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public decimal Salary { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public int QualificationId { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
