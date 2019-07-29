using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using System.Data.SqlClient;
using ConstructoraExample.Contratos.BaseDeDatos;
using ConstructoraExample.Areas.Clientes.Models;
using System.Net;

namespace ConstructoraExample.Areas.Clientes.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Clientes/Cliente
        public ActionResult Index()
        {
            List<ClienteViewModel> lstClientes = new List<ClienteViewModel>();
            List<ClienteViewModel> lstLinqClientes = new List<ClienteViewModel>();

            using (SqlConnection conex = ConexionSQLS.OpenConect())
            {  
                conex.Open();
                SqlCommand query = new SqlCommand("select *from cliente", conex);
                using (var reader = query.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lstClientes.Add(new ClienteViewModel()
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            name= reader["name"].ToString(),
                            lastName=reader["lastName"].ToString(),          
                            sex=reader["sexo"].ToString()

                        });
                    }
                }

            }

            if (lstClientes.Count==0)
            {
                
                ModelState.AddModelError(string.Empty, "Error al obtener los datos");
            }

            lstLinqClientes = (from s in lstClientes
                               where s.sex.Contains("M")
                               select s).ToList<ClienteViewModel>(); 
                              
                              //new { s.ID, s.name, s.lastName, s.sex};  
            
                return View(lstLinqClientes);
               
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ClienteViewModel cliente)
        {
            if (!ModelState.IsValid)
                return View(cliente);
            using (SqlConnection conex = ConexionSQLS.OpenConect())
            {
                SqlCommand query = new SqlCommand("InsertarCliente", conex);
                query.CommandType = CommandType.StoredProcedure;
                query.Parameters.AddWithValue("@ID",cliente.ID);
                query.Parameters.AddWithValue("@name", cliente.name);
                query.Parameters.AddWithValue("@lastName", cliente.lastName);
                query.Parameters.AddWithValue("@sexo", cliente.sex);
                query.Parameters["@ID"].Direction = ParameterDirection.Input;
                query.Parameters["@name"].Direction = ParameterDirection.Input;
                query.Parameters["@lastName"].Direction = ParameterDirection.Input;
                query.Parameters["@sexo"].Direction = ParameterDirection.Input;

                conex.Open();
                SqlDataReader reder;
                reder = query.ExecuteReader();
                conex.Close();
                return RedirectToAction("Index");

            }
        }

        public ActionResult Edit(int id)
        {
            ClienteViewModel cliente = null;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (SqlConnection conex = ConexionSQLS.OpenConect())
            {
                SqlCommand query = new SqlCommand("GetClienteById",conex);
                query.CommandType = CommandType.StoredProcedure;
                query.Parameters.Add("@id", SqlDbType.Int).Value = id;
                conex.Open();
                SqlDataReader reader = query.ExecuteReader();
                if (reader.Read())
                {
                    cliente = new ClienteViewModel()
                    {
                        name = reader["name"].ToString(),
                        lastName = reader["lastName"].ToString(),
                        sex = reader["sexo"].ToString()
                    };
                }
            }

            return View(cliente);
        }
        [HttpPost]
        public ActionResult Edit(ClienteViewModel cliente)
        {
            if (!ModelState.IsValid)
            {
                return View(cliente);
            }
            using (SqlConnection conex = ConexionSQLS.OpenConect())
            {
                SqlCommand query = new SqlCommand("ActualizaCliente", conex);
                query.CommandType = CommandType.StoredProcedure;
                query.Parameters.Add("@id", SqlDbType.Int).Value=cliente.ID;
                query.Parameters.Add("@name", SqlDbType.VarChar).Value = cliente.name;
                query.Parameters.Add("@lastName", SqlDbType.VarChar).Value = cliente.lastName;
                query.Parameters.Add("@sexo", SqlDbType.VarChar).Value = cliente.sex;
                conex.Open();
                SqlDataReader reader;
                reader = query.ExecuteReader();
                conex.Close();
                return RedirectToAction("Index");

            }
        }

        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (SqlConnection conex = ConexionSQLS.OpenConect())
            {
                SqlCommand query = new SqlCommand("EliminaCliente", conex);
                query.CommandType = CommandType.StoredProcedure;
                query.Parameters.Add("@id", SqlDbType.Int).Value= id;
                conex.Open();
                SqlDataReader reader;
                reader = query.ExecuteReader();
                conex.Close();
                return RedirectToAction("Index");


            }
        }

    }
}