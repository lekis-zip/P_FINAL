static public void MostrarMisExpedientes()
{
    Console.Clear();
    Console.WriteLine("========================================");
    Console.WriteLine("           MIS EXPEDIENTES");
    Console.WriteLine("========================================");

    bool encontrado = false;
    for (int i = 0; i < cantidad; i++)
    {
        if (usuarioActual >= 0 && propietariosDni[i] == usuariosDnis[usuarioActual])
        {
            encontrado = true;
            Console.WriteLine(codigos[i] + " | " + tiposDocumento[i] + " | " + especialidades[i] + " | " + fechasRegistro[i] + " " + MostrarHora12(horasRegistro[i]) + " | " + estados[i]);
        }
    }

    if (!encontrado) Console.WriteLine("Aún no tiene expedientes registrados.");
    Console.WriteLine("========================================");
    Console.ReadKey();
}

static public void RevisarEstadoUsuario()
{
    Console.Clear();
    Console.WriteLine("========================================");
    Console.WriteLine("          REVISAR ESTADO");
    Console.WriteLine("========================================");
    Console.Write("Código del expediente: ");
    string codigo = Console.ReadLine().ToUpper();
    int posicion = -1;

    for (int i = 0; i < cantidad; i++)
    {
        if (usuarioActual >= 0 && codigos[i] == codigo && propietariosDni[i] == usuariosDnis[usuarioActual]) posicion = i;
    }

    if (posicion == -1) Console.WriteLine("ERROR: El expediente no existe o no pertenece a esta cuenta.");
    else
    {
        Console.WriteLine("\nCódigo       : " + codigos[posicion]);
        Console.WriteLine("Documento    : " + tiposDocumento[posicion]);
        Console.WriteLine("Especialidad : " + especialidades[posicion]);
        Console.WriteLine("Fecha        : " + fechasRegistro[posicion]);
        Console.WriteLine("Hora         : " + MostrarHora12(horasRegistro[posicion]));
        Console.WriteLine("Estado       : " + estados[posicion]);
        Console.WriteLine("Juez asignado: " + jueces[posicion]);
    }
    Console.ReadKey();
}
