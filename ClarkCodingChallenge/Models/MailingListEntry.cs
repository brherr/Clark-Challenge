using System.ComponentModel.DataAnnotations;

namespace ClarkCodingChallenge.Models
{
    public class MailingListEntry
    {
				[Required(ErrorMessage = "First Name cannot be empty.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name cannot be empty.")]
				public string LastName { get; set; }

        [Required(ErrorMessage = "Email cannot be empty.")]
    		[EmailAddress(ErrorMessage = "Email must be a valid email address.")]
				public string Email { get; set; }
    }
}
