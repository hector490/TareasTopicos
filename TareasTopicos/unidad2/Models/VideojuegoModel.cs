using System;
using System.ComponentModel;

public class VideojuegoModel
{
    public string Nombre { get; set; } = "";
    public string Consola { get; set; } = "";
    public string Genero { get; set; } = "";
    public DateTime Año { get; set; } = DateTime.Now;
}
