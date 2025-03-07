using System;
using System.Collections.Generic;

namespace API_COCHES.Modelos;

public partial class Coche
{
    public int Id { get; set; }

    public string Marca { get; set; } = null!;

    public string Modelo { get; set; } = null!;

    public string Combustible { get; set; } = null!;

    public int Kilometros { get; set; }

    public decimal Precio { get; set; }

    public DateOnly Fecha { get; set; }

    public string Foto { get; set; } = null!;

    public bool Disponible { get; set; }

    public string Matricula { get; set; } = null!;
}
