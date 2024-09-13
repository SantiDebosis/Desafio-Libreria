public class Usuario
{
    public string NombreUsuario { get; set; }
    public string Password { get; set; }
    public bool EsClienteRegistrado { get; set; }

    public Usuario(string nombreUsuario, string password, bool esClienteRegistrado)
    {
        NombreUsuario = nombreUsuario;
        Password = password;
        EsClienteRegistrado = esClienteRegistrado;
    }
}
