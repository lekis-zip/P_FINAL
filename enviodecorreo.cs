// ==========================================================
// FUNCIONALIDAD ADICIONAL: ENVÍO DE CORREO
// Envía al correo del usuario el PDF correspondiente al estado.
// La contraseña de aplicación se pega solo en el espacio indicado.
// ==========================================================

static public bool EnviarCodigoRecuperacion(string destino, string codigo)
{
    try
    {
        using (MailMessage mensaje = new MailMessage())
        {
            mensaje.From = new MailAddress(
                correoSistema,
                "Mesa de Partes Digital"
            );

            mensaje.To.Add(destino);
            mensaje.Subject = "Código de recuperación - Mesa de Partes Digital";

            mensaje.Body =
                "Estimado(a) usuario(a):\n\n" +
                "Ha solicitado recuperar su contraseña.\n\n" +
                "Su código de verificación es: " + codigo + "\n\n" +
                "Este código tiene una validez de 10 minutos.\n\n" +
                "Si usted no solicitó este cambio, ignore este mensaje.\n\n" +
                "Atentamente,\nMesa de Partes Digital";

            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(
                    correoSistema,
                    claveAplicacion
                );

                smtp.Send(mensaje);
            }
        }

        Console.WriteLine("Código enviado correctamente.");
        return true;
    }
    catch (Exception ex)
    {
        Console.WriteLine("ERROR al enviar el código: " + ex.Message);
        return false;
    }
}
static public bool EnviarCorreoExpediente(int posicion, string rutaPDF, string estado)
{
    
    string correoSistema = "mesadepartes.csharp@gmail.com";
    string claveAplicacion = "sqmgxgltnxsuenlm";

   
    try
    {
        string asunto = "";
        string mensaje = "";

        if (estado == "REGISTRADO")
        {
            asunto = "Registro exitoso del expediente " + codigos[posicion];
            mensaje = "Estimada(o) " + nombres[posicion] + ":\n\n" +
                      "Se confirma el registro exitoso de su expediente en la Mesa de Partes Digital.\n" +
                      "Adjuntamos la constancia de registro en formato PDF, en la cual encontrará el detalle de la información registrada.\n\n" +
                      "Código de expediente: " + codigos[posicion] + "\n" +
                      "Estado actual: REGISTRADO\n\n" +
                      "Atentamente,\nMesa de Partes Digital";
        }
        else if (estado == "EN REVISIÓN")
        {
            asunto = "Actualización del expediente " + codigos[posicion] + " - En revisión";
            mensaje = "Estimada(o) " + nombres[posicion] + ":\n\n" +
                      "Se le informa que su expediente " + codigos[posicion] + " se encuentra actualmente EN REVISIÓN.\n" +
                      "Adjuntamos el documento de actualización, en el cual encontrará el detalle de la información de su expediente.\n\n" +
                      "Atentamente,\nMesa de Partes Digital";
        }
        else
        {
            asunto = "Actualización del expediente " + codigos[posicion] + " - Atendido";
            mensaje = "Estimada(o) " + nombres[posicion] + ":\n\n" +
                      "Se le informa que su expediente " + codigos[posicion] + " ha sido ATENDIDO.\n" +
                      "Adjuntamos el documento de actualización, en el cual encontrará el detalle de la información de su expediente.\n\n" +
                      "Atentamente,\nMesa de Partes Digital";
        }

        using (MailMessage correo = new MailMessage())
        {
            correo.From = new MailAddress(correoSistema, "Mesa de Partes Digital");
            correo.To.Add(correos[posicion]);
            correo.Subject = asunto;
            correo.Body = mensaje;
            correo.Attachments.Add(new Attachment(rutaPDF));

            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(correoSistema, claveAplicacion);
                smtp.Send(correo);
            }
        }

        Console.WriteLine("Correo enviado correctamente a: " + correos[posicion]);
        return true;
    }
    catch (Exception ex)
    {
        Console.WriteLine("AVISO: El PDF se generó correctamente, pero no se pudo enviar el correo.");
        Console.WriteLine("Detalle: " + ex.Message);
        return false;
    }
}

// ==================== GENERACIÓN DE PDF ====================

static public string CarpetaPDF()
{
    string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PDF_Generados");
    if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
    return carpeta;
}

// ==========================================================
// FUNCIONALIDAD ADICIONAL: GENERACIÓN DE PDF
// Los tres documentos usan una imagen A4 como plantilla.
// El programa solo escribe encima los datos que cambian.
// Así se evita dibujar todo el diseño con código.
// ==========================================================

