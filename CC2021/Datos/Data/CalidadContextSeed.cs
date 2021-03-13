using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Entities.Enums;
using Microsoft.Extensions.Logging;


namespace Datos.Data
{
    public class CalidadContextSeed
    {
        public static async Task SeedAsync(CalidadContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Colores.Any())
                {
                    var coloresData = File.ReadAllText("../Datos/Data/SeedData/colores.json");

                    var colores = JsonSerializer.Deserialize<List<Color>>(coloresData);

                    foreach (var item in colores)
                    {
                        context.Colores.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Modelos.Any())
                {
                    var modelosData = File.ReadAllText("../Datos/Data/SeedData/modelos.json");

                    var modelos = JsonSerializer.Deserialize<List<Modelo>>(modelosData);

                    foreach (var item in modelos)
                    {
                        context.Modelos.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
                /*if (!context.Ordenes.Any())
                {
                    var ordenesData = File.ReadAllText("../Datos/Data/SeedData/ordenes.json");

                    var ordenes = JsonSerializer.Deserialize<List<OrdenDeProduccion>>(ordenesData);

                    foreach (var item in ordenes)
                    {
                        context.Ordenes.Add(item);
                    }

                    await context.SaveChangesAsync();
                }*/
                if (!context.Defectos.Any())
                {
                    var defectosData = File.ReadAllText("../Datos/Data/SeedData/defectos.json");

                    var defectos = JsonSerializer.Deserialize<List<Defecto>>(defectosData);

                    foreach (var item in defectos)
                    {
                        
                        var defecto = new Defecto
                        {
                            TipoDefecto = (TipoDefecto)Enum.Parse(typeof(TipoDefecto), item.TipoDefecto.ToString()),
                            Descripcion = item.Descripcion
                            
                        };
                        context.Defectos.Add(defecto);
                    }

                    await context.SaveChangesAsync();
                }
                if (!context.Turnos.Any())
                {
                    var turnosData = File.ReadAllText("../Datos/Data/SeedData/turnos.json");

                    var turnos = JsonSerializer.Deserialize<List<Turno>>(turnosData);

                    foreach (var item in turnos)
                    {
                        context.Turnos.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Usuarios.Any())
                {
                        var andres = new Usuario()
                        {
                            DisplayName = "Andres",
                            User = "andres",
                            Password = "Pa$$w0rd",
                            Empleado = new Empleado()
                            {
                                Nombre = "Andres",
                                Apellido = "Vasile",
                                Documento = 12345678,
                                Email = "andres@gmail.com",
                                Rol = (TipoEmpleado)Enum.Parse(typeof(TipoEmpleado),TipoEmpleado.Administrativo.ToString())
                            }
                        };

                        context.Usuarios.Add(andres);

                        var victor = new Usuario()
                        {
                            DisplayName = "Victor",
                            User = "victor",
                            Password = "Pa$$w0rd",
                            Empleado = new Empleado()
                            {
                                Nombre = "Victor",
                                Apellido = "Bejas",
                                Documento = 87654321,
                                Email = "victor@gmail.com",
                                Rol = (TipoEmpleado)Enum.Parse(typeof(TipoEmpleado), TipoEmpleado.SupervisorDeCalidad.ToString())
                            }
                            
                        };

                        context.Usuarios.Add(victor);

                        var sergio = new Usuario()
                        {
                            DisplayName = "Sergio",
                            User = "sergio",
                            Password = "Pa$$w0rd",
                            Empleado = new Empleado()
                            {
                                Nombre = "Sergio",
                                Apellido = "Rubino",
                                Documento = 12312312,
                                Email = "sergio@gmail.com",
                                Rol = (TipoEmpleado)Enum.Parse(typeof(TipoEmpleado), TipoEmpleado.SupervisorDeLinea.ToString())
                            }
                        };
                        context.Usuarios.Add(sergio);

                        var diego = new Usuario()
                        {
                            DisplayName = "Diego",
                            User = "diego",
                            Password = "Pa$$w0rd",
                            Empleado = new Empleado()
                            {
                                Nombre = "Diego",
                                Apellido = "Cos",
                                Documento = 12312312,
                                Email = "diego@gmail.com",
                                Rol = (TipoEmpleado)Enum.Parse(typeof(TipoEmpleado), TipoEmpleado.SupervisorDeLinea.ToString())
                            }
                        };
                        context.Usuarios.Add(diego);

                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<CalidadContext>();
                logger.LogError(ex.Message);
            }
        }
    }
}