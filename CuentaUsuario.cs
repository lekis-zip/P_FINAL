static public void MostrarCuentaUsuario()
{
    string opcion = "";

    while (opcion != "3")
    {
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine("              MI CUENTA");
        Console.WriteLine("========================================");
        Console.WriteLine("Nombre   : " + usuariosNombres[usuarioActual]);
        Console.WriteLine("DNI      : " + usuariosDnis[usuarioActual]);
        Console.WriteLine("Teléfono : " + usuariosTelefonos[usuarioActual]);
        Console.WriteLine("Correo   : " + usuariosCorreos[usuarioActual]);
        Console.WriteLine();
        Console.WriteLine("1. Modificar teléfono");
        Console.WriteLine("2. Modificar contraseña");
        Console.WriteLine("3. Volver al panel");
        Console.Write("\nOpción: ");
        opcion = Console.ReadLine();

        if (opcion == "1") ModificarTelefonoUsuario();
        else if (opcion == "2") ModificarContrasenaUsuario();
        else if (opcion != "3")
        {
            Console.WriteLine("ERROR: Seleccione una opción válida.");
            Console.ReadKey();
        }
    }
}

static public void ModificarTelefonoUsuario()
{
    Console.Clear();
    Console.WriteLine("========================================");
    Console.WriteLine("        MODIFICAR TELÉFONO");
    Console.WriteLine("========================================");
    Console.WriteLine("Teléfono actual: " + usuariosTelefonos[usuarioActual]);
    Console.Write("Nuevo teléfono: ");
    string telefono = Console.ReadLine().Trim();

    while (!ValidarTelefono(telefono))
    {
        Console.Write("Nuevo teléfono: ");
        telefono = Console.ReadLine().Trim();
    }

    usuariosTelefonos[usuarioActual] = telefono;
    GuardarUsuarios();
    Console.WriteLine("\nTeléfono modificado correctamente.");
    Console.ReadKey();
}

static public void ModificarContrasenaUsuario()
{
    Console.Clear();
    Console.WriteLine("========================================");
    Console.WriteLine("         MODIFICAR CONTRASEÑA");
    Console.WriteLine("========================================");
    Console.Write("Contraseña actual: ");
    string actual = Console.ReadLine();

    if (actual != usuariosContrasenas[usuarioActual])
    {
        Console.WriteLine("ERROR: La contraseña actual no es correcta.");
        Console.ReadKey();
        return;
    }

    Console.Write("Nueva contraseña: ");
    string nueva = Console.ReadLine();
    while (!ValidarContrasena(nueva))
    {
        Console.Write("Nueva contraseña: ");
        nueva = Console.ReadLine();
    }

    Console.Write("Confirmar nueva contraseña: ");
    string confirmar = Console.ReadLine();
    while (confirmar != nueva)
    {
        Console.WriteLine("ERROR: Las contraseñas no coinciden.");
        Console.Write("Confirmar nueva contraseña: ");
        confirmar = Console.ReadLine();
    }

    usuariosContrasenas[usuarioActual] = nueva;
    GuardarUsuarios();
    Console.WriteLine("\nContraseña modificada correctamente.");
    Console.ReadKey();
}

static public string MostrarHora12(string hora)
{
    DateTime horaConvertida;
    if (DateTime.TryParseExact(hora, "HH:mm", null, System.Globalization.DateTimeStyles.None, out horaConvertida))
    {
        string texto = horaConvertida.ToString("hh:mm tt");
        texto = texto.Replace("AM", "a. m.").Replace("PM", "p. m.");
        return texto;
    }
    return hora;
}
