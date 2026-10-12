 static public void RegistrarUsuario()
 {
     Console.Clear();
     Console.WriteLine("========================================");
     Console.WriteLine("          REGISTRO DE USUARIO");
     Console.WriteLine("========================================");

     string nombre;
     Console.Write("\nNombre completo: ");
     nombre = Console.ReadLine().Trim();
     while (!ValidarNombre(nombre))
     {
         Console.Write("Nombre completo: ");
         nombre = Console.ReadLine().Trim();
     }

     string dni;
     Console.Write("DNI: ");
     dni = Console.ReadLine().Trim();
     while (!ValidarDNI(dni) || DNIUsuarioRegistrado(dni))
     {
         Console.Write("DNI: ");
         dni = Console.ReadLine().Trim();
     }

     string telefono;
     Console.Write("Teléfono: ");
     telefono = Console.ReadLine().Trim();
     while (!ValidarTelefono(telefono))
     {
         Console.Write("Teléfono: ");
         telefono = Console.ReadLine().Trim();
     }

     string correo;
     Console.Write("Correo electrónico: ");
     correo = Console.ReadLine().Trim();
     while (!ValidarCorreo(correo) || CorreoUsuarioRegistrado(correo))
     {
         Console.Write("Correo electrónico: ");
         correo = Console.ReadLine().Trim();
     }

     string contrasena;
     Console.Write("Crear contraseña para Mesa de Partes: ");
     contrasena = Console.ReadLine();
     while (!ValidarContrasena(contrasena))
     {
         Console.Write("Crear contraseña para Mesa de Partes: ");
         contrasena = Console.ReadLine();
     }

     Console.Write("Confirmar contraseña: ");
     string confirmar = Console.ReadLine();
     while (confirmar != contrasena)
     {
         Console.WriteLine("ERROR: Las contraseñas no coinciden.");
         Console.Write("Confirmar contraseña: ");
         confirmar = Console.ReadLine();
     }

     usuariosNombres[cantidadUsuarios] = nombre;
     usuariosDnis[cantidadUsuarios] = dni;
     usuariosTelefonos[cantidadUsuarios] = telefono;
     usuariosCorreos[cantidadUsuarios] = correo;
     usuariosContrasenas[cantidadUsuarios] = contrasena;
     usuarioActual = cantidadUsuarios;
     cantidadUsuarios++;
     GuardarUsuarios();

     Console.WriteLine("\n========================================");
     Console.WriteLine("     CUENTA CREADA CORRECTAMENTE");
     Console.WriteLine("========================================");
     Console.WriteLine("Bienvenido(a), " + nombre + ".");
     Console.WriteLine("Sus datos han sido guardados.");
     Console.WriteLine("\nPresione una tecla para ingresar al sistema...");
     Console.ReadKey();

     MenuUsuarioRegistrado();
     usuarioActual = -1;
 }
