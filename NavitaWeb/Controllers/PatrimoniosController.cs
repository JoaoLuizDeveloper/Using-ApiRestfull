using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NavitaWeb.Models;
using NavitaWeb.Models.ViewModel;
using NavitaWeb.Repository;
using NavitaWeb.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace NavitaWeb.Controllers
{
    [Authorize]
    public class PatrimoniosController : Controller
    {
        #region Construtor/Injection
        private readonly IMarcaRepository _npRepo;
        private readonly IPatrimonioRepository _npPatrimonio;

        public PatrimoniosController(IMarcaRepository npRepo, IPatrimonioRepository npPatrimonio)
        {
            _npRepo = npRepo;
            _npPatrimonio = npPatrimonio;
        }
        #endregion

        #region Listagem
        public IActionResult Index()
        {
            return View( new Patrimonio() { });
        }

        public async Task<IActionResult> GetAllPatrimonios()
        {
            var model = await _npPatrimonio.GetAllAsync(SD.PatrimonioAPIPath, HttpContext.Session.GetString("JWToken"));
            return Json(new { data = model });
        }
        #endregion

        #region Add e update
        public async Task<IActionResult> Upsert(int? id)
        {
            IEnumerable<Marca> npList = await _npRepo.GetAllAsync(SD.MarcaAPIPath, HttpContext.Session.GetString("JWToken"));

            PatrimoniosVM objVM = new PatrimoniosVM()
            {
                MarcasList = npList.Select(i => new SelectListItem {
                    Text = i.Nome,
                    Value = i.Id.ToString()
                }),
                Patrimonio = new Patrimonio()
            };

            if(id == null)
            {
                //Insert Or create
                return View(objVM);
            }
            objVM.Patrimonio = await _npPatrimonio.GetAsync(SD.PatrimonioAPIPath, id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken"));

            if(objVM.Patrimonio == null)
            {
                //Edit or Update
                return NotFound();
            }
            return View(objVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(PatrimoniosVM obj)
        {
            if (ModelState.IsValid)
            {
                if(obj.Patrimonio.Id == 0)
                {
                    obj.Patrimonio.Created = DateTime.Now;
                    obj.Patrimonio.NumeroTombo = new Random().Next();
                    await _npPatrimonio.CreateAsync(SD.PatrimonioAPIPath, obj.Patrimonio, HttpContext.Session.GetString("JWToken"));
                }
                else
                {
                    await _npPatrimonio.UpdateAsync(SD.PatrimonioAPIPath + obj.Patrimonio.Id, obj.Patrimonio, HttpContext.Session.GetString("JWToken"));
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                IEnumerable<Marca> npList = await _npRepo.GetAllAsync(SD.MarcaAPIPath, HttpContext.Session.GetString("JWToken"));

                PatrimoniosVM objVM = new PatrimoniosVM()
                {
                    MarcasList = npList.Select(i => new SelectListItem
                    {
                        Text = i.Nome,
                        Value = i.Id.ToString()
                    }),
                    Patrimonio = obj.Patrimonio
                };
                return View(objVM);
            }
        }
        #endregion

        #region Deletar
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _npPatrimonio.DeleteAsync(SD.PatrimonioAPIPath, id, HttpContext.Session.GetString("JWToken"));
            if (status)
            {
                return Json(new { success = true, message = "Deletado com sucesso!" });
            }
            return Json(new { success = false, message = "Falha ao deletar!" });
        }
        #endregion
    }
}