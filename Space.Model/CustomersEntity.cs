using System;
using System.ComponentModel.DataAnnotations;

namespace Space.Model
{
    /// <summary>
    /// Purpose: Data Contract Entity Model Class [CustomersEntity] for the table [dbo].[Customers].
    /// </summary>
    public class CustomersEntity : IDisposable
    {
        #region Class Public Methods

        /// <summary>
        /// Purpose: Implements the IDispose interface.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Class Property Declarations

        [Required(ErrorMessage = "You must enter a First Name.")]
        [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must enter a Last Name.")]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "You must enter a phone number.")]
        public int PhoneNumber { get; set; }

        [Required(ErrorMessage = "You must enter an email address.")]
        [StringLength(320, MinimumLength = 3)]
        public string EmailAddress { get; set; }
        public object CustomerID { get; set; }

        #endregion

    }
}
