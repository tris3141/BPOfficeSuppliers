using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BPOfficeSupplies.BusinessLogic;
using BPOfficeSupplies.Models;
using PagedList;
using System.Threading.Tasks;

namespace BPOfficeSupplies.Controllers
{
    public class RequestedQuotesController : Controller
    {
        QuoteLogic logic = new QuoteLogic();
        CustomerLogic clogic = new CustomerLogic();

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
            List<RequestedQuote> RequestedQuotes = logic.AllQuotes();

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
        public ActionResult AddQuote()
        {
            var added = logic.CreateQuote(User.Identity.Name);
            return Json(added, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPendingQuote()
        {
            var pending = logic.GetPendingQuote(User.Identity.Name);
            return Json(pending, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddItemToQuote(string ItemId, int qty)
        {
            var added = logic.AddItemToQuote(ItemId, qty, User.Identity.Name);
            return Json(added, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SendQuote(string quoteid)
        {
            var sent = logic.SendQuote(quoteid);
            //await logic.SendQuoteToAdminMail("terabytesad1@gmail.com", logic.GetQuoteByQuoteId(quoteid).QuoteNumber);
            return Json(sent, JsonRequestBehavior.AllowGet);
        }

        public ActionResult QuoteItems(string sortOrder, string currentFilter, string searchString, int? page, string id, string action, string qid, decimal? price)
        {
            if(action == "Update")
            {
                UpdateQuoteItem(qid, price ?? 1);
            }
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

            ViewBag.id = id;
            ViewBag.QuoteNumber = logic.GetQuoteByQuoteId(id).QuoteNumber;
            ViewBag.status = logic.GetQuoteByQuoteId(id).RequestStatus;
            ViewBag.CurrentFilter = searchString;
            var items = logic.QuoteItems(id);

            if (!String.IsNullOrEmpty(searchString))
            {
                var search = searchString.ToLower().ToString().Trim();
                items = items.Where(p => p.QuoteId.ToLower().Contains(search)).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    items = items.OrderByDescending(c => c.QuoteId).ToList();
                    break;
                default:
                    items = items.OrderBy(c => c.QuoteId).ToList();
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(items.ToPagedList(pageNumber, pageSize));
        }

        [Authorize(Roles = "Customer")]
        public ActionResult QuoteItemsCustomer(string sortOrder, string currentFilter, string searchString, int? page, string id, string action, string qid, decimal? price)
        {
            if (action == "Update")
            {
                UpdateQuoteItem(qid, price ?? 1);
            }
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

            ViewBag.total = logic.GetQuoteByQuoteId(id).TotalAmount;
            ViewBag.id = id;
            ViewBag.QuoteNumber = logic.GetQuoteByQuoteId(id).QuoteNumber;
            ViewBag.status = logic.GetQuoteByQuoteId(id).RequestStatus;
            ViewBag.CurrentFilter = searchString;
            var items = logic.QuoteItems(id);

            if (!String.IsNullOrEmpty(searchString))
            {
                var search = searchString.ToLower().ToString().Trim();
                items = items.Where(p => p.QuoteId.ToLower().Contains(search)).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    items = items.OrderByDescending(c => c.QuoteId).ToList();
                    break;
                default:
                    items = items.OrderBy(c => c.QuoteId).ToList();
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(items.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult UpdateQuoteItem(string id, decimal price)
        {
            var saved = logic.UpdateQuoteItem(id, price);
            if(saved.Contains("Price"))
            {
                TempData["err"] = "Invalid price.";
                return RedirectToAction("QuoteItems", "RequestedQuotes", new { id = saved });
            }
            return RedirectToAction("QuoteItems", "RequestedQuotes", new { id = saved });
        }

        [HttpPost]
        public ActionResult UpdateQuoteItemAlt(string id, decimal price)
        {
            var saved = logic.UpdateQuoteItem(id, price);
            return Json(saved, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SendToCustomer(string id)
        {
            var sent = logic.SendToCustomer(id);
            //await logic.SendQuoteToCustomerMail(clogic.GetCustomer(logic.GetQuoteByQuoteId(id).CustomerId).EmailAddress  , logic.GetQuoteByQuoteId(id).QuoteNumber);
            return Json(sent, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AcceptQuote(string id)
        {
            var accepted = logic.AcceptQuote(id);
            //await logic.SendDeclineQuoteToAdminMail("terabytesad1@gmail.com", logic.GetQuoteByQuoteId(id).QuoteNumber);
            return Json(accepted, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DeclineQuote(string id)
        {
            var declined = logic.DeclineQuote(id);
            return Json(declined, JsonRequestBehavior.AllowGet);
        }
    }
}
