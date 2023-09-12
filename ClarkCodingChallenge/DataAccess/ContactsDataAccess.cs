using System.Collections.Generic;
using ClarkCodingChallenge.Models;

namespace ClarkCodingChallenge.DataAccess
{
    public class ContactsDataAccess
    {
        private List<MailingListEntry> mailingList = new List<MailingListEntry>();

        public bool AddMailingListEntry(MailingListEntry entry)
        {
            // Add the entry to the in-memory list
            mailingList.Add(entry);
            return true; // Data added successfully
        }

        public IEnumerable<MailingListEntry> GetAllMailingListEntries()
        {
            return mailingList;
        }

        // You can add more methods for specific data retrieval or manipulation as needed
    }
}
