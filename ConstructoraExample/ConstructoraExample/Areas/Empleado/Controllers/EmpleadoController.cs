using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConstructoraExample.Areas.Empleado.Models;
namespace ConstructoraExample.Areas.Empleado.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: Empleado/Empleado
        public ActionResult Index()
        {
            List<EmpleadoViewModel> lstEmpleado = new List<EmpleadoViewModel>();
            using (var ctx = new ConstruramaDBEntitied())
            {
                lstEmpleado = ctx.EMPLEADO.Select(s => new EmpleadoViewModel()
                {
                    id = s.ID,
                    nombre = s.nombre,
                    apellido = s.apellido,
                    sexo = s.sexo,
                    salario = (double)s.salario

                }).ToList<EmpleadoViewModel>(); 

            }


                return View(lstEmpleado);
        }
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmpleadoViewModel empleado)
        {
            if (!ModelState.IsValid)
                return View(empleado);
            using (var datos = new ConstruramaDBEntitied())
            {
                datos.EMPLEADO.Add(new EMPLEADO()
                {
                    ID=empleado.id,
                    nombre=empleado.nombre,
                    apellido=empleado.apellido,
                    sexo=empleado.sexo,
                    salario=(decimal)empleado.salario

                });

                datos.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        public ActionResult Edit(int id)
        {
            EmpleadoViewModel empleadoSend = null;
            using (var datos = new ConstruramaDBEntitied())
            {
                 empleadoSend = datos.EMPLEADO.Where(s => s.ID == id).Select(s => new EmpleadoViewModel() {
                    nombre = s.nombre,
                    apellido = s.apellido,
                    sexo = s.sexo,
                    salario = (double)s.salario
                }).FirstOrDefault<EmpleadoViewModel>();

                if (empleadoSend != null)
                {
                   // empleadoSend = empleado.Res
                }
            }

            return View(empleadoSend);
        }

        [HttpPost]
        public ActionResult Edit(EmpleadoViewModel emp)
        {
            EmpleadoViewModel empleadoSend = null;
            using (var datos = new ConstruramaDBEntitied())
            {
                var empleado = datos.EMPLEADO.Where(s => s.ID == emp.id).FirstOrDefault<EMPLEADO>();

                if (empleado != null)
                {
                    empleado.nombre = emp.nombre;
                    empleado.apellido = emp.apellido;
                    empleado.sexo = emp.sexo;
                    empleado.salario = (decimal)emp.salario;

                    datos.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {

            }

            using (var datos= new ConstruramaDBEntitied())
            {
                var empleado = datos.EMPLEADO.Where(s => s.ID == id).FirstOrDefault();
                datos.Entry(empleado).State = System.Data.Entity.EntityState.Deleted;
                datos.SaveChanges();
                return RedirectToAction("Index");
            }
        }

    }
}