using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BPOfficeSupplies.BusinessLogic;
using BPOfficeSupplies.Models;
using PagedList;

namespace BPOfficeSupplies.Controllers
{
    public class CustomersController : Controller
    {
        CustomerLogic logic = new CustomerLogic();
        QuoteLogic qlogic = new QuoteLogic();

        public ActionResult AllCustomers(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;

            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            List<Customer> Customers = logic.GetCustomers();
            if (!String.IsNullOrEmpty(searchString))
            {
                var search = searchString.ToLower().ToString().Trim();
                Customers = Customers.Where(p => p.CustomerNumber.ToLower().Contains(search) || p.FirstName.ToLower().Contains(search) || p.Surname.ToString().ToLower().Contains(search) || p.EmailAddress.ToString().ToLower().Contains(search) || p.EmailAddress.ToString().ToLower().Contains(search) || p.ResidentialAddress.ToString().ToLower().Contains(search) || p.PhoneNumber.ToString().ToLower().Contains(search)).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    Customers = Customers.OrderByDescending(c => c.FirstName).ToList();
                    break;
                default:
                    Customers = Customers.OrderBy(c => c.FirstName).ToList();
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(Customers.ToPagedList(pageNumber, pageSize));
        }

        [Authorize(Roles = "Customer")]
        public ActionResult AllQuotes(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;

            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            List<RequestedQuote> RequestedQuotes = qlogic.AllQuotesCustomer(User.Identity.Name);

            if (!String.IsNullOrEmpty(searchString))
            {
                var search = searchString.ToLower().ToString().Trim();
                RequestedQuotes = RequestedQuotes.Where(p => p.QuoteNumber.ToLower().Contains(search) || p.RequestStatus.ToLower().Contains(search)).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    RequestedQuotes = RequestedQuotes.OrderByDescending(c => c.QuoteNumber).ToList();
                    break;
                default:
                    RequestedQuotes = RequestedQuotes.OrderBy(c => c.DateQuoted).ToList();
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(RequestedQuotes.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public ActionResult UpdateProfile(string email, string FirstName, string Surname, string PhoneNumber, string ResidentialAddress)
        {
            var updated = logic.UpdateCustomer(User.Identity.Name, FirstName, Surname, PhoneNumber, ResidentialAddress);
            TempData["response"] = updated;
            return RedirectToAction("ChangePassword", "Manage");
        }
    }
}
