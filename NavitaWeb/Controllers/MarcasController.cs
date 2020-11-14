using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NavitaWeb.Models;
using NavitaWeb.Repository.IRepository;

namespace NavitaWeb.Controllers
{
    [Authorize]
    public class MarcasController : Controller
    {
        #region Construtor/Injection
        private readonly IMarcaRepository _npRepo;

        public MarcasController(IMarcaRepository npRepo)
        {
            _npRepo = npRepo;
        }
        #endregion

        #region Listagem
        public IActionResult Index()
        {
            return View( new Marca() { });
        }

        public async Task<IActionResult> GetAllMarcas()
        {
            //WorkFlow

            var model = await _npRepo.GetAllAsync(SD.MarcaAPIPath, HttpContext.Session.GetString("JWToken"));
            return Json(new { data = model });
        }
        #endregion

        #region ADD e update
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Upsert(int? id)
        {
            Marca obj = new Marca();

            if(id == null)
            {
                //Insert Or create
                return View(obj);
            }
            obj = await _npRepo.GetAsync(SD.MarcaAPIPath, id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken"));

            if(obj == null)
            {
                //Edit or Update
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Marca obj)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    obj.Picture = p1;
                }

                if (obj.Id == 0)
                {
                    obj.Created = DateTime.Now;
                    await _npRepo.CreateAsync(SD.MarcaAPIPath, obj, HttpContext.Session.GetString("JWToken"));
                }
                else
                {
                    await _npRepo.UpdateAsync(SD.MarcaAPIPath + obj.Id, obj, HttpContext.Session.GetString("JWToken"));
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(obj);
            }
        }
        #endregion

        #region Deletar
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _npRepo.DeleteAsync(SD.MarcaAPIPath, id, HttpContext.Session.GetString("JWToken"));
            if (status)
            {
                return Json(new { success = true, message = "Deleted Successfull" });
            }
            return Json(new { success = false, message = "Delete not Successfull" });
        }
        #endregion
    }
}