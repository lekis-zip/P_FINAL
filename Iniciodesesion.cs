static public bool IniciarSesionUsuario()
{
    Console.Clear();
    Console.WriteLine("========================================");
    Console.WriteLine("          INICIAR SESIÓN");
    Console.WriteLine("========================================");

    Console.Write("\nCorreo electrónico: ");
    string correo = Console.ReadLine().Trim();
    Console.Write("Contraseña: ");
    string contrasena = Console.ReadLine();

    for (int i = 0; i < cantidadUsuarios; i++)
    {
        if (usuariosCorreos[i].ToLower() == correo.ToLower())
        {
            if (usuariosBloqueados[i])
            {
                Console.WriteLine("\nCUENTA BLOQUEADA.");
                Console.WriteLine("Debe recuperar su contraseña para desbloquearla.");
                RecuperarContrasenaUsuario();
                return false;
            }

            if (usuariosContrasenas[i] != contrasena)
            {
                intentosFallidos[i]++;

                if (intentosFallidos[i] >= 3)
                {
                    usuariosBloqueados[i] = true;
                    Console.WriteLine("\nCUENTA BLOQUEADA POR 3 INTENTOS INCORRECTOS.");
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                    Console.WriteLine("Recupere su contraseña para desbloquearla.");
                    RecuperarContrasenaUsuario();
                    return false;
                }

                Console.WriteLine("\nContraseña incorrecta.");
                Console.WriteLine("Intentos restantes: " + (3 - intentosFallidos[i]));
                Console.ReadKey();
                return false;
            }

            intentosFallidos[i] = 0;
        }
        if (usuariosCorreos[i].ToLower() == correo.ToLower() && usuariosContrasenas[i] == contrasena)
        {
            usuarioActual = i;
            Console.WriteLine("\nInicio de sesión correcto.");
            Console.WriteLine("Bienvenido(a), " + usuariosNombres[i] + ".");
            Console.ReadKey();
            return true;
        }
    }

    Console.WriteLine("\nERROR: Correo o contraseña incorrectos.");
    Console.WriteLine("¿Olvidaste tu contraseña?");
    Console.WriteLine("1. Sí, recuperar contraseña");
    Console.WriteLine("2. No, volver");
    Console.Write("Opción: ");
    string opcionRecuperacion = Console.ReadLine();

    if (opcionRecuperacion == "1")
    {
        RecuperarContrasenaUsuario();
    }

    return false;
   
}
