
public class Pedido
{
    public Usuario Cliente { get; set; }
    public List<(Libro libro, int cantidad)> LibrosSeleccionados { get; set; }
    public double Total { get; private set; }

    public Pedido(Usuario cliente)
    {
        Cliente = cliente;
        LibrosSeleccionados = new List<(Libro libro, int cantidad)>();
        Total = 0;
    }

    public void AgregarLibro(Libro libro, int cantidad)
    {
        LibrosSeleccionados.Add((libro, cantidad));
    }

    public void CalcularTotal()
    {
        Total = 0;
        foreach (var item in LibrosSeleccionados)
        {
            Total += item.libro.Precio * item.cantidad;
        }
    }

    public void MostrarDetalle()
    {
        Console.WriteLine("\nDetalle de la compra:");
        foreach (var item in LibrosSeleccionados)
        {
            Console.WriteLine($"{item.cantidad} x {item.libro.Titulo} - ${item.libro.Precio} c/u");
        }
        Console.WriteLine($"Total: ${Total}");
    }
}

