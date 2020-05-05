using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class EditEmployeeViewModel : CreateEmployeeViewModel
    {
        [HiddenInput]
        public int Id { get; set; }
        [HiddenInput]
        public string ExistingPath { get; set; }
    }
}