static public string RutaPlantilla(string archivo)
{
    string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plantillas", archivo);
    if (File.Exists(ruta)) return ruta;

    ruta = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Referencias_Diseno", archivo));
    return ruta;
}

// Escribe un dato sobre la plantilla con una sola tipografía.
static public void EscribirDato(XGraphics grafico, string texto, double x, double y, double ancho, double tamano, bool negrita)
{
    if (texto == null) texto = "";
    XFontStyle estilo = negrita ? XFontStyle.Bold : XFontStyle.Regular;
    XFont fuente = new XFont("Arial", tamano, estilo);
    grafico.DrawString(texto, fuente, XBrushes.Black, new XRect(x, y, ancho, 16), XStringFormats.CenterLeft);
}

// Coloca la imagen A4 como fondo. No dibuja cuadros ni encabezados adicionales.
static public void ColocarPlantilla(XGraphics grafico, string archivo)
{
    string ruta = RutaPlantilla(archivo);
    if (!File.Exists(ruta)) throw new Exception("No se encontró la plantilla: " + ruta);

    using (XImage fondo = XImage.FromFile(ruta))
    {
        grafico.DrawImage(fondo, 0, 0, 595, 842);
    }
}

// DATOS DEL USUARIO
static public void EscribirDatosUsuario(XGraphics grafico, int posicion, string estadoActual)
{
    double x = 232;
    double y = 294;
    double salto = 25.5;

    if (estadoActual == "EN REVISIÓN")
    {
        x = 232;
        y = 310;
        salto = 22.5;
    }
    else if (estadoActual == "ATENDIDO")
    {
        x = 242;
        y = 329;
        salto = 22.5;
    }
    EscribirDato(grafico, nombres[posicion], x,
    estadoActual == "REGISTRADO" ? y + 5 : y,
    310, 10.0, false);
    EscribirDato(grafico, dnis[posicion], x, y + salto, 310, 10.0, false);
    EscribirDato(grafico, telefonos[posicion], x, y + salto * 2, 310, 10.0, false);
    EscribirDato(grafico, correos[posicion], x, y + salto * 3, 310, 9.4, false);
}

// DATOS DEL EXPEDIENTE
static public void EscribirDatosExpediente(XGraphics grafico, int posicion, string estadoActual)
{
    double x = 232;
    double y = 426;
    double salto = 20.4;
    if (estadoActual == "ATENDIDO")
    {
        x = 242;
        y = 453;
        salto = 20.4;
    }
    if (estadoActual == "EN REVISIÓN")
    {
        x = 232;
        y = 433;
        salto = 20.4;
    }
    EscribirDato(grafico, tiposDocumento[posicion], x, y, 310, 9.8, false);
    EscribirDato(grafico, especialidades[posicion], x, y + salto, 310, 9.8, false);
    EscribirDato(grafico, Path.GetFileName(documentos[posicion]), x, y + salto * 2, 310, 9.2, false);
    EscribirDato(grafico, descripcionDocumento[posicion], x, y + salto * 3, 310, 9.2, false);
    EscribirDato(grafico, fechasRegistro[posicion], x, y + salto * 4, 310, 9.6, false);
    EscribirDato(grafico, MostrarHora12(horasRegistro[posicion]), x, y + salto * 5, 310, 9.6, false);
    if (estadoActual == "EN REVISIÓN")
    {


        if (estadoActual == "EN REVISIÓN")
        {
            EscribirDato(grafico, estadoActual, x + 15, y + salto * 6 + 5, 130, 9.3, true);
            EscribirDato(grafico, jueces[posicion], x, y + salto * 7 + 5, 310, 9.3, false);
        }

    }
    else if (estadoActual == "ATENDIDO")
    {
        XFont fuenteEstadoVerde = new XFont("Arial", 9.3, XFontStyle.Bold);

        grafico.DrawString(estadoActual, fuenteEstadoVerde,
            XBrushes.Black,
            new XRect(252, y + salto * 6, 107, 16),
            XStringFormats.Center);

        EscribirDato(grafico, jueces[posicion],
            x, y + salto * 7 + 7, 310, 9.3, false);
    }
    else if (estadoActual == "REGISTRADO")
    {
        XFont fuenteEstadoRojo = new XFont("Arial", 9.3, XFontStyle.Bold);

        grafico.DrawString(estadoActual, fuenteEstadoRojo,
            XBrushes.Black,
            new XRect(245, y + salto * 6 + 5, 126, 16),
            XStringFormats.Center);

        EscribirDato(grafico, jueces[posicion],
            x, y + salto * 7 + 5, 310, 9.3, false);
    }
    else
    {
        EscribirDato(grafico, estadoActual, x, y + salto * 6, 130, 9.3, true);
        EscribirDato(grafico, jueces[posicion], x, y + salto * 7, 310, 9.3, false);
    }
}

