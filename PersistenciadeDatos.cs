 // ==================== ARCHIVOS ====================

 // ==========================================================
 // TEMA: ARCHIVOS - PERSISTENCIA DE DATOS
 // Guarda los expedientes en expedientes.txt para conservarlos
 // aunque el programa se cierre.
 // ==========================================================
 static public void GuardarExpedientes()
 {
     StreamWriter archivo = new StreamWriter(archivoDatos, false);

     for (int i = 0; i < cantidad; i++)
     {
         archivo.WriteLine(codigos[i] + "|" + nombres[i] + "|" + dnis[i] + "|" + telefonos[i] + "|" + correos[i] + "|" + tiposDocumento[i] + "|" + especialidades[i] + "|" + jueces[i] + "|" + documentos[i] + "|" + descripcionDocumento[i] + "|" + cantidadMedios[i] + "|" + propietariosDni[i] + "|" + fechasRegistro[i] + "|" + horasRegistro[i] + "|" + estados[i] + "|" + foliosDocumentos[i]);

         for (int j = 0; j < cantidadMedios[i]; j++)
         {
             archivo.WriteLine("MEDIO|" + tiposMedios[i, j] + "|" + archivosMedios[i, j] + "|" + descripcionMedios[i, j]);
         }
     }
     archivo.Close();
 }

 // Lee expedientes.txt al iniciar y recupera los registros guardados.
 static public void CargarExpedientes()
 {
     if (!File.Exists(archivoDatos)) return;

     string[] lineas = File.ReadAllLines(archivoDatos);

     for (int i = 0; i < lineas.Length; i++)
     {
         string[] datos = lineas[i].Split('|');

         if (datos[0] != "MEDIO" && (datos.Length == 11 || datos.Length == 15 || datos.Length == 16))
         {
             codigos[cantidad] = datos[0];
             nombres[cantidad] = datos[1];
             dnis[cantidad] = datos[2];
             telefonos[cantidad] = datos[3];
             correos[cantidad] = datos[4];
             tiposDocumento[cantidad] = datos[5];
             especialidades[cantidad] = datos[6];
             jueces[cantidad] = datos[7];
             documentos[cantidad] = datos[8];
             descripcionDocumento[cantidad] = datos[9];
             cantidadMedios[cantidad] = int.Parse(datos[10]);

             if (datos.Length >= 15)
             {
                 propietariosDni[cantidad] = datos[11];
                 fechasRegistro[cantidad] = datos[12];
                 horasRegistro[cantidad] = datos[13];
                 estados[cantidad] = datos[14];
             }
             else
             {
                 // Expediente de una versión anterior: todavía no tenía propietario.
                 // No se asigna automáticamente a ningún usuario actual.
                 propietariosDni[cantidad] = "";
                 fechasRegistro[cantidad] = "SIN FECHA";
                 horasRegistro[cantidad] = "--:--";
                 estados[cantidad] = "REGISTRADO";
             }
             if (datos.Length == 16)
             {
                 foliosDocumentos[cantidad] = int.Parse(datos[15]);
             }
             else
             {
                 foliosDocumentos[cantidad] = ContarFoliosPDF(documentos[cantidad]);
             }
             for (int j = 0; j < cantidadMedios[cantidad]; j++)
             {
                 string[] medio = lineas[i + j + 1].Split('|');
                 tiposMedios[cantidad, j] = medio[1];
                 archivosMedios[cantidad, j] = medio[2];
                 descripcionMedios[cantidad, j] = medio[3];
             }

             i += cantidadMedios[cantidad];
             cantidad++;
         }
     }
 }
