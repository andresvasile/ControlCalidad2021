using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using API.Specifications;
using CC2021Proyecto.ValidationClass;
using CC2021Proyecto.ViewModels;
using Datos.Data.Interfaces;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CC2021Proyecto.Controllers
{
    public class ColoresController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ColoresController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var colores = await _unitOfWork.Repository<Color>().ListAllAsync();
            
            return View(MapViewModel(colores));
        }

        [HttpPost]
        public async Task<ActionResult> List(string filtro)
        {
            var colores = await _unitOfWork.Repository<Color>().ListAllAsync(); ;
            if (!string.IsNullOrEmpty(filtro))
            {
                var spec = new ColoresConFiltroSpecification(filtro);
                colores = await _unitOfWork.Repository<Color>().ListAsync(spec);

                return View(MapViewModel(colores));
            }

            return View(MapViewModel(colores));
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ColorValidation colorValidation)
        {
            if (colorValidation == null) return BadRequest(404);
            if (ModelState.IsValid)
            {
                var color = new Color
                {
                    Codigo = colorValidation.Codigo,
                    Descripcion = colorValidation.Descripcion
                };

                var spec = new ColorConNumeroSpecification(color.Codigo);
                var colorValido = await _unitOfWork.Repository<Color>().GetEntityWithSpec(spec);
                if (colorValido == null)
                {
                    _unitOfWork.Repository<Color>().Add(color);
                    await _unitOfWork.Complete();
                    _unitOfWork.Dispose();
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("","El codigo ya existe, ingrese uno nuevo");
            }
            return View();
            
        }

        public async Task<IActionResult> Edit(int codigoColor)
        {
            if (ModelState.IsValid)
            {
                var spec = new ColorConNumeroSpecification(codigoColor);
                var colorValido = await _unitOfWork.Repository<Color>().GetEntityWithSpec(spec);
                if (colorValido != null)
                {
                    var colorValidation = new ColorValidation
                    {
                        Codigo = colorValido.Codigo,
                        Descripcion = colorValido.Descripcion
                    };
                    return View(colorValidation);
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(ColorValidation colorValidation)
        {
            if (colorValidation == null) return BadRequest(404);
            if (ModelState.IsValid)
            {
                var color = new Color
                {
                    Codigo = colorValidation.Codigo,
                    Descripcion = colorValidation.Descripcion
                };

                var spec = new ColorConNumeroSpecification(color.Codigo);
                var colorValido = await _unitOfWork.Repository<Color>().GetEntityWithSpec(spec);
                if (colorValido != null)
                {
                    colorValido.Codigo = color.Codigo;
                    colorValido.Descripcion = color.Descripcion;
                    _unitOfWork.Repository<Color>().Update(colorValido);
                    await _unitOfWork.Complete();
                    _unitOfWork.Dispose();
                    return RedirectToAction("Index");
                }
                
            }
            return RedirectToAction("Edit");
        }

        public async Task<IActionResult> Delete(int codigoColor)
        {
            if (ModelState.IsValid)
            {
                var spec = new ColorConNumeroSpecification(codigoColor);
                var colorValido = await _unitOfWork.Repository<Color>().GetEntityWithSpec(spec);
                if (colorValido != null)
                {
                    _unitOfWork.Repository<Color>().Delete(colorValido);
                    await _unitOfWork.Complete();
                }
            }
            return RedirectToAction("Index");
        }

        private static ColoresListViewModel MapViewModel(IReadOnlyList<Color> colores)
        {
            return new ColoresListViewModel
            {
                Colores = colores
            };
        }
    }
}
