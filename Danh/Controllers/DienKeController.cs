﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Danh.Models;
using System.Net;
using System.Web.Mvc;
using System.Globalization;

namespace Danh.Controllers
{
    public class DienKeController : Controller
    {
        Models.DanhEntities1 _context = new Models.DanhEntities1(); // Tạo đối tượng để truy cập dữ liệu

        public ActionResult Index()


        {

            var listOfData = _context.DienKes.ToList(); // Lấy tất cả dữ liệu từ bảng HANGHOA
            return View(listOfData);
        }

        [HttpGet]
        public ActionResult Create()
        {

            var makhList = _context.KhachHangs.Select(x => new SelectListItem { Value = x.MaKH.ToString(), Text = x.MaKH }).ToList();
            ViewBag.MaKHList = new SelectList(makhList, "Value", "Text");
            var makhuvucList = _context.KhuVucs.Select(x => new SelectListItem { Value = x.MaKhuVuc.ToString(), Text = x.TenKhuVuc }).ToList();
            ViewBag.MaKhuVucList = new SelectList(makhuvucList, "Value", "Text");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.DienKe model)
        {
            if (ModelState.IsValid) // Kiểm tra xem dữ liệu được gửi từ form có hợp lệ không
            {
                try
                {




                    var makh = _context.KhachHangs.Find(model.MaKH);
                    var makhuvuc = _context.KhuVucs.Find(model.MaKhuVuc);


                    if (makh == null)
                    {
                        ModelState.AddModelError("MaNuoc", "Mã hoa không hợp lệ.");
                    }
                    if (makhuvuc == null)
                    {
                        ModelState.AddModelError("MaNuoc", "Mã hoa không hợp lệ.");
                    }


                    if (ModelState.IsValid) // Kiểm tra xem sau khi kiểm tra mã hoa và mã khu vực, ModelState có còn hợp lệ không
                    {
                        // Thêm model vào cơ sở dữ liệu
                        _context.DienKes.Add(model);
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
            var makhList = _context.KhachHangs.Select(x => new SelectListItem { Value = x.MaKH.ToString(), Text = x.MaKH }).ToList();
            ViewBag.MaKHList = new SelectList(makhList, "Value", "Text");
            var makhuvucList = _context.KhuVucs.Select(x => new SelectListItem { Value = x.MaKhuVuc.ToString(), Text = x.TenKhuVuc }).ToList();
            ViewBag.MaKhuVucList = new SelectList(makhuvucList, "Value", "Text");

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
            Models.DienKe itemToDelete = _context.DienKes.Find(id1);

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
            Models.DienKe itemToDelete = _context.DienKes.Find(id1);

            if (itemToDelete == null)
            {
                return HttpNotFound();
            }

            _context.DienKes.Remove(itemToDelete); // Xóa phần tử
            _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

            return RedirectToAction("Index");
        }
    }
}
