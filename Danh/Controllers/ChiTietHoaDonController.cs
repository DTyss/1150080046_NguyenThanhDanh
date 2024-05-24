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
    public class ChiTietHoaDonController : Controller
    {
        Models.DanhEntities1 _context = new Models.DanhEntities1(); // Tạo đối tượng để truy cập dữ liệu

        public ActionResult Index()


        {

            var listOfData = _context.ChiTietHoaDons.ToList(); // Lấy tất cả dữ liệu từ bảng HANGHOA
            return View(listOfData);
        }

        [HttpGet]
        public ActionResult Create()
        {

            var sohoadonList = _context.HoaDons.Select(x => new SelectListItem { Value = x.SoHoaDon.ToString(), Text = x.SoHoaDon }).ToList();
            ViewBag.SoHoaDonList = new SelectList(sohoadonList, "Value", "Text");
            var madongiaList = _context.DonGias.Select(x => new SelectListItem { Value = x.MaDonGia.ToString(), Text = x.MaDonGia }).ToList();
            ViewBag.MaDonGiaList = new SelectList(madongiaList, "Value", "Text");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.ChiTietHoaDon model)
        {
            if (ModelState.IsValid) // Kiểm tra xem dữ liệu được gửi từ form có hợp lệ không
            {
                try
                {




                    var sohoadon = _context.HoaDons.Find(model.SoHoaDon);
                    var madongia = _context.DonGias.Find(model.MaDonGia);


                    if (sohoadon == null)
                    {
                        ModelState.AddModelError("SoHoaDon", "Mã hoa không hợp lệ.");
                    }
                    if (madongia == null)
                    {
                        ModelState.AddModelError("MaDonGia", "Mã hoa không hợp lệ.");
                    }


                    if (ModelState.IsValid) // Kiểm tra xem sau khi kiểm tra mã hoa và mã khu vực, ModelState có còn hợp lệ không
                    {
                        // Thêm model vào cơ sở dữ liệu
                        _context.ChiTietHoaDons.Add(model);
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
            var sohoadonList = _context.HoaDons.Select(x => new SelectListItem { Value = x.SoHoaDon.ToString(), Text = x.SoHoaDon }).ToList();
            ViewBag.SoHoaDonList = new SelectList(sohoadonList, "Value", "Text");
            var madongiaList = _context.DonGias.Select(x => new SelectListItem { Value = x.MaDonGia.ToString(), Text = x.MaDonGia }).ToList();
            ViewBag.MaDonGiaList = new SelectList(madongiaList, "Value", "Text");

            return View(model); // Trả về View với model để hiển thị lại dữ liệu đã nhập
        }
        [HttpGet]
        public ActionResult Delete(string id1)
        {
            if (id1 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Tìm phần tử cần xóa trong cơ sở dữ liệu
            Models.ChiTietHoaDon itemToDelete = _context.ChiTietHoaDons.Find(id1);

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
            Models.ChiTietHoaDon itemToDelete = _context.ChiTietHoaDons.Find(id1);

            if (itemToDelete == null)
            {
                return HttpNotFound();
            }

            _context.ChiTietHoaDons.Remove(itemToDelete); // Xóa phần tử
            _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

            return RedirectToAction("Index");
        }
    }
}
