using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class CreateEmployeeViewModel
    {
        [Required]
        [MaxLength(20, ErrorMessage = "Name's length must be fewer than 20")]
        public string Name { get; set; }
        [Required, Display(Name = "Office email")]
        [RegularExpression(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
            ErrorMessage = "Invalid email")]
        public string Email { get; set; }
        [Required]
        public DepartmentEnum? Department { get; set; }

        public List<IFormFile> Photos { get; set; }
    }
}
