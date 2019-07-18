using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConstructoraExample.Contratos.BaseDeDatos;
using ConstructoraExample.Areas.Productos.Models;
using System.Data.SqlClient;
using System.Data;
namespace ConstructoraExample.Areas.Productos.Controllers
{
    public class ProductController : Controller
    {
        // GET: Productos/Product
        public ActionResult Index()
        {
            List<ProductoViewModel> lstProductor = new List<ProductoViewModel>();
            using (SqlConnection conex = ConexionSQLS.OpenConect())
            {
                conex.Open();
                SqlCommand query = new SqlCommand("select * from Product", conex);
                using (var reader = query.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lstProductor.Add(new ProductoViewModel()
                            {
                            numero= Convert.ToInt32(reader["Numero"]),
                            nombre=reader["nombre"].ToString(),
                            precio=Convert.ToSingle(reader["precio"]),
                            comentario=reader["comentario"].ToString() 
                            
                            });
                    }
                }
            }


            if (lstProductor.Count == 0)
            {

                ModelState.AddModelError(string.Empty, "Error al obtener los datos");
            }
            return View(lstProductor);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ProductoViewModel producto)
        {
            if (!ModelState.IsValid)
                return View(producto);
            using (SqlConnection conex = ConexionSQLS.OpenConect())
            {
                SqlCommand query = new SqlCommand("MasterProductCrud", conex);
                query.CommandType = CommandType.StoredProcedure;
                query.Parameters.Add("@nombre", SqlDbType.VarChar).Value = producto.nombre;
                query.Parameters.Add("@precio", SqlDbType.Decimal).Value = producto.precio;
                query.Parameters.Add("@comentario", SqlDbType.VarChar).Value = producto.comentario;

                conex.Open();
                query.ExecuteNonQuery();
                conex.Close();
                return RedirectToAction("Index");
            }
        }
    }
}