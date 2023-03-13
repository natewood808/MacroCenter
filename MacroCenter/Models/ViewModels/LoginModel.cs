using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MacroCenter.Models.ViewModels
{
    /// <summary>
    /// This is a view model which is used to represent user login info.
    /// </summary>
    public class LoginModel
    {
        public string UserName { get; set; }

        [Required] // Uses model validation to make sure values are provided.
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        // When we use asp-for attribute on the input element in the login Razor view,
        // the tag helper will set the type attribute to password; that way, the text
        // entered by the user isn't visible onscreen
        [UIHint("password")]
        public string Password { get; set; }
        
        public string ReturnUrl { get; set; }
    }
}
