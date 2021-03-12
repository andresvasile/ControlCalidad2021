using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Specifications;
using CC2021Proyecto.ValidationClass;
using CC2021Proyecto.ViewModels;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CC2021Proyecto.Controllers
{
    public class ModelosController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ModelosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var modelos = await _unitOfWork.Repository<Modelo>().ListAllAsync();
            return View(MapViewModel(modelos));
        }

        [HttpPost]
        public async Task<ActionResult> List(string filtro)
        {
            var modelos = await _unitOfWork.Repository<Modelo>().ListAllAsync(); ;
            if (!string.IsNullOrEmpty(filtro))
            {
                var spec = new ModelosConFiltroSpecification(filtro);
                modelos = await _unitOfWork.Repository<Modelo>().ListAsync(spec);

                return View(MapViewModel(modelos));
            }


            return View(MapViewModel(modelos));
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ModeloValidation modeloValidation)
        {
            if (modeloValidation == null) return BadRequest(404);
            if (ModelState.IsValid)
            {
                var modelo = new Modelo
                {
                    Sku = modeloValidation.Sku,
                    Denominacion = modeloValidation.Denominacion,
                    Objetivo = modeloValidation.Objetivo
                };

                var spec = new ModeloConNumeroSpecification(modeloValidation.Sku);
                var modeloValido = await _unitOfWork.Repository<Modelo>().GetEntityWithSpec(spec);
                if (modeloValido == null)
                {
                    _unitOfWork.Repository<Modelo>().Add(modelo);
                    await _unitOfWork.Complete();
                    _unitOfWork.Dispose();
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "El codigo ya existe, ingrese uno nuevo");
            }
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Edit(string sku)
        {
            if (ModelState.IsValid)
            {
                var spec = new ModeloConNumeroSpecification(sku);
                var modeloValido = await _unitOfWork.Repository<Modelo>().GetEntityWithSpec(spec);
                if (modeloValido != null)
                {
                    var modeloValidation = new ModeloValidation()
                    {
                        Sku = modeloValido.Sku,
                        Denominacion = modeloValido.Denominacion,
                        Objetivo = modeloValido.Objetivo
                    };
                    return View(modeloValidation);
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(ModeloValidation modeloValidation)
        {
            if (modeloValidation == null) return BadRequest(404);
            if (ModelState.IsValid)
            {
                var modelo = new Modelo
                {
                    Sku = modeloValidation.Sku,
                    Denominacion = modeloValidation.Denominacion,
                    Objetivo = modeloValidation.Objetivo
                };

                var spec = new ModeloConNumeroSpecification(modelo.Sku);
                var modeloValido = await _unitOfWork.Repository<Modelo>().GetEntityWithSpec(spec);
                if (modeloValido != null)
                {
                    modeloValido.Sku = modelo.Sku;
                    modeloValido.Denominacion = modelo.Denominacion;
                    modeloValido.Objetivo = modelo.Objetivo;
                    _unitOfWork.Repository<Modelo>().Update(modeloValido);
                    await _unitOfWork.Complete();
                    _unitOfWork.Dispose();
                    return RedirectToAction("Index");
                }

            }
            return RedirectToAction("Edit");
        }

        public async Task<IActionResult> Delete(string sku)
        {
            if (ModelState.IsValid)
            {
                var spec = new ModeloConNumeroSpecification(sku);
                var modeloValido = await _unitOfWork.Repository<Modelo>().GetEntityWithSpec(spec);
                if (modeloValido != null)
                {
                    _unitOfWork.Repository<Modelo>().Delete(modeloValido);
                    await _unitOfWork.Complete();
                }
            }
            return RedirectToAction("Index");
        }

        private static ModelosListViewModel MapViewModel(IReadOnlyList<Modelo> modelos)
        {
            return new ModelosListViewModel
            {
                Modelos = modelos
            };
        }
    }
}

