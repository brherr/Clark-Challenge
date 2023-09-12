using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ClarkCodingChallenge.Models;
using ClarkCodingChallenge.DataAccess;

namespace ClarkCodingChallenge.BusinessLogic
{
    public class ContactsService
    {
        private readonly ContactsDataAccess _dataAccess;

        public ContactsService(ContactsDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public bool AddMailingListEntry(MailingListEntry entry)
        {
            // Validation logic
            if (string.IsNullOrWhiteSpace(entry.FirstName) || string.IsNullOrWhiteSpace(entry.LastName) || !IsValidEmail(entry.Email))
            {
                return false; // Validation failed
            }

            // Call data access to add the entry
            _dataAccess.AddMailingListEntry(entry);
            return true; // Added successfully
        }

        public IEnumerable<MailingListEntry> GetMailingListEntries(string lastName = null, string sortDirection = "ascending")
        {
            // Call data access to get mailing list entries
            var mailingList = _dataAccess.GetAllMailingListEntries();

            var filteredList = string.IsNullOrWhiteSpace(lastName)
                ? mailingList
                : mailingList.Where(entry => entry.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

            return sortDirection.ToLower() == "descending"
                ? filteredList.OrderByDescending(entry => entry.LastName).ThenBy(entry => entry.FirstName)
                : filteredList.OrderBy(entry => entry.LastName).ThenBy(entry => entry.FirstName);
        }

        private bool IsValidEmail(string email)
        {
           // Regular expression pattern for a basic email validation
    			string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

    			// Use Regex.IsMatch to check if the email matches the pattern
    			return !string.IsNullOrWhiteSpace(email) && System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern);
        }
    }
}
