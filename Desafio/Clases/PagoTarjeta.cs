public class PagoConTarjeta : IPago
{
    public string NumeroTarjeta { get; set; }
    public string FechaVencimiento { get; set; }
    public string CodigoSeguridad { get; set; }

    public PagoConTarjeta(string numeroTarjeta, string fechaVencimiento, string codigoSeguridad)
    {
        NumeroTarjeta = numeroTarjeta;
        FechaVencimiento = fechaVencimiento;
        CodigoSeguridad = codigoSeguridad;
    }

    public bool ProcesarPago()
    {
       
        if (NumeroTarjeta.Length == 16 && CodigoSeguridad.Length == 3)
        {
            Console.WriteLine("Pago con tarjeta procesado correctamente.");
            return true;
        }
        else
        {
            Console.WriteLine("Error en el procesamiento del pago con tarjeta.");
            return false;
        }
    }
}

