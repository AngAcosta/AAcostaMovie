﻿using System;
using System.Collections.Generic;

namespace DL;

public partial class Cine
{
    public int IdCine { get; set; }

    public decimal? Latitud { get; set; }

    public decimal? Longitud { get; set; }

    public string? Direccion { get; set; }

    public decimal? Venta { get; set; }

    public int? IdZona { get; set; }

    //sql
    public string NombreZona { get; set; }
}
