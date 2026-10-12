// ==========================================================
    // TEMA: VALIDACIÓN DE ARCHIVOS - DOCUMENTO PRINCIPAL
    // Comprueba formato PDF, nombre solicitado y tamaño máximo.
    // ==========================================================

    static public bool ValidarDocumento(string ruta, string nombreEsperado)
    {
        string extension = Path.GetExtension(ruta).ToLower();
        string nombre = Path.GetFileName(ruta);
        FileInfo archivo = new FileInfo(ruta);

        if (extension != ".pdf")
        {
            Console.WriteLine("ERROR: El documento principal solo puede estar en formato PDF.");
            return false;
        }
        if (nombre.ToLower() != nombreEsperado.ToLower())
        {
            Console.WriteLine("ERROR: El nombre del documento no es correcto.");
            Console.WriteLine("Nombre requerido: " + nombreEsperado);
            return false;
        }
        if (archivo.Length > 30 * 1024 * 1024)
        {
            Console.WriteLine("ERROR: El documento supera el tamaño máximo permitido de 30 MB.");
            return false;
        }
        
        return true;
    }
    static public int ContarFoliosPDF(string ruta)
    {
        try
        {
            using (PdfDocument documento = PdfReader.Open(ruta, PdfDocumentOpenMode.Import))
            {
                return documento.PageCount;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR al contar los folios del PDF: " + ex.Message);
            return 0;
        }
    } 
    static public string SeleccionarDocumento(string especialidad, string dni)
    {
        string nombreEsperado = especialidad + "-" + dni + ".pdf";
        string ruta = "";

        while (ruta == "")
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("         DOCUMENTO PRINCIPAL");
            Console.WriteLine("========================================");
            Console.WriteLine("Formato requerido : PDF");
            Console.WriteLine("Tamaño máximo     : 30 MB");
            Console.WriteLine("Nombre requerido  : " + nombreEsperado);
            Console.WriteLine("\nIMPORTANTE: El documento debe cumplir con el formato,");
            Console.WriteLine("nombre y tamaño indicados para ser aceptado.");
            Console.WriteLine("\nPresione ENTER para seleccionar el archivo...");
            Console.ReadLine();

            OpenFileDialog ventana = new OpenFileDialog();
            ventana.Title = "Seleccionar documento principal";
            ventana.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            ventana.Filter = "Todos los archivos (*.*)|*.*";

            if (ventana.ShowDialog() == DialogResult.OK)
            {
                if (ValidarDocumento(ventana.FileName, nombreEsperado))
                {
                    int folios = ContarFoliosPDF(ventana.FileName);

                    if (folios < 1 || folios > 100)
                    {
                        Console.WriteLine("\nERROR: El documento debe tener entre 1 y 100 folios.");
                        Console.WriteLine("Número de folios detectados: " + folios);
                        Console.WriteLine("Documento rechazado.");
                    }
                    else
                    {
                        ruta = ventana.FileName;
                        Console.WriteLine("\nDocumento validado correctamente.");
                        Console.WriteLine("Número de folios: " + folios);
                    }
                }
            }
            else Console.WriteLine("AVISO: Debe seleccionar un documento para continuar.");
        }
        return ruta;
    }
