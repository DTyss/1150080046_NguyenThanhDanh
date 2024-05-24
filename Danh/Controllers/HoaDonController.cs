using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Danh.Models;
using System.Net;
using System.Web.Mvc;
using System.Globalization;

namespace Danh.Controllers
{
    public class HoaDonController : Controller
    {
        Models.DanhEntities1 _context = new Models.DanhEntities1(); // Tạo đối tượng để truy cập dữ liệu

        public ActionResult Index()


        {

            var listOfData = _context.HoaDons.ToList(); // Lấy tất cả dữ liệu từ bảng HANGHOA
            return View(listOfData);
        }

        [HttpGet]
        public ActionResult Create()
        {

            var madienkeList = _context.DienKes.Select(x => new SelectListItem { Value = x.MaDienKe.ToString(), Text = x.HieuDienThe }).ToList();
            ViewBag.MaDienKeList = new SelectList(madienkeList, "Value", "Text");


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.HoaDon model)
        {
            if (ModelState.IsValid) // Kiểm tra xem dữ liệu được gửi từ form có hợp lệ không
            {
                try
                {




                    var madienke = _context.DienKes.Find(model.MaDienKe);


                    if (madienke == null)
                    {
                        ModelState.AddModelError("MaNuoc", "Mã hoa không hợp lệ.");
                    }



                    if (ModelState.IsValid) // Kiểm tra xem sau khi kiểm tra mã hoa và mã khu vực, ModelState có còn hợp lệ không
                    {
                        // Thêm model vào cơ sở dữ liệu
                        _context.HoaDons.Add(model);
                        _context.SaveChanges();

                        ViewBag.Message = "Data inserted successfully";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Error occurred: " + ex.Message;
                }
            }
            var madienkeList = _context.DienKes.Select(x => new SelectListItem { Value = x.MaDienKe.ToString(), Text = x.HieuDienThe }).ToList();
            ViewBag.MaDienKeList = new SelectList(madienkeList, "Value", "Text");


            return View(model);
        }
        [HttpGet]
        public ActionResult Delete(string id1)
        {
            if (id1 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Tìm phần tử cần xóa trong cơ sở dữ liệu
            Models.HoaDon itemToDelete = _context.HoaDons.Find(id1);

            if (itemToDelete == null)
            {
                return HttpNotFound();
            }

            return View(itemToDelete);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id1)
        {
            // Tìm phần tử cần xóa trong cơ sở dữ liệu
            Models.HoaDon itemToDelete = _context.HoaDons.Find(id1);

            if (itemToDelete == null)
            {
                return HttpNotFound();
            }

            _context.HoaDons.Remove(itemToDelete); // Xóa phần tử
            _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

            return RedirectToAction("Index");
        }

    }
}
