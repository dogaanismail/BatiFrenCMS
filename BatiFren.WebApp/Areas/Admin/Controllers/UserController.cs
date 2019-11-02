using BatiFren.Business.Abstract;
using BatiFren.Business.DependencyResolvers.Ninject;
using BatiFren.Entities.EntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BatiFren.WebApp.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        GeneralHelper helper = new GeneralHelper();
        private IMessageService _messageService = InstanceFactory.GetInstance<IMessageService>();
        public ActionResult Index(int? ID)
        {
            if (Session["login"]==null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.UserID = ID;
                return View();
            }
         
        }

        public ActionResult SendMessage(int senderID, string textMessage)
        {
            if (textMessage != "")
            {
                Entities.Message messages = new Entities.Message
                {
                    UserID = senderID,
                    Text = textMessage.ToString(),
                    InsertDate = DateTime.Now
                };
                _messageService.Add(messages);
            }
            return PartialView(_messageService.GetAllLazyWithoutID().OrderBy(x => x.InsertDate).ToList());
        }

        public ActionResult DeleteMessage(int msgID)
        {
            Entities.Message dltMessage = new Entities.Message();
            dltMessage = _messageService.GetByMessageId(msgID);
            _messageService.Delete(dltMessage);
            return PartialView("SendMessage",_messageService.GetAllLazyWithoutID().OrderBy(x => x.InsertDate).ToList());
        }
    }
}