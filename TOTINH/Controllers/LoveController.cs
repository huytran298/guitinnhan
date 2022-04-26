using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TOTINH.Models;

namespace TOTINH.Controllers
{
    public class LoveController : Controller
    {
        // GET: Love
        [HttpGet]
        public ActionResult GHILOI()
        {
            ViewBag.Check = false;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Controller lấy tin nhắn gửi về Model
        public ActionResult GHILOI(LoiNhan value_message)
        {
            //sau khi submit sẽ kiểm tra đã nhập tên và tin nhắn chưa
           if(!String.IsNullOrEmpty(value_message.yourname) && !String.IsNullOrEmpty(value_message.message))
            {
                //khai báo đối tượng send để đẩy tin nhắn và tên về Model
                var send = new LoiNhan();
                send.add_message(value_message.yourname, value_message.message);
                
                
                ModelState.Clear();
                ViewBag.Check = true;
                ViewBag.Message = "Link";
                ViewBag.Controller = Url.Action("NHANLOINHAN", "Love", new { id = send.id_message }, this.Request.Url.Scheme);
                return View();
            }else ViewBag.Check = false;
            return View(value_message);
        }
        [HttpGet]
        //Controller kiểm tra id tin nhắn đã có chưa nếu chưa thì quay về phần gửi tin nhắn
        public ActionResult NHANLOINHAN(int id)
        {
            ViewBag.Check = false;
            if(id == null)
            {
                return RedirectToAction("GHILOI", "Love");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        //Controller đưa tin nhắn lên View nếu HttpGet có được id
        public ActionResult NHANLOINHAN(int id, LoiNhan reciever)
        {
            
          
            // nếu mật khẩu trùng với mật khẩu của tin nhắn 
            if(reciever.password == reciever.get_password(id))
            {
                //đẩy tin nhắn lên View
                string message_reciever = reciever.get_message(id);
                ViewBag.Message = message_reciever;
                ViewBag.name = reciever.get_password(id);
                ViewBag.Check = true;
                return View();
                
            }
            else
            {
                //nếu nhập sai mật khẩu Controller đẩy thông báo lên View
                ViewBag.Check = false;
                ModelState.AddModelError(String.Empty, "mật khẩu bị sai, vui lòng nhập lại");
            }
            
            return View(reciever);
        }

    }
}