 static public void AccesoUsuario()


 {
     string opcion = "";

     while (opcion != "3")
     {
         Console.Clear();
         Console.WriteLine("========================================");
         Console.WriteLine("          ACCESO DE USUARIO");
         Console.WriteLine("========================================");
         Console.WriteLine("1. Iniciar sesión");
         Console.WriteLine("2. Registrarme");
         Console.WriteLine("3. Volver");
         Console.Write("\nOpción: ");
         opcion = Console.ReadLine();

         if (opcion == "1")
         {
             if (IniciarSesionUsuario())
             {
                 MenuUsuarioRegistrado();
                 usuarioActual = -1;
             }
         }
         else if (opcion == "2") RegistrarUsuario();
         else if (opcion != "3")
         {
             Console.WriteLine("ERROR: Seleccione una opción válida.");
             Console.ReadKey();
         }
     }
 }

 static public void MenuUsuarioRegistrado()
 {
     string opcion = "";

     while (opcion != "5")
     {
         Console.Clear();
         Console.WriteLine("========================================");
         Console.WriteLine("          PANEL DEL USUARIO");
         Console.WriteLine("========================================");
         if (usuarioActual >= 0) Console.WriteLine("Usuario: " + usuariosNombres[usuarioActual]);
         Console.WriteLine();
         Console.WriteLine("1. Registrar expediente");
         Console.WriteLine("2. Mis expedientes");
         Console.WriteLine("3. Revisar estado");
         Console.WriteLine("4. Mi cuenta");
         Console.WriteLine("5. Cerrar sesión");
         Console.Write("\nOpción: ");
         opcion = Console.ReadLine();

         if (opcion == "1") RegistrarExpediente();
         else if (opcion == "2") MostrarMisExpedientes();
         else if (opcion == "3") RevisarEstadoUsuario();
         else if (opcion == "4") MostrarCuentaUsuario();
         else if (opcion != "5")
         {
             Console.WriteLine("ERROR: Seleccione una opción válida.");
             Console.ReadKey();
         }
     }
 }