// MEDIOS PROBATORIOS
static public void EscribirMediosProbatorios(XGraphics grafico, int posicion, string estadoActual)
{
    double yInicial = 646;
    double salto = 20.8;
    if (estadoActual == "REGISTRADO")
    {
        yInicial = 660;
    }
    if (estadoActual == "EN REVISIÓN")
    {
        yInicial = 661;
    }
    else if (estadoActual == "ATENDIDO")
    {
        yInicial = 686;
    }
    if (cantidadMedios[posicion] == 0)
    {
        EscribirDato(grafico, "Sin medios probatorios adicionales", 110, yInicial, 420, 8.6, false);
        return;
    }

    for (int i = 0; i < cantidadMedios[posicion] && i < 3; i++)
    {
        double y = yInicial + salto * i;
        string descripcion = descripcionMedios[posicion, i] ?? "";
        if (descripcion.Length > 42) descripcion = descripcion.Substring(0, 42) + "...";

        if (estadoActual == "EN REVISIÓN")
        {
            XFont fuente = new XFont("Arial", 8.2, XFontStyle.Regular);

            grafico.DrawString((i + 1).ToString(), fuente, XBrushes.Black,
                new XRect(47, y, 61, 16), XStringFormats.Center);

            grafico.DrawString(tiposMedios[posicion, i], fuente, XBrushes.Black,
                new XRect(108, y, 104, 16), XStringFormats.Center);

            grafico.DrawString(Path.GetFileName(archivosMedios[posicion, i]), fuente, XBrushes.Black,
                new XRect(212, y, 177, 16), XStringFormats.Center);

            grafico.DrawString(descripcion, fuente, XBrushes.Black,
                new XRect(389, y, 185, 16), XStringFormats.Center);
        }
        else if (estadoActual == "ATENDIDO")
        {
            XFont fuenteVerdeTabla = new XFont("Arial", 8.2, XFontStyle.Regular);

            grafico.DrawString((i + 1).ToString(), fuenteVerdeTabla,
                XBrushes.Black,
                new XRect(70, y, 29, 16),
                XStringFormats.Center);

            grafico.DrawString(tiposMedios[posicion, i], fuenteVerdeTabla,
                XBrushes.Black,
                new XRect(99, y, 119, 16),
                XStringFormats.Center);

            grafico.DrawString(Path.GetFileName(archivosMedios[posicion, i]), fuenteVerdeTabla,
                XBrushes.Black,
                new XRect(218, y, 172, 16),
                XStringFormats.Center);

            grafico.DrawString(descripcion, fuenteVerdeTabla,
                XBrushes.Black,
                new XRect(390, y, 189, 16),
                XStringFormats.Center);
        }
        else if (estadoActual == "REGISTRADO")
        {
            XFont fuenteRojaTabla = new XFont("Arial", 8.2, XFontStyle.Regular);

            grafico.DrawString((i + 1).ToString(), fuenteRojaTabla,
                XBrushes.Black,
                new XRect(78, y, 29, 16),
                XStringFormats.Center);

            grafico.DrawString(tiposMedios[posicion, i], fuenteRojaTabla,
                XBrushes.Black,
                new XRect(107, y, 115, 16),
                XStringFormats.Center);

            grafico.DrawString(Path.GetFileName(archivosMedios[posicion, i]), fuenteRojaTabla,
                XBrushes.Black,
                new XRect(208, y, 182, 16),
                XStringFormats.Center);

            grafico.DrawString(descripcion, fuenteRojaTabla,
                XBrushes.Black,
                new XRect(390, y, 216, 16),
                XStringFormats.Center);
        }
        else
        {
            EscribirDato(grafico, (i + 1).ToString(), 82, y, 24, 8.8, false);
            EscribirDato(grafico, tiposMedios[posicion, i], 112, y, 82, 8.6, false);
            EscribirDato(grafico, Path.GetFileName(archivosMedios[posicion, i]), 202, y, 165, 8.2, false);
            EscribirDato(grafico, descripcion, 376, y, 174, 8.2, false);
        }
    }
}
