using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConstructoraExample.Contratos.BaseDeDatos;
using ConstructoraExample.Areas.Productos.Models;
using System.Data.SqlClient;

namespace ConstructoraExample.Areas.Productos.Controllers
{
    public class CategoriaController : Controller
    {
        // GET: Productos/Categoria
        public ActionResult Index()
        {
            List<CategoriaViewModel> lstCategoria = new List<CategoriaViewModel>();
            using (SqlConnection conex= ConexionSQLS.OpenConect())
            {
                conex.Open();
                SqlCommand query = new SqlCommand("Select * from Categoria", conex);
                using (var reader = query.ExecuteReader())
                {
                    //lstCategoria = reader.Read();
                }
            }

                return View();
        }
    }
}