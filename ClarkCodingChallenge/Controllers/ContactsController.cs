using Microsoft.AspNetCore.Mvc;
using ClarkCodingChallenge.Models;
using ClarkCodingChallenge.BusinessLogic;
using System.Diagnostics;

namespace ClarkCodingChallenge.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ContactsService _contactsService;

        public ContactsController(ClarkCodingChallenge.BusinessLogic.ContactsService contactsService)
        {
            _contactsService = contactsService;
        }

        public IActionResult Index(MailingListEntry model)
				{
						if (ModelState.IsValid)
    {
        // If the model is valid, add it to the mailing list
        if (_contactsService.AddMailingListEntry(model))
        {
            // If added successfully, clear the model to prevent re-display
            ModelState.Clear(); // Clear the ModelState
            model = new MailingListEntry(); // Clear the model
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Validation failed. Please check your input.");
        }
    }

    // Retrieve the mailing list entries with sorting options
    var mailingList = _contactsService.GetMailingListEntries(sortDirection: "ascending");

    return View("Index", new IndexViewModel
    {
        FormModel = model,
        MailingListEntries = mailingList
    });

				}

        [HttpPost]
        public IActionResult AddMailingListEntry(MailingListEntry model)
				{
						 if (!ModelState.IsValid)
    {
        // Return to the Index view with validation errors
        var mailingList = _contactsService.GetMailingListEntries(sortDirection: "ascending");
        return View("Index", new IndexViewModel
        {
            FormModel = model,
            MailingListEntries = mailingList
        });
    }

    if (_contactsService.AddMailingListEntry(model))
    {
        // If the entry was added successfully, clear the form model
        model = new MailingListEntry();
    }
    else
    {
        ModelState.AddModelError(string.Empty, "Validation failed. Please check your input.");
    }

    // Update the mailing list entries in the view model
    var updatedViewModel = new IndexViewModel
    {
        FormModel = model,
        MailingListEntries = _contactsService.GetMailingListEntries(sortDirection: "ascending")
    };

    return View("Index", updatedViewModel);
				}

        [HttpGet]
        public IActionResult GetMailingList(string lastName = null, string sortDirection = "ascending")
        {
            var mailingList = _contactsService.GetMailingListEntries(lastName, sortDirection);
            return View("MailingList", mailingList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

				public IActionResult Confirmation()
				{
    			return View();
				}
    }
}
