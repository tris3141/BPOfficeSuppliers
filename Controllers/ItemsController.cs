using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BPOfficeSupplies.BusinessLogic;

namespace BPOfficeSupplies.Controllers
{
    public class ItemsController : Controller
    {
        ItemLogic logic = new ItemLogic();

        [HttpPost]
        public ActionResult AddItem(string Description, string ItemPicture, string CategoryId, HttpPostedFileBase file)
        {
            if (file != null)
            {
                string newPic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Content/ItemPictures"), newPic);
                ItemPicture = newPic;
                file.SaveAs(path);
            }
            var saved = logic.AddItemAlternative(Description, ItemPicture, CategoryId);
            TempData["AlertMessage"] = saved;
            return RedirectToAction("Admin", "Home");
        }

        [HttpGet]
        public ActionResult GetItem(string id)
        {
            return Json(logic.GetItem(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateItem(string itemid, string ItemNumber, string Description, string ItemPicture, string DateAdded, string CategoryId, HttpPostedFileBase file)
        {
            if (file != null)
            {
                string newPic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Content/ItemPictures"), newPic);
                ItemPicture = newPic;
                file.SaveAs(path);
            }
            var saved = logic.UpdateItemAlternative(itemid,ItemNumber,Description, ItemPicture, CategoryId);
            TempData["AlertMessage"] = saved;
            return RedirectToAction("Admin", "Home");
        }

        public ActionResult DeleteItem(string id)
        {
            var deleted = logic.DeleteItem(id);
            TempData["AlertMessage"] = deleted;
            return RedirectToAction("Admin", "Home");

        }
    }
}
