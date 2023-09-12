using System.Collections.Generic;

namespace ClarkCodingChallenge.Models
{
    public class IndexViewModel
    {
        public MailingListEntry FormModel { get; set; }
        public IEnumerable<MailingListEntry> MailingListEntries { get; set; }
    }
}
