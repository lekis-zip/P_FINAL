using System;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf.IO;
namespace Avances
{
    internal class Program
    {
        static string correoSistema = "mesadepartes.csharp@gmail.com";
        static string claveAplicacion = "sqmgxgltnxsuenlm";
        // ==========================================================
        // TEMA: ARREGLOS UNIDIMENSIONALES
        // Se reserva espacio para almacenar hasta 100 expedientes.
        // Cada posición de los arreglos corresponde al mismo expediente.
        // Solo se utilizan las posiciones que realmente se registran.
        // ==========================================================
        static string[] codigos = new string[100];
        static int[] foliosDocumentos = new int[100];
        static string[] nombres = new string[100];
        static string[] dnis = new string[100];
        static string[] telefonos = new string[100];
        static string[] correos = new string[100];
        static string[] tiposDocumento = new string[100];
        static string[] especialidades = new string[100];
        static string[] jueces = new string[100];
        static string[] documentos = new string[100];
        static string[] descripcionDocumento = new string[100];
        static string[] propietariosDni = new string[100];
        static string[] fechasRegistro = new string[100];
        static string[] horasRegistro = new string[100];
        static string[] estados = new string[100];

        // ==========================================================
        // TEMA: ARREGLOS BIDIMENSIONALES
        // Cada fila representa un expediente y cada columna uno de sus
        // medios probatorios. Se permiten hasta 3 medios por expediente.
        // ==========================================================
        static string[,] tiposMedios = new string[100, 3];
        static string[,] archivosMedios = new string[100, 3];
        static string[,] descripcionMedios = new string[100, 3];
        static int[] cantidadMedios = new int[100];

        static int cantidad = 0;
        static string archivoDatos = "expedientes.txt";

        // ==================== CUENTAS DE USUARIO ====================
        static string[] usuariosNombres = new string[100];
        static string[] usuariosDnis = new string[100];
        static string[] usuariosTelefonos = new string[100];
        static string[] usuariosCorreos = new string[100];
        static string[] usuariosContrasenas = new string[100];
        static int[] intentosFallidos = new int[100];
        static bool[] usuariosBloqueados = new bool[100];
        static int cantidadUsuarios = 0;
        static int usuarioActual = -1;
        static string archivoUsuarios = "usuarios.txt";

        // ==========================================================
        // TEMA: FUNCIONES + CONDICIONALES + STRINGS - VALIDACIONES
        // Estas funciones comprueban los datos antes de guardarlos.
        // Devuelven true cuando el dato es correcto y false si hay error.
        // ==========================================================

        static public bool ValidarNombre(string nombre)
        {
            if (nombre == "")
            {
                Console.WriteLine("ERROR: El nombre no puede quedar vacío.");
                return false;
            }
            for (int i = 0; i < nombre.Length; i++)
            {
                if (!char.IsLetter(nombre[i]) && nombre[i] != ' ')
                {
                    Console.WriteLine("ERROR: El nombre solo permite letras y espacios.");
                    return false;
                }
            }
            return true;
        }

        static public bool ValidarDNI(string dni)
        {
            if (dni.Length != 8)
            {
                Console.WriteLine("ERROR: El DNI debe contener exactamente 8 dígitos.");
                return false;
            }
            for (int i = 0; i < dni.Length; i++)
            {
                if (!char.IsDigit(dni[i]))
                {
                    Console.WriteLine("ERROR: El DNI solo permite dígitos numéricos.");
                    return false;
                }
            }
            return true;
        }

        static public bool ValidarTelefono(string telefono)
        {
            if (telefono.Length != 9)
            {
                Console.WriteLine("ERROR: El teléfono debe contener exactamente 9 dígitos.");
                return false;
            }
            if (telefono[0] != '9')
            {
                Console.WriteLine("ERROR: El teléfono debe comenzar con el número 9.");
                return false;
            }
            for (int i = 0; i < telefono.Length; i++)
            {
                if (!char.IsDigit(telefono[i]))
                {
                    Console.WriteLine("ERROR: El teléfono solo permite dígitos numéricos.");
                    return false;
                }
            }
            return true;
        }

        static public bool ValidarCorreo(string correo)
        {
            correo = correo.Trim();
            if (correo == "")
            {
                Console.WriteLine("ERROR: El correo no puede quedar vacío.");
                return false;
            }
            if (!correo.Contains("@") || !correo.Contains(".") || correo.Contains(" "))
            {
                Console.WriteLine("ERROR: Ingrese un correo electrónico válido.");
                return false;
            }
            return true;
        }

        static public bool ValidarDescripcion(string descripcion)
        {
            if (descripcion == "")
            {
                Console.WriteLine("ERROR: La descripción no puede quedar vacía.");
                return false;
            }
            if (descripcion.Length < 10)
            {
                Console.WriteLine("ERROR: La descripción debe contener como mínimo 10 caracteres.");
                return false;
            }
            return true;
        }

        static public bool ValidarTipoDocumento(string opcion)
        {
            if (opcion != "1" && opcion != "2" && opcion != "3")
            {
                Console.WriteLine("ERROR: Seleccione una opción válida del 1 al 3.");
                return false;
            }
            return true;
        }

        static public bool ValidarEspecialidad(string opcion)
        {
            if (opcion != "1" && opcion != "2")
            {
                Console.WriteLine("ERROR: Seleccione 1 para Penal o 2 para Familia.");
                return false;
            }
            return true;
        }
