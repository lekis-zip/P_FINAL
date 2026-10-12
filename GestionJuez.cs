 static public void MenuJuez(string juez, string especialidad)
 {
     string opcion = "";

     while (opcion != "6")
     {
         Console.Clear();
         Console.WriteLine("========================================");
         Console.WriteLine("          PANEL DEL JUEZ");
         Console.WriteLine("========================================");
         Console.WriteLine("Juez         : " + juez);
         Console.WriteLine("Especialidad : " + especialidad);
         Console.WriteLine("----------------------------------------");
         Console.WriteLine("Asignados    : " + ContarExpedientesJuez(juez));
         Console.WriteLine("Registrados  : " + ContarEstadoJuez(juez, "REGISTRADO"));
         Console.WriteLine("En revisión  : " + ContarEstadoJuez(juez, "EN REVISIÓN"));
         Console.WriteLine("Atendidos    : " + ContarEstadoJuez(juez, "ATENDIDO"));
         Console.WriteLine("========================================");
         Console.WriteLine("1. Expedientes asignados");
         Console.WriteLine("2. Consultar expediente");
         Console.WriteLine("3. Ordenar expedientes por fecha");
         Console.WriteLine("4. Estadísticas");
         Console.WriteLine("5. Mi cuenta");
         Console.WriteLine("6. Cerrar sesión");
         Console.Write("\nOpción: ");
         opcion = Console.ReadLine();

         if (opcion == "1") MostrarExpedientesJuez(juez);
         else if (opcion == "2") ConsultarExpedienteJuez(juez);
         else if (opcion == "3") OrdenarExpedientesJuez(juez);
         else if (opcion == "4") MostrarEstadisticasJuez(juez, especialidad);
         else if (opcion == "5") MostrarCuentaJuez(juez, especialidad);
         else if (opcion != "6")
         {
             Console.WriteLine("ERROR: Seleccione una opción válida.");
             Console.ReadKey();
         }
     }
 }

 static public void MostrarExpedientesJuez(string juez)
 {
     Console.Clear();
     Console.WriteLine("========================================");
     Console.WriteLine("        EXPEDIENTES ASIGNADOS");
     Console.WriteLine("========================================");
     bool encontrado = false;

     for (int i = 0; i < cantidad; i++)
     {
         if (jueces[i] == juez)
         {
             encontrado = true;
             Console.WriteLine(codigos[i] + " | " + tiposDocumento[i] + " | " + fechasRegistro[i] + " " + MostrarHora12(horasRegistro[i]) + " | " + estados[i]);
         }
     }

     if (!encontrado) Console.WriteLine("No tiene expedientes asignados.");
     Console.WriteLine("========================================");
     Console.ReadKey();
 }

 static public void ConsultarExpedienteJuez(string juez)
 {
     Console.Clear();
     Console.WriteLine("========================================");
     Console.WriteLine("         CONSULTAR EXPEDIENTE");
     Console.WriteLine("========================================");
     Console.Write("Código del expediente: ");
     string codigo = Console.ReadLine().ToUpper();
     int posicion = -1;

     for (int i = 0; i < cantidad; i++)
         if (codigos[i] == codigo && jueces[i] == juez) posicion = i;

     if (posicion == -1)
     {
         Console.WriteLine("ERROR: El expediente no existe o no está asignado a este juez.");
         Console.ReadKey();
         return;
     }

     string opcion = "";
     while (opcion != "4")
     {
         Console.Clear();
         MostrarDetalleExpedienteJuez(posicion);
         Console.WriteLine("\n----------------------------------------");
         Console.WriteLine("1. Abrir documento principal");
         Console.WriteLine("2. Abrir medio probatorio");
         Console.WriteLine("3. Actualizar estado");
         Console.WriteLine("4. Volver");
         Console.Write("Opción: ");
         opcion = Console.ReadLine();

         if (opcion == "1") AbrirArchivo(documentos[posicion]);
         else if (opcion == "2") AbrirMedioProbatorio(posicion);
         else if (opcion == "3") ActualizarEstadoExpediente(posicion);
         else if (opcion != "4")
         {
             Console.WriteLine("ERROR: Seleccione una opción válida.");
             Console.ReadKey();
         }
     }
 }

 static public void MostrarDetalleExpedienteJuez(int posicion)
 {
     Console.WriteLine("========================================");
     Console.WriteLine("       EXPEDIENTE " + codigos[posicion]);
     Console.WriteLine("========================================");
     Console.WriteLine("DATOS DEL USUARIO");
     Console.WriteLine("Nombre       : " + nombres[posicion]);
     Console.WriteLine("DNI          : " + dnis[posicion]);
     Console.WriteLine("Teléfono     : " + telefonos[posicion]);
     Console.WriteLine("Correo       : " + correos[posicion]);
     Console.WriteLine("----------------------------------------");
     Console.WriteLine("DATOS DEL EXPEDIENTE");
     Console.WriteLine("Documento    : " + tiposDocumento[posicion]);
     Console.WriteLine("Especialidad : " + especialidades[posicion]);
     Console.WriteLine("Fecha        : " + fechasRegistro[posicion]);
     Console.WriteLine("Hora         : " + MostrarHora12(horasRegistro[posicion]));
     Console.WriteLine("Estado       : " + estados[posicion]);
     Console.WriteLine("Descripción  : " + descripcionDocumento[posicion]);
     Console.WriteLine("PDF          : " + Path.GetFileName(documentos[posicion]));
     Console.WriteLine("Número de folios: " + foliosDocumentos[posicion]);
     Console.WriteLine("----------------------------------------");
     Console.WriteLine("MEDIOS PROBATORIOS: " + cantidadMedios[posicion]);
     for (int i = 0; i < cantidadMedios[posicion]; i++)
         Console.WriteLine((i + 1) + ". " + tiposMedios[posicion, i] + " | " + Path.GetFileName(archivosMedios[posicion, i]) + " | " + descripcionMedios[posicion, i]);
 }

 static public void AbrirArchivo(string ruta)
 {
     if (ruta == null || ruta == "" || !File.Exists(ruta))
     {
         Console.WriteLine("ERROR: El archivo no se encuentra disponible en esta computadora.");
         Console.ReadKey();
         return;
     }

     try
     {
         System.Diagnostics.Process.Start(ruta);
         Console.WriteLine("Archivo abierto correctamente.");
     }
     catch
     {
         Console.WriteLine("ERROR: No se pudo abrir el archivo.");
     }
     Console.ReadKey();
 }

 static public void AbrirMedioProbatorio(int posicion)
 {
     if (cantidadMedios[posicion] == 0)
     {
         Console.WriteLine("Este expediente no tiene medios probatorios registrados.");
         Console.ReadKey();
         return;
     }

     Console.WriteLine("\nSeleccione el medio probatorio:");
     for (int i = 0; i < cantidadMedios[posicion]; i++)
         Console.WriteLine((i + 1) + ". " + tiposMedios[posicion, i] + " - " + Path.GetFileName(archivosMedios[posicion, i]));

     Console.Write("Opción: ");
     int numero;
     if (!int.TryParse(Console.ReadLine(), out numero) || numero < 1 || numero > cantidadMedios[posicion])
     {
         Console.WriteLine("ERROR: Seleccione un medio válido.");
         Console.ReadKey();
         return;
     }

     AbrirArchivo(archivosMedios[posicion, numero - 1]);
 }

 static public void ActualizarEstadoExpediente(int posicion)
 {
     Console.Clear();
     Console.WriteLine("========================================");
     Console.WriteLine("        ACTUALIZAR ESTADO");
     Console.WriteLine("========================================");
     Console.WriteLine("Expediente: " + codigos[posicion]);
     Console.WriteLine("Estado actual: " + estados[posicion]);
     Console.WriteLine();
     Console.WriteLine("1. REGISTRADO");
     Console.WriteLine("2. EN REVISIÓN");
     Console.WriteLine("3. ATENDIDO");
     Console.WriteLine("4. Cancelar");
     Console.Write("Opción: ");
     string opcion = Console.ReadLine();

     if (opcion == "1") estados[posicion] = "REGISTRADO";
     else if (opcion == "2") estados[posicion] = "EN REVISIÓN";
     else if (opcion == "3") estados[posicion] = "ATENDIDO";
     else if (opcion == "4") return;
     else
     {
         Console.WriteLine("ERROR: Seleccione una opción válida.");
         Console.ReadKey();
         return;
     }

     GuardarExpedientes();

     string rutaPDF = "";
     if (estados[posicion] == "EN REVISIÓN") rutaPDF = GenerarPDFRevision(posicion);
     if (estados[posicion] == "ATENDIDO") rutaPDF = GenerarPDFAtendido(posicion);

     Console.WriteLine("\nEstado actualizado correctamente.");
     if (rutaPDF != "")
     {
         Console.WriteLine("PDF de actualización generado: " + rutaPDF);
         EnviarCorreoExpediente(posicion, rutaPDF, estados[posicion]);
     }
     Console.ReadKey();
 }
