using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
namespace ConstructoraExample.Contratos.BaseDeDatos
{
    public class ConexionSQLS
    {
       static string conectionString = string.Empty;
       static string source="SAII";
       static string catalogo= "Construrama";
       static string user;
       static string password;
      // static SqlConnection conexion;

        public static SqlConnection OpenConect()
        {
            conectionString = @"Data Source=" + source + ";Initial Catalog=" + catalogo + ";integrated security=True";
            SqlConnection conexion = new SqlConnection(conectionString);
          //  conexion.Open();
            Console.WriteLine("HAAHAH MAGAZO LOLOGRO");
            return conexion;
        }
    }
}