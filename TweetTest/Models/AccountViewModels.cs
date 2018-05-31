using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TweetTest.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "ユーザー名")]
        public string Name { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }
}
