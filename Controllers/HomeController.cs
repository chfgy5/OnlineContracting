using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OnlineContracting.Models;
using System.Net.Mail;
using OnlineContracting.Controllers;
using System.IO;
using System.Net.Mime;

namespace OnlineContracting.Controllers
{
    [Authorize(Roles = "Inactive, Agent, Admin, SuperAdmin")]
    public class HomeController : Controller
    {
        private contractdataEntities db = new contractdataEntities();
        private ApplicationDbContext userdb = new ApplicationDbContext();

        public ActionResult Index()
        {
            return RedirectToHomePage();
        }

        public ActionResult Inactive()
        {
            return View();
        }

        // Redirects to the desired home page depending on the role of the logged in user.
        public ActionResult RedirectToHomePage()
        {
            if (User.Identity.IsAuthenticated)
            {
                var email = User.Identity.GetUserName();
                var user = userdb.Users.FirstOrDefault(u => u.Email == email);
                if (User.IsInRole("Agent"))
                {
                    return RedirectToAction("Index", "Agent", new { id = user.Id });
                }
                else if (User.IsInRole("Admin") || User.IsInRole("SuperAdmin"))
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (User.IsInRole("Inactive"))
                {
                    return RedirectToAction("Inactive", "Home");
                }
            }
            return Redirect("/Account/Login");
        }


        public void DailyEmail()
        {
            if (ModelState.IsValid)
            {
                if (Infoes.Items != null)
                {
                //if (Request.IsLocal)
                //{
                    var current = db.infoes.ToList();
                    var body = "<h3>Test</h3>";
                    for (var i = 0; i < current.Count; i++)
                    {
                        if (current[i].AR != Infoes.Items[i].AR)
                            body += "<p>" + current[i].name + " at " + current[i].carrier_name + " has changed to "
                                + GetStatus(current[i].AR) + " in Arizona</p>";
                        if (current[i].AZ != Infoes.Items[i].AZ)
                            body += "<p>" + current[i].name + " at " + current[i].carrier_name + " has changed to "
                                + GetStatus(current[i].AZ) + " in Arizona</p>";
                        if (current[i].CO != Infoes.Items[i].CO)
                            body += "<p>" + current[i].name + " at " + current[i].carrier_name + " has changed to "
                                + GetStatus(current[i].CO) + " in Arizona</p>";
                        if (current[i].GA != Infoes.Items[i].GA)
                            body += "<p>" + current[i].name + " at " + current[i].carrier_name + " has changed to "
                                + GetStatus(current[i].GA) + " in Arizona</p>";
                        if (current[i].ID != Infoes.Items[i].ID)
                            body += "<p>" + current[i].name + " at " + current[i].carrier_name + " has changed to "
                                + GetStatus(current[i].ID) + " in Arizona</p>";
                        if (current[i].IL != Infoes.Items[i].IL)
                            body += "<p>" + current[i].name + " at " + current[i].carrier_name + " has changed to "
                                + GetStatus(current[i].IL) + " in Arizona</p>";
                        if (current[i].IN != Infoes.Items[i].IN)
                            body += "<p>" + current[i].name + " at " + current[i].carrier_name + " has changed to "
                                + GetStatus(current[i].IN) + " in Arizona</p>";
                        if (current[i].KS != Infoes.Items[i].KS)
                            body += "<p>" + current[i].name + " at " + current[i].carrier_name + " has changed to "
                                + GetStatus(current[i].KS) + " in Arizona</p>";
                        if (current[i].MI != Infoes.Items[i].MI)
                            body += "<p>" + current[i].name + " at " + current[i].carrier_name + " has changed to "
                                + GetStatus(current[i].MI) + " in Arizona</p>";
                        if (current[i].MO != Infoes.Items[i].MO)
                            body += "<p>" + current[i].name + " at " + current[i].carrier_name + " has changed to "
                                + GetStatus(current[i].MO) + " in Arizona</p>";
                        if (current[i].MS != Infoes.Items[i].MS)
                            body += "<p>" + current[i].name + " at " + current[i].carrier_name + " has changed to "
                                + GetStatus(current[i].MS) + " in Arizona</p>";
                        if (current[i].NC != Infoes.Items[i].NC)
                            body += "<p>" + current[i].name + " at " + current[i].carrier_name + " has changed to "
                                + GetStatus(current[i].NC) + " in Arizona</p>";
                        if (current[i].NE != Infoes.Items[i].NE)
                            body += "<p>" + current[i].name + " at " + current[i].carrier_name + " has changed to "
                                + GetStatus(current[i].NE) + " in Arizona</p>";
                        if (current[i].NM != Infoes.Items[i].NM)
                            body += "<p>" + current[i].name + " at " + current[i].carrier_name + " has changed to "
                                + GetStatus(current[i].NM) + " in Arizona</p>";
                        if (current[i].NV != Infoes.Items[i].NV)
                            body += "<p>" + current[i].name + " at " + current[i].carrier_name + " has changed to "
                                + GetStatus(current[i].NV) + " in Arizona</p>";
                        if (current[i].OH != Infoes.Items[i].OH)
                            body += "<p>" + current[i].name + " at " + current[i].carrier_name + " has changed to "
                                + GetStatus(current[i].OH) + " in Arizona</p>";
                        if (current[i].OK != Infoes.Items[i].OK)
                            body += "<p>" + current[i].name + " at " + current[i].carrier_name + " has changed to "
                                + GetStatus(current[i].OK) + " in Arizona</p>";
                        if (current[i].PA != Infoes.Items[i].PA)
                            body += "<p>" + current[i].name + " at " + current[i].carrier_name + " has changed to "
                                + GetStatus(current[i].PA) + " in Arizona</p>";
                        if (current[i].SC != Infoes.Items[i].SC)
                            body += "<p>" + current[i].name + " at " + current[i].carrier_name + " has changed to "
                                + GetStatus(current[i].SC) + " in Arizona</p>";
                        if (current[i].TX != Infoes.Items[i].TX)
                            body += "<p>" + current[i].name + " at " + current[i].carrier_name + " has changed to "
                                + GetStatus(current[i].TX) + " in Arizona</p>";
                        if (current[i].UT != Infoes.Items[i].UT)
                            body += "<p>" + current[i].name + " at " + current[i].carrier_name + " has changed to "
                                + GetStatus(current[i].UT) + " in Arizona</p>";
                    }

                    var reciepient = "caleb.fagan@smsteam.net";
                    var subject = DateTime.Today.Date + " Daily Report";
                    SendEmail(body, reciepient, subject);
                }
                Infoes.Items = db.infoes.ToList();
            }
        }

