using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FarmClassLibrary;
using Poryecto2AVL.viewModels;
using System.IO;

namespace Poryecto2AVL.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            List<Medicamento<int>> medicamentos = new List<Medicamento<int>>();
            var avltree = new AVLTree<int>();
            var fileContents = System.IO.File.ReadAllText(Server.MapPath(@"~/App_Data/csv.txt"));
            foreach (string row in fileContents.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    var medicamento = new Medicamento<int>
                    {

                        id_medicamento = Convert.ToInt32(row.Split(',')[0]),
                        nombre = row.Split(',')[1],
                        descripcion = row.Split(',')[1],
                        casa_productora = row.Split(',')[3],
                        precio = row.Split(',')[4],
                        existencia = Convert.ToInt32(row.Split(',')[5])
                    };
                    avltree.Add(medicamento.id_medicamento, medicamento.nombre);
                }
            }
            IList<int> intList = new List<int>();
            avltree.InOrderTraversal(medicamentos.Add);
            //return Json(medicamentos, JsonRequestBehavior.AllowGet);
            var indexView = new IndexViewModel
            {
                Medicamentos = medicamentos
            };
        return View(indexView);
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFile postedFile)
        {
            List<Medicamento<int>> medicamentos = new List<Medicamento<int>>();
            string filePath = string.Empty;

            if (postedFile != null)
            {
                string path = Server.MapPath("~/uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);


            var avltree = new AVLTree<int>();
            var fileContents = System.IO.File.ReadAllText(Server.MapPath(@"~/App_Data/csv.txt"));
                return Content(fileContents);
            foreach (string row in fileContents.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    var medicamento = new Medicamento<int>
                    {

                        id_medicamento = Convert.ToInt32(row.Split(',')[0]),
                        nombre = row.Split(',')[1],
                        descripcion = row.Split(',')[1],
                        casa_productora = row.Split(',')[3],
                        precio = row.Split(',')[4],
                        existencia = Convert.ToInt32(row.Split(',')[5])
                    };
                    avltree.Add(medicamento.id_medicamento, medicamento.nombre);
                }
            }
            IList<int> intList = new List<int>();
            avltree.InOrderTraversal(medicamentos.Add);
            }
            //return Json(medicamentos, JsonRequestBehavior.AllowGet);
            var indexView = new IndexViewModel
            {
                Medicamentos = medicamentos
            };
            return View(indexView);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}