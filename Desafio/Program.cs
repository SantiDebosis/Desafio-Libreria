
public class SistemaLibreria

    //Usuario: Santi08
    //Contraseña: Utn2024
{
    private List<Usuario> usuarios = new List<Usuario>();
    private List<Libro> inventario = new List<Libro>();

    public SistemaLibreria()
    {
        InicializarSistema();
    }

    public void EjecutarPrograma()
    {
        Console.WriteLine("Bienvenido a la Librería Online");
        Usuario usuario = LoguearUsuario();
        if (usuario != null)
        {
            Pedido pedido = new Pedido(usuario);
            MostrarRubros();
            RubroLibro rubroSeleccionado = (RubroLibro)Enum.Parse(typeof(RubroLibro), Console.ReadLine());
            MostrarCatalogo(rubroSeleccionado, pedido);

            Console.WriteLine("¿Desea confirmar la compra? (s/n)");
            if (Console.ReadLine().ToLower() == "s")
            {
                if (VerificarStock(pedido))
                {
                    pedido.CalcularTotal();
                    pedido.MostrarDetalle();
                    IPago pago = SolicitarDatosPago();
                    if (pago.ProcesarPago())
                    {
                        GuardarCompra(pedido);
                        ActualizarStock(pedido);
                        Console.WriteLine("Compra finalizada con éxito.");
                    }
                }
                else
                {
                    Console.WriteLine("Compra cancelada.");
                }
            }
        }
    }

    private void InicializarSistema()
    {
        usuarios.Add(new Usuario("Santi08", "Utn2024", true));

        inventario.Add(new Libro("La maquina del tiempo", RubroLibro.Ficcion, 500, 10));
        inventario.Add(new Libro("Sapiens", RubroLibro.Historia, 300, 5));
        inventario.Add(new Libro("Soy leyenda", RubroLibro.Ficcion, 450, 2));
        inventario.Add(new Libro("Harold McGee", RubroLibro.Cocina, 400, 5));
        inventario.Add(new Libro("La divina comedia", RubroLibro.Poesia, 400, 5));
    }

    private Usuario LoguearUsuario()
    {
        Console.WriteLine("Ingrese nombre de usuario:");
        string nombreUsuario = Console.ReadLine();
        Console.WriteLine("Ingrese contraseña:");
        string password = Console.ReadLine();

        foreach (var usuario in usuarios)
        {
            if (usuario.NombreUsuario == nombreUsuario && usuario.Password == password && usuario.EsClienteRegistrado)
            {
                Console.WriteLine("Login exitoso.");
                return usuario;
            }
        }

        Console.WriteLine("Usuario no registrado.");
        return null;
    }

    private void MostrarRubros()
    {
        Console.WriteLine("Seleccione un rubro de libros: ");
        foreach (var rubro in Enum.GetValues(typeof(RubroLibro)))
        {
            Console.WriteLine($"- {rubro}");
        }
    }

    private void MostrarCatalogo(RubroLibro rubro, Pedido pedido)
    {
        Console.WriteLine($"Libros disponibles en el rubro {rubro}:");
        foreach (var libro in inventario)
        {
            if (libro.Rubro == rubro)
            {
                Console.WriteLine($"{libro.Titulo} - ${libro.Precio} (Stock: {libro.Stock})");
            }
        }

        string continuar;
        do
        {
            Console.WriteLine("Ingrese el título del libro que desea comprar: ");
            string tituloLibro = Console.ReadLine();
            Libro libroSeleccionado = inventario.Find(l => l.Titulo == tituloLibro);
            if (libroSeleccionado != null)
            {
                Console.WriteLine("Ingrese la cantidad: ");
                int cantidad = int.Parse(Console.ReadLine());
                pedido.AgregarLibro(libroSeleccionado, cantidad);
            }

            Console.WriteLine("¿Desea agregar otro libro? (s/n)");
            continuar = Console.ReadLine();
        } while (continuar.ToLower() == "s");
    }

    private bool VerificarStock(Pedido pedido)
    {
        foreach (var item in pedido.LibrosSeleccionados)
        {
            if (item.libro.Stock < item.cantidad)
            {
                Console.WriteLine($"No hay suficiente stock de {item.libro.Titulo}. Stock actual: {item.libro.Stock}. Modifique la cantidad o elimine el libro.");
                return false;
            }
        }
        return true;
    }

    private IPago SolicitarDatosPago()
    {
        Console.WriteLine("Ingrese los datos de la tarjeta de crédito: ");
        Console.WriteLine("Número de tarjeta: ");
        string numeroTarjeta = Console.ReadLine();
        Console.WriteLine("Fecha de vencimiento (MM/AA): ");
        string fechaVencimiento = Console.ReadLine();
        Console.WriteLine("Código de seguridad: ");
        string codigoSeguridad = Console.ReadLine();

        return new PagoConTarjeta(numeroTarjeta, fechaVencimiento, codigoSeguridad);
    }

    private void GuardarCompra(Pedido pedido)
    {
        Console.WriteLine("Compra guardada en el sistema.");
    }

    private void ActualizarStock(Pedido pedido)
    {
        foreach (var item in pedido.LibrosSeleccionados)
        {
            item.libro.Stock -= item.cantidad;
        }
        Console.WriteLine("Stock actualizado.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        SistemaLibreria sistema = new SistemaLibreria();
        sistema.EjecutarPrograma();
    }
}

