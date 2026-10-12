        // ==========================================================
        // PDF 1 - CONSTANCIA DE REGISTRO
        // Se genera cuando el expediente queda REGISTRADO.
        // ==========================================================
        static public string GenerarPDFRegistro(int posicion)
        {
            return CrearPDF(posicion, "REGISTRADO", "01_REGISTRADO_VINO_A4.png", "CONSTANCIA_REGISTRO_");
        }

        // ==========================================================
        // PDF 2 - EXPEDIENTE EN REVISIÓN
        // Se genera cuando el juez cambia el estado a EN REVISIÓN.
        // ==========================================================
        static public string GenerarPDFRevision(int posicion)
        {
            return CrearPDF(posicion, "EN REVISIÓN", "02_EN_REVISION_AZUL_A4.png", "ACTUALIZACION_REVISION_");
        }

        // ==========================================================
        // PDF 3 - EXPEDIENTE ATENDIDO
        // Se genera cuando el juez cambia el estado a ATENDIDO.
        // ==========================================================
        static public string GenerarPDFAtendido(int posicion)
        {
            return CrearPDF(posicion, "ATENDIDO", "03_ATENDIDO_VERDE_A4.png", "ACTUALIZACION_ATENDIDO_");
        }

        // FUNCIÓN REUTILIZABLE PARA LOS TRES PDF
        // La lógica es la misma; solo cambia la plantilla y el estado.
        static public string CrearPDF(int posicion, string estadoActual, string plantilla, string nombreArchivo)
        {
            try
            {
                PdfDocument pdf = new PdfDocument();
                PdfPage pagina = pdf.AddPage();
                pagina.Size = PdfSharp.PageSize.A4;
                XGraphics grafico = XGraphics.FromPdfPage(pagina);

                // 1. PLANTILLA DEL DOCUMENTO
                ColocarPlantilla(grafico, plantilla);

                // 2. CÓDIGO Y FECHA DEL DOCUMENTO
                string fechaDocumento = fechasRegistro[posicion] + "   " + MostrarHora12(horasRegistro[posicion]);
                if (estadoActual != "REGISTRADO")
                    fechaDocumento = DateTime.Now.ToString("dd/MM/yyyy") + "   " + DateTime.Now.ToString("hh:mm tt").Replace("AM", "a. m.").Replace("PM", "p. m.");

                if (estadoActual == "EN REVISIÓN")
                {
                    XFont fuenteCodigo = new XFont("Arial", 10.0, XFontStyle.Bold);
                    XFont fuenteFecha = new XFont("Arial", 8.2, XFontStyle.Regular);

                    grafico.DrawString(codigos[posicion], fuenteCodigo,
                        XBrushes.Black, new XRect(430, 183, 138, 16),
                        XStringFormats.Center);

                    grafico.DrawString(fechaDocumento, fuenteFecha,
                        XBrushes.Black, new XRect(430, 232, 138, 16),
                        XStringFormats.Center);
                }
                else if (estadoActual == "ATENDIDO")
                {
                    XFont fuenteCodigoVerde = new XFont("Arial", 10.0, XFontStyle.Bold);
                    XFont fuenteFechaVerde = new XFont("Arial", 8.2, XFontStyle.Regular);

                    grafico.DrawString(codigos[posicion], fuenteCodigoVerde,
                        XBrushes.Black, new XRect(430, 207, 138, 16),
                        XStringFormats.Center);

                    grafico.DrawString(fechaDocumento, fuenteFechaVerde,
                        XBrushes.Black, new XRect(430, 258, 138, 16),
                        XStringFormats.Center);
                }
                else
                {
                    XFont fuenteCodigoRojo = new XFont("Arial", 10.0, XFontStyle.Bold);
                    XFont fuenteFechaRoja = new XFont("Arial", 8.2, XFontStyle.Regular);

                    grafico.DrawString(codigos[posicion], fuenteCodigoRojo,
                        XBrushes.Black,
                        new XRect(430, 194, 138, 16),
                        XStringFormats.Center);

                    grafico.DrawString(fechaDocumento, fuenteFechaRoja,
                        XBrushes.Black,
                        new XRect(430, 239, 138, 16),
                        XStringFormats.Center);
                }
                // 3. DATOS DEL USUARIO
                EscribirDatosUsuario(grafico, posicion, estadoActual);

                // 4. DATOS DEL EXPEDIENTE
                EscribirDatosExpediente(grafico, posicion, estadoActual);

                // 5. MEDIOS PROBATORIOS
                EscribirMediosProbatorios(grafico, posicion, estadoActual);
                if (estadoActual == "EN REVISIÓN")
                {
                    // Cubrir el aviso original y sus restos difuminados
                    grafico.DrawRectangle(XBrushes.White, 20, 705, 555, 78);

                    // Nuevo aviso alineado con las tablas
                    XBrush fondoAviso = new XSolidBrush(XColor.FromArgb(232, 244, 255));
                    XPen bordeAviso = new XPen(XColor.FromArgb(180, 210, 235), 0.6);

                    grafico.DrawRectangle(fondoAviso, 68, 711, 500, 58);
                    grafico.DrawRectangle(bordeAviso, 68, 711, 500, 58);

                    XFont fuenteAviso = new XFont("Arial", 11.0, XFontStyle.Regular);

                    grafico.DrawString(
                        "Su documentación presentada está siendo revisada por el órgano",
                         fuenteAviso, XBrushes.Black,
                         new XRect(83, 717, 470, 16), XStringFormats.CenterLeft);

                    grafico.DrawString(
                        "jurisdiccional correspondiente. En caso de requerirse información",
                        fuenteAviso, XBrushes.Black,
                        new XRect(83, 734, 470, 16), XStringFormats.CenterLeft);

                    grafico.DrawString(
                        "adicional, se le comunicará oportunamente a través de este medio.",
                        fuenteAviso, XBrushes.Black,
                        new XRect(83, 751, 470, 16), XStringFormats.CenterLeft);
                    // Ícono de documento con verificación
                    XPen lapizIcono = new XPen(XColor.FromArgb(40, 110, 190), 2);

                    // Contorno del documento
                    grafico.DrawRectangle(lapizIcono, 505, 724, 22, 28);

                    // Líneas interiores
                    grafico.DrawLine(lapizIcono, 510, 733, 521, 733);
                    grafico.DrawLine(lapizIcono, 510, 739, 521, 739);

                    // Símbolo de verificación
                    grafico.DrawLine(lapizIcono, 509, 745, 514, 750);
                    grafico.DrawLine(lapizIcono, 514, 750, 525, 738);
                }
                if (estadoActual == "ATENDIDO")
                {
                    // Cubrir el aviso original y los restos del pie
                    grafico.DrawRectangle(XBrushes.White, 20, 705, 555, 78);

                    // Recuadro verde alineado con las tablas
                    XBrush fondoAvisoVerde = new XSolidBrush(XColor.FromArgb(235, 248, 238));
                    XPen bordeAvisoVerde = new XPen(XColor.FromArgb(155, 205, 165), 0.6);

                    grafico.DrawRectangle(fondoAvisoVerde, 68, 711, 500, 58);
                    grafico.DrawRectangle(bordeAvisoVerde, 68, 711, 500, 58);

                    // Texto del aviso
                    XFont fuenteVerde = new XFont("Arial", 11.0, XFontStyle.Regular);

                    grafico.DrawString(
                        "La atención correspondiente a su expediente ha sido registrada",
                        fuenteVerde, XBrushes.Black,
                        new XRect(83, 717, 400, 16), XStringFormats.CenterLeft);

                    grafico.DrawString(
                        "en el sistema. Puede consultar la información actualizada",
                        fuenteVerde, XBrushes.Black,
                        new XRect(83, 734, 400, 16), XStringFormats.CenterLeft);

                    grafico.DrawString(
                        "ingresando a la Mesa de Partes Digital.",
                        fuenteVerde, XBrushes.Black,
                        new XRect(83, 751, 400, 16), XStringFormats.CenterLeft);

                    // Ícono verde de documento verificado
                    XPen lapizVerde = new XPen(XColor.FromArgb(35, 130, 65), 2);

                    grafico.DrawRectangle(lapizVerde, 505, 724, 22, 28);
                    grafico.DrawLine(lapizVerde, 510, 733, 521, 733);
                    grafico.DrawLine(lapizVerde, 510, 739, 521, 739);
                    grafico.DrawLine(lapizVerde, 509, 745, 514, 750);
                    grafico.DrawLine(lapizVerde, 514, 750, 525, 738);
                }
                if (estadoActual == "REGISTRADO")
                {
                 
                    grafico.DrawRectangle(XBrushes.White, 20, 753, 555, 27);
                }

                // 6. PIE DE PÁGINA
                // Se limpia únicamente la línea variable del pie para evitar textos superpuestos.
                // Los textos fijos "Código de constancia", "Generado automáticamente..." y
                // "Página 1 de 1" permanecen en la plantilla.
                string codigoComunicacion = "";
                if (estadoActual == "REGISTRADO") codigoComunicacion = "CR-" + codigos[posicion] + "-" + DateTime.Now.Year;
                else if (estadoActual == "EN REVISIÓN") codigoComunicacion = "AR-" + codigos[posicion] + "-" + DateTime.Now.Year;
                else codigoComunicacion = "AA-" + codigos[posicion] + "-" + DateTime.Now.Year;


                if (estadoActual == "EN REVISIÓN" || estadoActual == "ATENDIDO")
                {
                    grafico.DrawRectangle(XBrushes.White, 20, 780, 555, 55);
                    EscribirDato(grafico, codigoComunicacion, 30, 795, 250, 10.0, true);
                }
                else
                {
                    
                    grafico.DrawRectangle(XBrushes.White, 20, 780, 555, 55);
                    EscribirDato(grafico, codigoComunicacion, 30, 795, 250, 10.0, true);
                }
                DateTime fechaPie = estadoActual == "REGISTRADO" ? ObtenerFechaHora(fechasRegistro[posicion], horasRegistro[posicion]) : DateTime.Now;
                if (fechaPie == DateTime.MaxValue) fechaPie = DateTime.Now;
                string[] meses = { "enero", "febrero", "marzo", "abril", "mayo", "junio", "julio", "agosto", "septiembre", "octubre", "noviembre", "diciembre" };
                string fechaLima = "Lima, " + fechaPie.Day + " de " + meses[fechaPie.Month - 1] + " de " + fechaPie.Year;

                if (estadoActual == "EN REVISIÓN" || estadoActual == "ATENDIDO")
                {
                    EscribirDato(grafico, fechaLima, 400, 795, 175, 9.5, false);
                }
                else
                {
                    EscribirDato(grafico, fechaLima, 400, 795, 175, 9.5, false);
                }

                // 7. GUARDAR PDF
                string ruta = Path.Combine(CarpetaPDF(), nombreArchivo + codigos[posicion] + ".pdf");
                pdf.Save(ruta);
                pdf.Close();
                return ruta;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR al generar el PDF: " + ex.Message);
                return "";
            }
        }
