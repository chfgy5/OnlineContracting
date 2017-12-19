using OnlineContracting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using OnlineContracting.Controllers;

namespace OnlineContracting.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class AdminController : Controller
    {
        private contractdataEntities db = new contractdataEntities();
        private ApplicationDbContext UserDb = new ApplicationDbContext();

        // GET: Admin
        public ActionResult Index()
        {
            var model = db.agents;
            return View(model.ToList());
        }

        [HttpPost]
        public void UpdateInfoTable(int id, string column, int value)
        {
            if (ModelState.IsValid)
            {
                var infoline = db.infoes.Find(id);
                // TODO: Think of better way... if possible
                switch (column)
                {
                    case "AR":
                        infoline.AR = value; break;
                    case "AZ":
                        infoline.AZ = value; break;
                    case "CO":
                        infoline.CO = value; break;
                    case "GA":
                        infoline.GA = value; break;
                    case "ID":
                        infoline.ID = value; break;
                    case "IL":
                        infoline.IL = value; break;
                    case "IN":
                        infoline.IN = value; break;
                    case "KS":
                        infoline.KS = value; break;
                    case "MI":
                        infoline.MI = value; break;
                    case "MO":
                        infoline.MO = value; break;
                    case "MS":
                        infoline.MS = value; break;
                    case "NC":
                        infoline.NC = value; break;
                    case "NE":
                        infoline.NE = value; break;
                    case "NM":
                        infoline.NM = value; break;
                    case "NV":
                        infoline.NV = value; break;
                    case "OH":
                        infoline.OH = value; break;
                    case "OK":
                        infoline.OK = value; break;
                    case "PA":
                        infoline.PA = value; break;
                    case "SC":
                        infoline.SC = value; break;
                    case "TX":
                        infoline.TX = value; break;
                    case "UT":
                        infoline.UT = value; break;
                    default:
                        return;
                }// End switch
                db.SaveChanges();
            }
            return;
        }

        [HttpPost]
        public void UpdateFollowUp(int id, string date)
        {
            if (ModelState.IsValid)
            {
                var infoline = db.infoes.Find(id);
                infoline.FollowUp = DateTime.Parse(date);
                db.SaveChanges();
            }
        }

        [HttpPost]
        public void UpdateNote(int id, string note)
        {
            if (ModelState.IsValid)
            {
                var infoline = db.infoes.Find(id);
                infoline.notes = note;
                db.SaveChanges();
            }
        }

        //Gets all advisors based on certain satus. Returns HTML string
        public string GetAdvisorStatus(int status)
        {
            if (ModelState.IsValid)
            {
                string insert = "";
                var infoes = db.infoes.ToArray();
                foreach (var item in infoes)
                {
                    if (item.AR == status)
                    {
                        insert += "<p><strong>" + db.agents.Find(item.agent_id).name + "</strong> requires action with " + item.carrier_name + " in Arizona</p>";
                    }
                    if (item.AZ == status)
                    {
                        insert += "<p><strong>" + db.agents.Find(item.agent_id).name + "</strong> requires action with " + item.carrier_name + " in Arizona</p>";
                    }
                    if (item.CO == status)
                    {
                        insert += "<p><strong>" + db.agents.Find(item.agent_id).name + "</strong> requires action with " + item.carrier_name + " in Colorado</p>";
                    }
                    if (item.GA == status)
                    {
                        insert += "<p><strong>" + db.agents.Find(item.agent_id).name + "</strong> requires action with " + item.carrier_name + " in Georgia</p>";
                    }
                    if (item.ID == status)
                    {
                        insert += "<p><strong>" + db.agents.Find(item.agent_id).name + "</strong> requires action with " + item.carrier_name + " in Idaho</p>";
                    }
                    if (item.IL == status)
                    {
                        insert += "<p><strong>" + db.agents.Find(item.agent_id).name + "</strong> requires action with " + item.carrier_name + " in Illinois</p>";
                    }
                    if (item.IN == status)
                    {
                        insert += "<p><strong>" + db.agents.Find(item.agent_id).name + "</strong> requires action with " + item.carrier_name + " in Indiana</p>";
                    }
                    if (item.KS == status)
                    {
                        insert += "<p><strong>" + db.agents.Find(item.agent_id).name + "</strong> requires action with " + item.carrier_name + " in Kansas</p>";
                    }
                    if (item.MI == status)
                    {
                        insert += "<p><strong>" + db.agents.Find(item.agent_id).name + "</strong> requires action with " + item.carrier_name + " in Michigan</p>";
                    }
                    if (item.MO == status)
                    {
                        insert += "<p><strong>" + db.agents.Find(item.agent_id).name + "</strong> requires action with " + item.carrier_name + " in Missouri</p>";
                    }
                    if (item.MS == status)
                    {
                        insert += "<p><strong>" + db.agents.Find(item.agent_id).name + "</strong> requires action with " + item.carrier_name + " in Mississppi</p>";
                    }
                    if (item.NC == status)
                    {
                        insert += "<p><strong>" + db.agents.Find(item.agent_id).name + "</strong> requires action with " + item.carrier_name + " in North Carolina</p>";
                    }
                    if (item.NM == status)
                    {
                        insert += "<p><strong>" + db.agents.Find(item.agent_id).name + "</strong> requires action with " + item.carrier_name + " in New Mexico</p>";
                    }
                    if (item.NV == status)
                    {
                        insert += "<p><strong>" + db.agents.Find(item.agent_id).name + "</strong> requires action with " + item.carrier_name + " in Neveda</p>";
                    }
                    if (item.OH == status)
                    {
                        insert += "<p><strong>" + db.agents.Find(item.agent_id).name + "</strong> requires action with " + item.carrier_name + " in Ohio</p>";
                    }
                    if (item.OK == status)
                    {
                        insert += "<p><strong>" + db.agents.Find(item.agent_id).name + "</strong> requires action with " + item.carrier_name + " in Oklahoma</p>";
                    }
                    if (item.PA == status)
                    {
                        insert += "<p><strong>" + db.agents.Find(item.agent_id).name + "</strong> requires action with " + item.carrier_name + " in Pennsylvania</p>";
                    }
                    if (item.SC == status)
                    {
                        insert += "<p><strong>" + db.agents.Find(item.agent_id).name + "</strong> requires action with " + item.carrier_name + " in South Carolina</p>";
                    }
                    if (item.TX == status)
                    {
                        insert += "<p><strong>" + db.agents.Find(item.agent_id).name + "</strong> requires action with " + item.carrier_name + " in Texas</p>";
                    }
                    if (item.UT == status)
                    {
                        insert += "<p><strong>" + db.agents.Find(item.agent_id).name + "</strong> requires action with " + item.carrier_name + " in Utah</p>";
                    }
                }
                return insert;
            }
            return "";
        }

        // Begin SuperAdmin Controller
        public ActionResult SuperAdmin()
        {
            return View(UserDb.Users);
        }

        // Built By visual studio. No changes made. Gets view
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = UserDb.Users.Find(id);
            if (User == null)
            {
                return HttpNotFound();
            }
            ViewBag.Name = new SelectList(UserDb.Roles.ToList(), "Name", "Name");
            return View(user);
        }

        // This method is called for edit view in admin folder. This one does saving.
        // Post method that edits the values of the staff member once the proper values are passed through.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection userEdit)
        {
            if (ModelState.IsValid)
            {
                var user = UserDb.Users.Find(userEdit["Id"]);
                user.UserName = userEdit["UserName"];
                user.Email = userEdit["Email"];
                user.PhoneNumber = userEdit["PhoneNumber"];
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(UserDb));
                var roleToRemove = UserDb.Roles.Find(user.Roles.First().RoleId);
                UserManager.AddToRole(user.Id, userEdit["UserRoles"]);
                var result = UserManager.RemoveFromRole(user.Id, roleToRemove.Name);
                if (roleToRemove.Name == "Inactive")
                {
                    var body = "<p>Congragulations on being an approved user/</p>" +
                        "<p>Go to localhost:51732 to begin using our services.";
                    var subject = "You have been activated!";
                    var reciepient = "caleb.fagan@smsteam.net";//user.Email;// may have bug here. not sure
                    new HomeController().SendEmail(body, reciepient, subject);
                }
                UserDb.Entry(user).State = EntityState.Modified;
                UserDb.SaveChanges();
            }
            return RedirectToAction("SuperAdmin");
        }

        // Initializes the Delete staff view.
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = UserDb.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // Post method that deletes the staff member whose id is passed through.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var user = UserDb.Users.Find(id);
            UserDb.Users.Remove(user);
            UserDb.SaveChanges();
            return RedirectToAction("Index");
        }

        // Gets role as string bc cant do that in view?
        public string GetRole(string id)
        {
            if (ModelState.IsValid)
            {
                var user = UserDb.Users.Find(id);
                var roleId = user.Roles.FirstOrDefault().RoleId;
                switch (roleId)
                {
                    case "0":
                        return "Inactive";
                    case "1":
                        return "Agent";
                    case "2":
                        return "Admin";
                    case "3":
                        return "Super Admin";
                    default:
                        return "Unknown";
                }
            }
            return "Bad Request";
        }
    }
}