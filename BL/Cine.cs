﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Cine
    {
        public static ML.Result Add(ML.Cine cine)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AacostaCineContext context = new DL.AacostaCineContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"CineAdd '{cine.Latitud}','{cine.Longitud}','{cine.Direccion}','{cine.Venta}', '{cine.Zona.IdZona}'");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se Inserto el Cine";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;
        }
        public static ML.Result Update(ML.Cine cine)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AacostaCineContext context = new DL.AacostaCineContext())
                {

                    int query = context.Database.ExecuteSqlRaw($"CineUpdate  '{cine.IdCine}','{cine.Latitud}','{cine.Longitud}','{cine.Direccion}','{cine.Venta}', '{cine.Zona.IdZona}'");

                    if (query >= 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se Modifico el Cine";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;

        }
        public static ML.Result Delete(int IdCine)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AacostaCineContext context = new DL.AacostaCineContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"CineDelete {IdCine}");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se Elimino el Cine";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;
        }
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AacostaCineContext context = new DL.AacostaCineContext())
                {
                    var query = context.Cines.FromSqlRaw("CineGetAll").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Cine cine = new ML.Cine();

                            cine.IdCine = obj.IdCine;
                            cine.Latitud = obj.Latitud.Value;
                            cine.Longitud = obj.Longitud.Value;
                            cine.Direccion = obj.Direccion;
                            cine.Venta = obj.Venta.Value;

                            cine.Zona = new ML.Zona();
                            cine.Zona.IdZona = obj.IdZona.Value;
                            cine.Zona.Nombre = obj.NombreZona;

                            result.Objects.Add(cine);
                        }
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Exception = ex;

            }
            return result;
        }
        public static ML.Result GetById(int IdCine)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AacostaCineContext context = new DL.AacostaCineContext())
                {
                    var query = context.Cines.FromSqlRaw($"CineGetById {IdCine}").AsEnumerable().FirstOrDefault();


                    if (query != null)
                    {
                        ML.Cine cine = new ML.Cine();

                        cine.IdCine = query.IdCine;
                        cine.Latitud = query.Latitud.Value;
                        cine.Longitud = query.Longitud.Value;
                        cine.Direccion = query.Direccion;
                        cine.Venta = query.Venta.Value;

                        cine.Zona = new ML.Zona();
                        cine.Zona.IdZona = query.IdZona.Value;
                        cine.Zona.Nombre = query.NombreZona;


                        result.Object = cine;
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public static ML.Result Porcentaje()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AacostaCineContext context = new DL.AacostaCineContext())
                {
                    var query = context.Cines.FromSqlRaw("CineGetAll").ToList();

                    if (query != null)
                    {
                        ML.Cine cine = new ML.Cine();
                        cine.Zona = new ML.Zona();

                        decimal sumatotal = 0, sumat;
                        decimal sumanorte = 0, suman;
                        decimal sumasur = 0, sumas;
                        decimal sumaeste = 0, sumae;
                        decimal sumaoeste = 0, sumao;

                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            sumatotal += obj.Venta.Value;

                            if (obj.IdZona == 1)
                            {
                                sumanorte += obj.Venta.Value;
                            }
                            if (obj.IdZona == 2)
                            {
                                sumasur += obj.Venta.Value;
                            }
                            if (obj.IdZona == 3)
                            {
                                sumaeste += obj.Venta.Value;

                            }
                            if (obj.IdZona == 4)
                            {

                                sumaoeste += obj.Venta.Value;

                            }
                        }

                        sumat = sumatotal;
                        suman = sumanorte;
                        sumas = sumasur;
                        sumae = sumaeste;
                        sumao = sumaoeste;

                        cine.porcentajeN = suman / sumat * 100;
                        cine.porcentajeS = sumas / sumat * 100;
                        cine.porcentajeE = sumae / sumat * 100;
                        cine.porcentajeO = sumao / sumat * 100;

                        result.Object = cine;
                        //porcentajeT = porcentajeN + porcentajeS + porcentajeE + porcentajeO;
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }
    }
}