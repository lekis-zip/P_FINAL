static public void RegistrarExpediente()
{
    if (usuarioActual < 0)
    {
        Console.WriteLine("ERROR: Debe iniciar sesión para registrar un expediente.");
        Console.ReadKey();
        return;
    }

    Console.Clear();
    Console.WriteLine("========================================");
    Console.WriteLine("        REGISTRO DE EXPEDIENTE");
    Console.WriteLine("========================================");
    Console.WriteLine("Usuario: " + usuariosNombres[usuarioActual]);
    Console.WriteLine("DNI    : " + usuariosDnis[usuarioActual]);

    nombres[cantidad] = usuariosNombres[usuarioActual];
    dnis[cantidad] = usuariosDnis[usuarioActual];
    telefonos[cantidad] = usuariosTelefonos[usuarioActual];
    correos[cantidad] = usuariosCorreos[usuarioActual];
    propietariosDni[cantidad] = usuariosDnis[usuarioActual];

    Console.WriteLine("\n========================================");
    Console.WriteLine("          TIPO DE DOCUMENTO");
    Console.WriteLine("========================================");
    Console.WriteLine("¿Qué tipo de documento desea presentar?");
    Console.WriteLine("1. Demanda");
    Console.WriteLine("2. Escrito");
    Console.WriteLine("3. Solicitud");
    Console.Write("Opción: ");
    string opcionDocumento = Console.ReadLine();

    while (!ValidarTipoDocumento(opcionDocumento))
    {
        Console.Write("Opción: ");
        opcionDocumento = Console.ReadLine();
    }

    if (opcionDocumento == "1") tiposDocumento[cantidad] = "DEMANDA";
    if (opcionDocumento == "2") tiposDocumento[cantidad] = "ESCRITO";
    if (opcionDocumento == "3") tiposDocumento[cantidad] = "SOLICITUD";

    Console.WriteLine("\n========================================");
    Console.WriteLine("             ESPECIALIDAD");
    Console.WriteLine("========================================");
    Console.WriteLine("1. Penal");
    Console.WriteLine("2. Familia");
    Console.Write("Opción: ");
    string opcionEspecialidad = Console.ReadLine();

    while (!ValidarEspecialidad(opcionEspecialidad))
    {
        Console.Write("Opción: ");
        opcionEspecialidad = Console.ReadLine();
    }

    if (opcionEspecialidad == "1") especialidades[cantidad] = "PENAL";
    if (opcionEspecialidad == "2") especialidades[cantidad] = "FAMILIA";

    documentos[cantidad] = SeleccionarDocumento(especialidades[cantidad], dnis[cantidad]);
    foliosDocumentos[cantidad] = ContarFoliosPDF(documentos[cantidad]);

    Console.WriteLine("\nA continuación, ingrese una breve descripción del contenido del documento.");
    Console.WriteLine("IMPORTANTE: Indique brevemente de qué trata el documento presentado.");
    Console.Write("Descripción: ");
    descripcionDocumento[cantidad] = Console.ReadLine();

    while (!ValidarDescripcion(descripcionDocumento[cantidad]))
    {
        Console.Write("Descripción: ");
        descripcionDocumento[cantidad] = Console.ReadLine();
    }

    RegistrarMedios(cantidad, especialidades[cantidad]);
    codigos[cantidad] = "EXP-" + (cantidad + 1).ToString("D4");

    if (especialidades[cantidad] == "PENAL")
    {
        jueces[cantidad] = "Dr. Carlos Mendoza Rojas";
    }
    else
    {
        jueces[cantidad] = "Dra. Patricia Salazar Torres";
    }

    fechasRegistro[cantidad] = DateTime.Now.ToString("dd/MM/yyyy");
    horasRegistro[cantidad] = DateTime.Now.ToString("HH:mm");
    estados[cantidad] = "REGISTRADO";

    Console.WriteLine("\n========================================");
    Console.WriteLine("       CONFIRMACIÓN DEL REGISTRO");
    Console.WriteLine("========================================");

    Console.WriteLine("Código expediente  : " + codigos[cantidad]);
    Console.WriteLine("Nombre             : " + nombres[cantidad]);
    Console.WriteLine("DNI                : " + dnis[cantidad]);
    Console.WriteLine("Teléfono           : " + telefonos[cantidad]);
    Console.WriteLine("Correo             : " + correos[cantidad]);
    Console.WriteLine("Fecha de registro  : " + fechasRegistro[cantidad]);
    Console.WriteLine("Hora               : " + MostrarHora12(horasRegistro[cantidad]));
    Console.WriteLine("Estado             : " + estados[cantidad]);

    Console.WriteLine();
    Console.WriteLine("Tipo de documento  : " + tiposDocumento[cantidad]);
    Console.WriteLine("Especialidad       : " + especialidades[cantidad]);
    Console.WriteLine("Documento          : " + Path.GetFileName(documentos[cantidad]));
    Console.WriteLine("Medios probatorios : " + cantidadMedios[cantidad]);

    Console.WriteLine();
    Console.WriteLine("Juez asignado      : " + jueces[cantidad]);
    Console.WriteLine("Su caso ha sido asignado al juez mencionado.");

    Console.WriteLine("========================================");

    Console.WriteLine("\n1. Confirmar registro");
    Console.WriteLine("2. Cancelar");
    Console.Write("Opción: ");
    string confirmar = Console.ReadLine();

    while (confirmar != "1" && confirmar != "2")
    {
        Console.WriteLine("ERROR: Seleccione 1 para confirmar o 2 para cancelar.");
        Console.Write("Opción: ");
        confirmar = Console.ReadLine();
    }

    if (confirmar == "2")
    {
        Console.WriteLine("\nRegistro cancelado.");
        Console.ReadKey();
        return;
    }

    codigos[cantidad] = "EXP-" + (cantidad + 1).ToString("D4");

    if (especialidades[cantidad] == "PENAL") jueces[cantidad] = "Dr. Carlos Mendoza Rojas";
    else jueces[cantidad] = "Dra. Patricia Salazar Torres";

    cantidad++;
    GuardarExpedientes();

    string rutaConstancia = GenerarPDFRegistro(cantidad - 1);

    Console.WriteLine("\nEl expediente se encuentra registrado correctamente en el sistema.");
    if (rutaConstancia != "")
    {
        Console.WriteLine("Constancia PDF generada: " + rutaConstancia);
        EnviarCorreoExpediente(cantidad - 1, rutaConstancia, "REGISTRADO");
    }
    Console.WriteLine("Presione una tecla para continuar.");
    Console.ReadKey();
}
