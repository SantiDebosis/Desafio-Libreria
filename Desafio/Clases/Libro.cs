
public class Libro
{
    public string Titulo { get; set; }
    public RubroLibro Rubro { get; set; }
    public double Precio { get; set; }
    public int Stock { get; set; }

    public Libro(string titulo, RubroLibro rubro, double precio, int stock)
    {
        Titulo = titulo;
        Rubro = rubro;
        Precio = precio;
        Stock = stock;
    }
}

