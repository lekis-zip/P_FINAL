// ==================== CONSULTAR ====================

// ==========================================================
// TEMA: BÚSQUEDA
// Recorre los códigos registrados hasta encontrar coincidencia.
// ==========================================================
static public void BuscarExpediente()
{
    Console.Clear();
    Console.Write("Ingrese el código del expediente: ");
    string codigo = Console.ReadLine().ToUpper();
    int posicion = -1;

    for (int i = 0; i < cantidad; i++)
    {
        if (codigos[i] == codigo) posicion = i;
    }

    if (posicion == -1) Console.WriteLine("ERROR: Expediente no encontrado.");
    else
    {
        Console.WriteLine("\n========================================");
        Console.WriteLine("       DATOS DEL DENUNCIANTE");
        Console.WriteLine("========================================");
        Console.WriteLine();
        Console.WriteLine("Nombre          : " + nombres[posicion]);
        Console.WriteLine("DNI             : " + dnis[posicion]);
        Console.WriteLine("Teléfono        : " + telefonos[posicion]);
        Console.WriteLine("Correo          : " + correos[posicion]);

        Console.WriteLine("\n----------------------------------------");
        Console.WriteLine("          DATOS DEL EXPEDIENTE");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Tipo documento  : " + tiposDocumento[posicion]);
        Console.WriteLine("Especialidad    : " + especialidades[posicion]);
        Console.WriteLine("Juez asignado   : " + jueces[posicion]);
        Console.WriteLine("Documento       : " + Path.GetFileName(documentos[posicion]));
        Console.WriteLine("Número de folios: " + foliosDocumentos[posicion]);
        Console.WriteLine("Descripción     : " + descripcionDocumento[posicion]);
        Console.WriteLine("Código          : " + codigos[posicion]);
        Console.WriteLine("Estado          : REGISTRADO");

        Console.WriteLine("\n----------------------------------------");
        Console.WriteLine("          MEDIOS PROBATORIOS");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Cantidad        : " + cantidadMedios[posicion]);

        for (int i = 0; i < cantidadMedios[posicion]; i++)
        {
            Console.WriteLine();
            Console.WriteLine("Medio " + (i + 1) + "         : " + tiposMedios[posicion, i]);
            Console.WriteLine("Archivo         : " + Path.GetFileName(archivosMedios[posicion, i]));
            Console.WriteLine("Descripción     : " + descripcionMedios[posicion, i]);
        }

        Console.WriteLine("\n========================================");
        Console.WriteLine("El expediente se encuentra registrado");
        Console.WriteLine("correctamente en el sistema.");
        Console.WriteLine("========================================");

        Console.WriteLine("\nEl expediente ha sido derivado al juez asignado");
        Console.WriteLine("para su evaluación y trámite correspondiente.");
    }
    Console.ReadKey();
}

// ==========================================================
// TEMA: ESTRUCTURA REPETITIVA - FOR
// Recorre los arreglos y muestra los expedientes registrados.
// ==========================================================
static public void MostrarExpedientes()
{
    Console.Clear();
    Console.WriteLine("EXPEDIENTES REGISTRADOS\n");

    if (cantidad == 0) Console.WriteLine("No existen expedientes registrados.");

    for (int i = 0; i < cantidad; i++)
    {
        Console.WriteLine(codigos[i] + " | " + tiposDocumento[i] + " | " + especialidades[i] + " | " + nombres[i]);
    }
    Console.ReadKey();
}

// ==========================================================
// TEMA: ORDENAMIENTO - MÉTODO BURBUJA
// Compara expedientes consecutivos y los intercambia hasta
// dejarlos ordenados de forma ascendente por código.
// ==========================================================

static public void Cambiar(string[] arreglo, int posicion)
{
    string auxiliar = arreglo[posicion];
    arreglo[posicion] = arreglo[posicion + 1];
    arreglo[posicion + 1] = auxiliar;
}

static public void CambiarMatriz(string[,] matriz, int fila, int columna)
{
    string auxiliar = matriz[fila, columna];
    matriz[fila, columna] = matriz[fila + 1, columna];
    matriz[fila + 1, columna] = auxiliar;
}

static public void OrdenarExpedientes()
{
    // ESTRUCTURA REPETITIVA: for dentro de otro for (Método Burbuja)
    for (int i = 0; i < cantidad - 1; i++)
    {
        for (int j = 0; j < cantidad - 1; j++)
        {
            if (string.Compare(codigos[j], codigos[j + 1]) > 0)
            {
                Cambiar(codigos, j);
                Cambiar(nombres, j);
                Cambiar(dnis, j);
                Cambiar(telefonos, j);
                Cambiar(correos, j);
                Cambiar(tiposDocumento, j);
                Cambiar(especialidades, j);
                Cambiar(jueces, j);
                Cambiar(documentos, j);
                Cambiar(descripcionDocumento, j);

                int auxiliar = cantidadMedios[j];
                cantidadMedios[j] = cantidadMedios[j + 1];
                cantidadMedios[j + 1] = auxiliar;

                for (int k = 0; k < 3; k++)
                {
                    CambiarMatriz(tiposMedios, j, k);
                    CambiarMatriz(archivosMedios, j, k);
                    CambiarMatriz(descripcionMedios, j, k);
                }
            }
        }
    }

    GuardarExpedientes();

    Console.Clear();
    Console.WriteLine("========================================");
    Console.WriteLine("       EXPEDIENTES ORDENADOS");
    Console.WriteLine("========================================");

    for (int i = 0; i < cantidad; i++)
    {
        Console.WriteLine(
            codigos[i] + " | " +
            tiposDocumento[i] + " | " +
            especialidades[i] + " | " +
            nombres[i]);
    }

    Console.WriteLine("========================================");
    Console.WriteLine("Expedientes ordenados correctamente");
    Console.WriteLine("de forma ascendente.");

    Console.ReadKey();
}
