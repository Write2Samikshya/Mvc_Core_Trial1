using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EMPMGT22.Models
{
    public class Employee
    {

        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [MaxLength(8,ErrorMessage ="length cant exceed 8 alphabets")]
        [MinLength(2,ErrorMessage ="length should be more than 2 letters")]       
        public string Name { get; set; }
        [Required]
        //[RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        //ErrorMessage = "Invalid email format")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",ErrorMessage ="Invalid Email format")]
        [Display( Name="Office Email")]
        public string Email{ get; set; }
        //public string Department { get; set; }
        [Required]
        public Dept? Department { get; set; }
        public string PhotoPath { get; set; }


    }
}



//if you wants to allow minimum one alphabet, use +

///^([a - zA - Z])+$/

//if you wants to allow 0 alphabets also, use *

///^([a - zA - Z]) *$/