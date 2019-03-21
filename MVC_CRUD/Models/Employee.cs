using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_CRUD.Models
{
    public class Employee
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        //[Required]
        public string Language { get; set; }
        //[Required]
    }
}
