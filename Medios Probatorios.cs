        // ==========================================================
        // TEMA: ARCHIVOS + ARREGLO BIDIMENSIONAL - MEDIOS PROBATORIOS
        // Valida tipo, extensión y tamaño antes de guardar cada medio.
        // ==========================================================

        static public bool ValidarMedio(string ruta, string tipo, int maximo)
        {
            string extension = Path.GetExtension(ruta).ToLower();
            bool formato = false;

            if (tipo == "IMAGEN" && (extension == ".jpg" || extension == ".jpeg" || extension == ".png")) formato = true;
            if (tipo == "AUDIO" && extension == ".mp3") formato = true;
            if (tipo == "VIDEO" && extension == ".mp4") formato = true;

            if (!formato)
            {
                Console.WriteLine("ERROR: El formato del archivo no está permitido.");
                return false;
            }

            FileInfo archivo = new FileInfo(ruta);
            if (archivo.Length > maximo * 1024 * 1024)
            {
                Console.WriteLine("ERROR: El archivo supera el tamaño máximo de " + maximo + " MB.");
                return false;
            }

            Console.WriteLine(tipo + " validado correctamente.");
            return true;
        }

        static public string SeleccionarMedio(string tipo, int maximo)
        {
            string ruta = "";

            while (ruta == "")
            {
                Console.WriteLine("\n========================================");
                Console.WriteLine("          ADJUNTAR " + tipo);
                Console.WriteLine("========================================");

                if (tipo == "IMAGEN") Console.WriteLine("Formatos permitidos : JPG, JPEG, PNG");
                if (tipo == "AUDIO") Console.WriteLine("Formato permitido   : MP3");
                if (tipo == "VIDEO") Console.WriteLine("Formato permitido   : MP4");

                Console.WriteLine("Tamaño máximo       : " + maximo + " MB");
                Console.WriteLine("\nIMPORTANTE: El archivo debe cumplir con el formato");
                Console.WriteLine("y tamaño indicados para ser aceptado.");
                Console.WriteLine("\nPresione ENTER para seleccionar el archivo...");
                Console.ReadLine();

                OpenFileDialog ventana = new OpenFileDialog();
                ventana.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                ventana.Filter = "Todos los archivos (*.*)|*.*";

                if (ventana.ShowDialog() == DialogResult.OK)
                {
                    if (ValidarMedio(ventana.FileName, tipo, maximo)) ruta = ventana.FileName;
                }
                else Console.WriteLine("AVISO: Debe seleccionar un archivo para continuar.");
            }
            return ruta;
        }

        static public void MostrarEjemplos(string especialidad, string tipo)
        {
            Console.WriteLine("\nIMPORTANTE: Describa brevemente qué contiene el archivo");
            Console.WriteLine("y cuál es su relación con el caso.");
            Console.WriteLine("Ejemplos:");

            if (especialidad == "FAMILIA")
            {
                if (tipo == "IMAGEN")
                {
                    Console.WriteLine("- Fotografía del domicilio familiar relacionado con los hechos.");
                    Console.WriteLine("- Fotografía de los bienes materia de controversia familiar.");
                }
                if (tipo == "AUDIO")
                {
                    Console.WriteLine("- Audio de una conversación relacionada con el caso.");
                    Console.WriteLine("- Audio de una comunicación entre las partes involucradas.");
                }
                if (tipo == "VIDEO")
                {
                    Console.WriteLine("- Video del lugar relacionado con los hechos.");
                    Console.WriteLine("- Video de una situación vinculada con el caso familiar.");
                }
            }
            else
            {
                if (tipo == "IMAGEN")
                {
                    Console.WriteLine("- Fotografía del lugar relacionado con los hechos.");
                    Console.WriteLine("- Fotografía de un objeto relacionado con el caso.");
                }
                if (tipo == "AUDIO")
                {
                    Console.WriteLine("- Audio de una conversación relacionada con los hechos.");
                    Console.WriteLine("- Audio de una comunicación vinculada con el caso.");
                }
                if (tipo == "VIDEO")
                {
                    Console.WriteLine("- Video del lugar relacionado con los hechos.");
                    Console.WriteLine("- Video de una situación vinculada con el caso.");
                }
            }
        }

        static public void RegistrarMedios(int posicion, string especialidad)
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("          MEDIOS PROBATORIOS");
            Console.WriteLine("========================================");
            Console.WriteLine("¿Desea adjuntar un medio probatorio?");
            Console.WriteLine("1. Sí");
            Console.WriteLine("2. No");
            Console.Write("Opción: ");
            string respuesta = Console.ReadLine();

            while (respuesta != "1" && respuesta != "2")
            {
                Console.WriteLine("ERROR: Seleccione 1 para Sí o 2 para No.");
                Console.Write("Opción: ");
                respuesta = Console.ReadLine();
            }

            for (int i = 0; i < 3 && respuesta == "1"; i++)
            {
                Console.WriteLine("\nSeleccione el tipo de medio probatorio:");
                Console.WriteLine("1. Imagen");
                Console.WriteLine("2. Audio");
                Console.WriteLine("3. Video");
                Console.Write("Opción: ");
                string opcion = Console.ReadLine();

                while (opcion != "1" && opcion != "2" && opcion != "3")
                {
                    Console.WriteLine("ERROR: Seleccione una opción válida del 1 al 3.");
                    Console.Write("Opción: ");
                    opcion = Console.ReadLine();
                }

                string tipo = "";
                string ruta = "";

                if (opcion == "1") { tipo = "IMAGEN"; ruta = SeleccionarMedio(tipo, 5); }
                if (opcion == "2") { tipo = "AUDIO"; ruta = SeleccionarMedio(tipo, 10); }
                if (opcion == "3") { tipo = "VIDEO"; ruta = SeleccionarMedio(tipo, 15); }

                Console.WriteLine("\nA continuación, ingrese una breve descripción del medio probatorio.");
                MostrarEjemplos(especialidad, tipo);
                Console.Write("\nDescripción: ");
                string descripcion = Console.ReadLine();

                while (!ValidarDescripcion(descripcion))
                {
                    Console.Write("Descripción: ");
                    descripcion = Console.ReadLine();
                }

                tiposMedios[posicion, i] = tipo;
                archivosMedios[posicion, i] = ruta;
                descripcionMedios[posicion, i] = descripcion;
                cantidadMedios[posicion]++;

                Console.WriteLine("Medio probatorio agregado correctamente.");

                if (i == 2) Console.WriteLine("AVISO: Ha alcanzado el máximo de 3 medios probatorios.");
                else
                {
                    Console.WriteLine("\n¿Desea agregar otro medio probatorio?");
                    Console.WriteLine("1. Sí");
                    Console.WriteLine("2. No");
                    Console.Write("Opción: ");
                    respuesta = Console.ReadLine();

                    while (respuesta != "1" && respuesta != "2")
                    {
                        Console.WriteLine("ERROR: Seleccione 1 para Sí o 2 para No.");
                        Console.Write("Opción: ");
                        respuesta = Console.ReadLine();
                    }
                }
            }
        }
