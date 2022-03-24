using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterlySales.Models.DomainModels
{
	/*
	The User class is the domain class for the user object
	*/
    public class User
    {
        [Key]
        [Required]
        public String username { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[A-Z])[a-zA-Z\d]{6,}$", ErrorMessage = "The password should be more than six characters and should contain an upper case character")]
        public String password { get; set; }
        public bool isAdmin { get; set; }
    }
}
