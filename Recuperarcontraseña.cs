        static public bool RecuperarContrasenaUsuario()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("       RECUPERAR CONTRASEÑA");
            Console.WriteLine("========================================");

            Console.Write("\nIngrese su correo registrado: ");
            string correo = Console.ReadLine().Trim();

            int posicion = -1;

            for (int i = 0; i < cantidadUsuarios; i++)
            {
                if (usuariosCorreos[i].ToLower() == correo.ToLower())
                {
                    posicion = i;
                    break;
                }
            }

            if (posicion == -1)
            {
                Console.WriteLine("No se encontró una cuenta con ese correo.");
                Console.ReadKey();
                return false;
            }

            Random aleatorio = new Random();
            string codigo = aleatorio.Next(100000, 1000000).ToString();

            if (!EnviarCodigoRecuperacion(correo, codigo))
            {
                Console.WriteLine("No se pudo enviar el código de recuperación.");
                Console.ReadKey();
                return false;
            }

            DateTime vencimiento = DateTime.Now.AddMinutes(10);

            Console.WriteLine("\nSe envió un código de 6 dígitos a su correo.");
            Console.WriteLine("El código vence en 10 minutos.");

            for (int intento = 1; intento <= 3; intento++)
            {
                Console.Write("\nIngrese el código recibido: ");
                string ingresado = Console.ReadLine();

                if (DateTime.Now > vencimiento)
                {
                    Console.WriteLine("El código ha vencido.");
                    Console.ReadKey();
                    return false;
                }

                if (ingresado == codigo)
                {
                    Console.WriteLine("\nCódigo verificado correctamente.");

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

                    usuariosContrasenas[posicion] = nueva;
                    intentosFallidos[posicion] = 0;
                    usuariosBloqueados[posicion] = false;
                    GuardarUsuarios();

                    Console.WriteLine("\nContraseña recuperada correctamente.");
                    Console.WriteLine("Ya puede iniciar sesión.");
                    Console.ReadKey();
                    return true;
                }

                Console.WriteLine("Código incorrecto. Intento " + intento + " de 3.");
            }

            Console.WriteLine("\nHa superado los intentos de verificación.");
            Console.ReadKey();
            return false;
        }
