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
    public class MacroController : ApiController
    {
        // GET: Macro
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public HttpResponseMessage Get()
        {

            //    SELECT emp.p_feed_code,emp.bags_int,emp.batch_int,emp.proddate,
            //emp.feed_type,emp.bagorbin,additional_bin,emp.series_num,emp.prod_id
            //FROM rdf_production_advance emp WHERE emp.is_selected = '1'  AND NOT emp.canceltheapprove IS NOT NULL
            //ORDER BY emp.prod_id ASC


            string query = @"
                SELECT
			emp.item_id,
			emp.item_code,
			emp.item_description,
			emp.Category,
			emp.item_group,
			emp.total_quantity_raw,
			emp.per_bag,
			emp.qty_repack,
			emp.qty_repack_available,
			emp.qty_production,
			emp.active_qty_repack,
			--emp.quantity,
			--sr.supplier,
			--sr.address,
			--		--	sr.email_address,
			--sr.contact_no,
			--sr.email_address,
					emp.date_added,
					emp.price,
	
			emp.expiration_details,
			emp.delivery_details,
			emp.date_added,
			emp.item_location,
			emp.item_remarks,
			emp.item_added_by,
			emp.item_location,
			emp.buffer_of_stocks,
			emp.classification_buffer,
			emp.ordering_buffer,
			ISNULL(t3.SCADA,0) as SCADA,
			ISNULL(t2.MACRESERVED,0) as MACRESERVED,
			ISNULL(t4.MACREPACK,0) as MACREPACK,
			ISNULL(t5.RECEIVING,0) as RECEIVING,
			ISNULL(t6.ISSUE,0) as ISSUE,
			ISNULL(t7.OUTING,0) as OUTING,
			ISNULL(t5.RECEIVING,0) + ISNULL(t6.ISSUE,0) as TOTAL_RECEIVED,
			ISNULL(t5.RECEIVING,0) + ISNULL(t6.ISSUE,0) - ISNULL(t2.MACRESERVED,0) - ISNULL(t7.OUTING,0) as RESERVED,
	        (ISNULL(t6.ISSUE,0) * 1  +ISNULL(t5.RECEIVING,0)) - (ISNULL(t3.SCADA,0)  + ISNULL(t7.OUTING,0) + ISNULL(t4.MACREPACK,0))   as ONHAND,
						CONVERT(date, emp.last_used, 101) as LAST_USED,
			DATEDIFF(DAY,CONVERT(date, emp.last_used, 101),CAST(GETDATE() as date)) as MOVEMENT,
			emp.report,
		(ISNULL(t5.RECEIVING,0) + ISNULL(t6.ISSUE,0) - ISNULL(t2.MACRESERVED,0) - ISNULL(t7.OUTING,0) - 	CAST(emp.buffer_of_stocks as float))/100 *100 as DIF,
				ISNULL(t75.QA_RECEIVING,0) as QA_RECEIVING
		FROM rdf_raw_materials emp
	
	
		         LEFT JOIN ( select BC.theo_item_code, sum( BC.actual_qty ) as SCADA
                        from theo_logs_tbl BC where CAST(date_time as date) BETWEEN '2021-01-12' and GETDATE()+30
                        group by BC.theo_item_code ) t3
            on emp.item_code = t3.theo_item_code


					         LEFT JOIN ( select BC.item_code, sum(CAST(BC.quantity as float)* 2)  as MACRESERVED
                        from rdf_recipe_to_production BC where CAST(BC.proddate as date) BETWEEN '2021-01-12' and GETDATE()+30 and status_of_person IS NULL
                        group by BC.item_code) t2
            on emp.item_code = t2.item_code

								         LEFT JOIN ( select BC.rp_item_code, sum(CAST(BC.total_repack as float))  as MACREPACK
                        from rdf_repackin_entry BC where CAST(BC.production_date as date) BETWEEN '2021-01-12' and GETDATE()+30
                        group by BC.rp_item_code) t4
            on emp.item_code = t4.rp_item_code

							         LEFT JOIN ( select BC.r_item_code, sum(CAST(REPLACE(BC.r_quantity,',','') as float))  as RECEIVING
                        from rdf_microreceiving_entry BC where BC.is_active='1' and BC.transaction_type='PO'
                        group by BC.r_item_code) t5
            on emp.item_code = t5.r_item_code


										         LEFT JOIN ( select BC.r_item_code, sum(CAST(REPLACE(BC.actual_count_good,',','') as float))  as ISSUE
                        from rdf_microreceiving_entry BC where BC.receiving_status='1' and BC.is_active='1' and BC.transaction_type='Miscellaneous Receipt'
                        group by BC.r_item_code) t6
            on emp.item_code = t6.r_item_code

			
										         LEFT JOIN ( select BC.item_code, sum(CAST(REPLACE(BC.qty,',','') as float))  as OUTING
                        from rdf_transaction_out_progress BC where BC.is_active='1'
                        group by BC.item_code) t7
            on emp.item_code = t7.item_code

			
											         LEFT JOIN ( select BC.item_code, sum(CAST(REPLACE(BC.qty_ordered,',','') as float))  as QA_RECEIVING
                        from rdf_po_summary_rpt BC where NOT BC.checklist_approval='Cancel' AND NOT BC.qty_ordered = BC.qty_good AND NOT BC.qty_remarks < 0 AND BC.Password IS NULL
                        group by BC.item_code) t75
            on emp.item_code = t75.item_code

		--LEFT JOIN rdf_production_advance st ON st.prod_id = t2.
		--LEFT JOIN rdf_supplier sr ON emp.supplier = sr.supplier_id
		WHERE emp.is_active = 1 AND emp.Category='MACRO' ORDER BY emp.item_group ASC

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
    }
}