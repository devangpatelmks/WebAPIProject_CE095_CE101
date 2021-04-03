using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS_Project.Models;
using System.Net.Http;

namespace RMS_Project.Controllers
{
    public class CRUDController : Controller
    {
        // GET: CRUD
        public ActionResult Index()
        {
            IEnumerable<Res_Tab> rmsobj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44399/api/RMS");
            var consumeapi = hc.GetAsync("RMS");
            consumeapi.Wait();
            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<IList<Res_Tab>>();
                displaydata.Wait();
                rmsobj = displaydata.Result;
            }
            return View(rmsobj);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Res_Tab orderins)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44399/api/RMS");
            var insertrecord = hc.PostAsJsonAsync<Res_Tab>("RMS", orderins);
            insertrecord.Wait();
            var savedata = insertrecord.Result;
            if (savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Create");
        }

        public ActionResult Details(int id) 
        {
            RMSClass rmsobj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44399/api/");
            var consumeapi = hc.GetAsync("RMS?id=" + id.ToString());
            consumeapi.Wait();
            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode) 
            {
                var displaydata = readdata.Content.ReadAsAsync<RMSClass>();
                displaydata.Wait();
                rmsobj = displaydata.Result;
            }
            return View(rmsobj);
        }

        public ActionResult Edit(int id)
        {
            RMSClass rmsobj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44399/api/");
            var consumeapi = hc.GetAsync("RMS?id=" + id.ToString());
            consumeapi.Wait();
            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<RMSClass>();
                displaydata.Wait();
                rmsobj = displaydata.Result;
            }
            return View(rmsobj);
        }

        [HttpPost]
        public ActionResult Edit(RMSClass rc)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44399/api/RMS");
            var insertrecord = hc.PutAsJsonAsync<RMSClass>("RMS", rc);
            insertrecord.Wait();
            var savedata = insertrecord.Result;
            if (savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.message = "Order record not updated!";
            }
            return View(rc);
        }

        public ActionResult Delete(int id)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44399/api/RMS");
            var delrecord = hc.DeleteAsync("RMS/" + id.ToString());
            delrecord.Wait();
            var displaydata = delrecord.Result;
            if (displaydata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Index");
        }
    }
}