        // https://www.mikesdotnetting.com/article/268/how-to-send-email-in-asp-net-mvc
        // Using send instead of mailsendasync as I was having trouble calling method with async tag
        public void SendEmail(string message, string reciepient, string subject)
        {
            string body = string.Empty;
            var Email = new MailMessage();
            if (reciepient != "")
                Email.To.Add(new MailAddress(reciepient));
            else
                return; // If no intended address dont send email.
            
            Email.Subject = subject;
            Email.Body = String.Format(message + signiture);
            Email.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                smtp.Send(Email);
            }
        }

        // int? means its nullable. Odd interacting between database.
        private string GetStatus(int? InfoStatus)
        {
            switch (InfoStatus)
            {
                case 0:
                    return "Not Available";
                case 1:
                    return "Appointed";
                case 2:
                    return "Needs to be Requested";
                case 3:
                    return "Requested";
                default:
                    break;
            }
            return "";
        }

        // SMS Default signiture
        string signiture = "<table><tbody><tr><td><a href='http://www.smsteam.net/'><h3 style='color:#055aac'>Senior Marketing Specialists</h2></a></td></tr><tr><td>" +
            "<p>801 Gray Oak Drive&nbsp;|&nbsp;&nbsp;Columbia, MO 65201<br /> tel(800) 689-2800&nbsp;&nbsp;|&nbsp;" +
            "&nbsp;fax(800) 569-2011</p></td></tr><tr><td><p><a href = 'http://www.smsteam.net/' ><strong> Homepage " +
            "&nbsp;</strong></a><strong>&nbsp;|&nbsp;</strong><strong>&nbsp;</strong><a href = " +
            "'http://www.sms-university.com/' ><strong> SMS University&nbsp;</strong></a><strong>&nbsp;|&nbsp;</strong>" +
            "<strong>&nbsp;</strong><a href = 'http://www.cvent.com/Events/Calendar/Calendar.aspx?cal=ef540b77-6db7-4b97-96e5-995524cae177' >" +
            "<strong> Agent Engagement Calendar&nbsp;</strong></a><strong>&nbsp;|&nbsp;</strong><strong>&nbsp;</strong>" +
            "<a href = 'https://www.google.com/maps/place/Senior+Marketing+Specialists/@38.913274,-92.330109,17z/data=!3m1!4b1!4m2!3m1!1s0x87dcb70fcd4ea97d:0xef468f5b545b528c' >" +
            "<strong> Map &nbsp;</strong></a><strong>&nbsp;|&nbsp;</strong><strong>&nbsp;</strong><a href = " +
            "'mailto:impmarketing@smsteam.net' ><strong> Email </strong></a></p></td></tr><tr><td><p>" +
            "<strong>Confidentiality Note:</strong>&nbsp;This email may contain confidential and/or private information. " +
            "If you received this email in error, please delete it and notify the sender immediately.</p></td></tr></tbody></table>";
    }

    // Only used to track changes
    static class Infoes
    {
        public static List<info> Items;
    }
}