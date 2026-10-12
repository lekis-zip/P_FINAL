        // ==================== ACCESO USUARIO ====================

        static public void MenuUsuario()
        {
            string opcion = "";

            while (opcion != "5")
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("       MESA DE PARTES DIGITAL");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Registrar expediente");
                Console.WriteLine("2. Mostrar estado de expediente");
                Console.WriteLine("3. Mostrar expedientes");
                Console.WriteLine("4. Ordenar expedientes");
                Console.WriteLine("5. Volver");
                Console.Write("Opción: ");
                opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1": RegistrarExpediente(); break;
                    case "2": BuscarExpediente(); break;
                    case "3": MostrarExpedientes(); break;
                    case "4": OrdenarExpedientes(); break;
                    case "5": break;
                    default: Console.WriteLine("ERROR: Seleccione una opción válida."); Console.ReadKey(); break;
                }
            }
        }

        // ==================== ACCESO JUEZ ====================

        // Cuentas institucionales de prueba del prototipo.
        // En la interfaz final estas credenciales no se mostrarán en pantalla.
        static string[] juecesCorreos = { "carlos.mendoza@pj.gob.pe", "patricia.salazar@pj.gob.pe" };
        static string[] juecesContrasenas = { "Carlos2026*", "Patricia2026*" };
        static string[] juecesNombres = { "Dr. Carlos Mendoza Rojas", "Dra. Patricia Salazar Torres" };
        static string[] juecesEspecialidades = { "PENAL", "FAMILIA" };

        static public void AccesoJuez()
        {
            string opcion = "";

            while (opcion != "2")
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("             ACCESO DEL JUEZ");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Iniciar sesión");
                Console.WriteLine("2. Volver");
                Console.Write("\nOpción: ");
                opcion = Console.ReadLine();

                if (opcion == "1") IniciarSesionJuez();
                else if (opcion != "2")
                {
                    Console.WriteLine("ERROR: Seleccione una opción válida.");
                    Console.ReadKey();
                }
            }
        }

        static public void IniciarSesionJuez()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("        INICIAR SESIÓN - JUEZ");
            Console.WriteLine("========================================");
            Console.Write("Correo institucional: ");
            string correo = Console.ReadLine().Trim().ToLower();
            Console.Write("Contraseña: ");
            string contrasena = Console.ReadLine();

            int posicionJuez = -1;
            for (int i = 0; i < juecesCorreos.Length; i++)
            {
                if (juecesCorreos[i].ToLower() == correo && juecesContrasenas[i] == contrasena)
                    posicionJuez = i;
            }

            if (posicionJuez == -1)
            {
                Console.WriteLine("\nERROR: Correo institucional o contraseña incorrectos.");
                Console.ReadKey();
                return;
            }

            MenuJuez(juecesNombres[posicionJuez], juecesEspecialidades[posicionJuez]);
        }

        static public int ContarExpedientesJuez(string juez)
        {
            int total = 0;
            for (int i = 0; i < cantidad; i++)
                if (jueces[i] == juez) total++;
            return total;
        }

        static public int ContarEstadoJuez(string juez, string estado)
        {
            int total = 0;
            for (int i = 0; i < cantidad; i++)
                if (jueces[i] == juez && estados[i] == estado) total++;
            return total;
        }
