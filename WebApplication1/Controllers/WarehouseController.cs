using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class WarehouseController : ApiController
    {
        public HttpResponseMessage Get()
        {
         

   
 
            string query = @"
                    select wh_id, warehouse, account_title, addded_by, date_added
                    from
                    dbo.rdf_warehouse where is_active='1'
                    ";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);

          

        }
        // GET: Warehouse
        //public ActionResult Index()
        //{
        //    //return View();
        //}
    }
}