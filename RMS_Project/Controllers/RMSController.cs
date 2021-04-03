using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RMS_Project.Models;

namespace RMS_Project.Controllers
{
    public class RMSController : ApiController
    {
        Res_DBEntities Res_DB = new Res_DBEntities();
        public IHttpActionResult getorder()
        {
            var results = Res_DB.Res_Tab.ToList();
            return Ok(results);
        }

        [HttpPost]
        public IHttpActionResult orderins(Res_Tab orderins)
        {
            Res_DB.Res_Tab.Add(orderins);
            Res_DB.SaveChanges();
            return Ok();
        }

        public IHttpActionResult GetOrderID(int id)
        {
            RMSClass rmsdetails = null;
            rmsdetails = Res_DB.Res_Tab.Where(x => x.O_ID == id).Select(x => new RMSClass()
            {
                O_ID = x.O_ID,
                O_Name = x.O_Name,
                O_Desc = x.O_Desc,
                O_Price = x.O_Price,
                O_Type = x.O_Type,
                O_Stat = x.O_Stat,
                O_Cate = x.O_Cate,
            }).FirstOrDefault<RMSClass>();
            if (rmsdetails == null) 
            {
                return NotFound();
            }
            return Ok(rmsdetails);
        }

        public IHttpActionResult Put(RMSClass rc)
        {
            var updateorder = Res_DB.Res_Tab.Where(x => x.O_ID == rc.O_ID).FirstOrDefault<Res_Tab>();
            if (updateorder != null)
            {
                updateorder.O_ID = rc.O_ID;
                updateorder.O_Name = rc.O_Name;
                updateorder.O_Desc = rc.O_Desc;
                updateorder.O_Price = rc.O_Price;
                updateorder.O_Type = rc.O_Type;
                updateorder.O_Stat = rc.O_Stat;
                updateorder.O_Cate = rc.O_Cate;
                Res_DB.SaveChanges();
            }
            else 
            {
                return NotFound();
            }
            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var orderdel = Res_DB.Res_Tab.Where(x => x.O_ID == id).FirstOrDefault();
            Res_DB.Entry(orderdel).State = System.Data.Entity.EntityState.Deleted;
            Res_DB.SaveChanges();
            return Ok();
        }
    }
}
