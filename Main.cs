        static public DateTime ObtenerFechaHora(string fecha, string hora)
        {
            DateTime resultado;
            if (DateTime.TryParseExact(fecha + " " + hora, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out resultado))
                return resultado;
            return DateTime.MaxValue;
        }

        static public void OrdenarExpedientesJuez(string juez)
        {
            int total = ContarExpedientesJuez(juez);
            if (total == 0)
            {
                Console.WriteLine("No tiene expedientes asignados para ordenar.");
                Console.ReadKey();
                return;
            }

            string[] ordenCodigos = new string[total];
            string[] ordenTipos = new string[total];
            string[] ordenFechas = new string[total];
            string[] ordenHoras = new string[total];
            string[] ordenEstados = new string[total];
            int posicion = 0;

            for (int i = 0; i < cantidad; i++)
            {
                if (jueces[i] == juez)
                {
                    ordenCodigos[posicion] = codigos[i];
                    ordenTipos[posicion] = tiposDocumento[i];
                    ordenFechas[posicion] = fechasRegistro[i];
                    ordenHoras[posicion] = horasRegistro[i];
                    ordenEstados[posicion] = estados[i];
                    posicion++;
                }
            }

            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("      ORDENAR EXPEDIENTES POR FECHA");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Ascendente  (más antiguos → más recientes)");
            Console.WriteLine("2. Descendente (más recientes → más antiguos)");
            Console.Write("Opción: ");
            string opcion = Console.ReadLine();

            if (opcion != "1" && opcion != "2")
            {
                Console.WriteLine("ERROR: Seleccione una opción válida.");
                Console.ReadKey();
                return;
            }

            // Ordenamiento burbuja sobre los expedientes asignados al juez.
            for (int i = 0; i < total - 1; i++)
            {
                for (int j = 0; j < total - 1 - i; j++)
                {
                    DateTime fecha1 = ObtenerFechaHora(ordenFechas[j], ordenHoras[j]);
                    DateTime fecha2 = ObtenerFechaHora(ordenFechas[j + 1], ordenHoras[j + 1]);
                    bool cambiar = false;

                    if (opcion == "1" && fecha1 > fecha2) cambiar = true;
                    if (opcion == "2" && fecha1 < fecha2) cambiar = true;

                    if (cambiar)
                    {
                        Cambiar(ordenCodigos, j);
                        Cambiar(ordenTipos, j);
                        Cambiar(ordenFechas, j);
                        Cambiar(ordenHoras, j);
                        Cambiar(ordenEstados, j);
                    }
                }
            }

            Console.WriteLine("\n========================================");
            Console.WriteLine("          RESULTADO DEL ORDENAMIENTO");
            Console.WriteLine("========================================");
            for (int i = 0; i < total; i++)
                Console.WriteLine(ordenCodigos[i] + " | " + ordenTipos[i] + " | " + ordenFechas[i] + " " + MostrarHora12(ordenHoras[i]) + " | " + ordenEstados[i]);
            Console.ReadKey();
        }

        // ==========================================================
        // TEMA: CONTADORES Y ESTADÍSTICAS
        // Recorre los expedientes del juez, cuenta tipos de documento y
        // estados, y determina cuál tipo de documento se presenta más.
        // ==========================================================
        static public void MostrarEstadisticasJuez(string juez, string especialidad)
        {
            int demandas = 0;
            int escritos = 0;
            int solicitudes = 0;
            int registrados = 0;
            int revision = 0;
            int atendidos = 0;

            for (int i = 0; i < cantidad; i++)
            {
                if (jueces[i] == juez)
                {
                    if (tiposDocumento[i] == "DEMANDA") demandas++;
                    if (tiposDocumento[i] == "ESCRITO") escritos++;
                    if (tiposDocumento[i] == "SOLICITUD") solicitudes++;
                    if (estados[i] == "REGISTRADO") registrados++;
                    if (estados[i] == "EN REVISIÓN") revision++;
                    if (estados[i] == "ATENDIDO") atendidos++;
                }
            }

            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("       ESTADÍSTICAS - " + especialidad);
            Console.WriteLine("========================================");
            Console.WriteLine("Total de expedientes : " + (demandas + escritos + solicitudes));
            Console.WriteLine("\nTIPO DE DOCUMENTO");
            Console.WriteLine("Demandas     : " + demandas);
            Console.WriteLine("Escritos     : " + escritos);
            Console.WriteLine("Solicitudes  : " + solicitudes);
            Console.WriteLine("\nESTADOS");
            Console.WriteLine("Registrados  : " + registrados);
            Console.WriteLine("En revisión  : " + revision);
            Console.WriteLine("Atendidos    : " + atendidos);
            Console.WriteLine("\nDOCUMENTO MÁS FRECUENTE");

            if (demandas == 0 && escritos == 0 && solicitudes == 0)
                Console.WriteLine("Aún no existen expedientes para calcularlo.");
            else if (demandas > escritos && demandas > solicitudes)
                Console.WriteLine("DEMANDA - " + demandas + " expediente(s)");
            else if (escritos > demandas && escritos > solicitudes)
                Console.WriteLine("ESCRITO - " + escritos + " expediente(s)");
            else if (solicitudes > demandas && solicitudes > escritos)
                Console.WriteLine("SOLICITUD - " + solicitudes + " expediente(s)");
            else
            {
                Console.Write("EMPATE: ");
                int mayor = demandas;
                if (escritos > mayor) mayor = escritos;
                if (solicitudes > mayor) mayor = solicitudes;
                if (demandas == mayor) Console.Write("DEMANDA ");
                if (escritos == mayor) Console.Write("ESCRITO ");
                if (solicitudes == mayor) Console.Write("SOLICITUD ");
                Console.WriteLine("- " + mayor + " expediente(s) cada uno");
            }
            Console.WriteLine("========================================");
            Console.ReadKey();
        }

        static public void MostrarCuentaJuez(string juez, string especialidad)
        {
            int posicion = -1;
            for (int i = 0; i < juecesNombres.Length; i++)
                if (juecesNombres[i] == juez) posicion = i;

            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("             MI CUENTA");
            Console.WriteLine("========================================");
            Console.WriteLine("Nombre        : " + juez);
            Console.WriteLine("Especialidad  : " + especialidad);
            if (posicion >= 0) Console.WriteLine("Correo        : " + juecesCorreos[posicion]);
            Console.WriteLine("========================================");
            Console.ReadKey();
        }

        // ==================== MAIN ====================

        [STAThread]
        static void Main(string[] args)
        {
            CargarExpedientes();
            CargarUsuarios();
            string opcion = "";

            while (opcion != "3")
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("       MESA DE PARTES DIGITAL");
                Console.WriteLine("========================================");
                Console.WriteLine("Seleccione el tipo de acceso:");
                Console.WriteLine();
                Console.WriteLine("1. Usuario");
                Console.WriteLine("2. Juez");
                Console.WriteLine("3. Salir");
                Console.Write("\nOpción: ");
                opcion = Console.ReadLine();

                if (opcion == "1")
                    AccesoUsuario();
                else if (opcion == "2")
                    AccesoJuez();
                else if (opcion == "3")
                    Console.WriteLine("Saliendo del sistema...");
                else
                {
                    Console.WriteLine("ERROR: Seleccione una opción válida.");
                    Console.ReadKey();
                }
            }
        }
    }
}
