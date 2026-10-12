  // ==================== USUARIOS ====================

  static public bool DNIUsuarioRegistrado(string dni)
  {
      for (int i = 0; i < cantidadUsuarios; i++)
      {
          if (usuariosDnis[i] == dni)
          {
              Console.WriteLine("ERROR: El DNI ya se encuentra registrado.");
              return true;
          }
      }
      return false;
  }

  static public bool CorreoUsuarioRegistrado(string correo)
  {
      for (int i = 0; i < cantidadUsuarios; i++)
      {
          if (usuariosCorreos[i].ToLower() == correo.ToLower())
          {
              Console.WriteLine("ERROR: El correo ya se encuentra registrado.");
              return true;
          }
      }
      return false;
  }

  static public bool ValidarContrasena(string contrasena)
  {
      if (contrasena.Length < 6)
      {
          Console.WriteLine("ERROR: La contraseña debe contener como mínimo 6 caracteres.");
          return false;
      }
      return true;
  }

  static public void GuardarUsuarios()
  {
      StreamWriter archivo = new StreamWriter(archivoUsuarios, false);

      for (int i = 0; i < cantidadUsuarios; i++)
      {
          archivo.WriteLine(usuariosNombres[i] + "|" + usuariosDnis[i] + "|" + usuariosTelefonos[i] + "|" + usuariosCorreos[i] + "|" + usuariosContrasenas[i]);
      }

      archivo.Close();
  }

  static public void CargarUsuarios()
  {
      if (!File.Exists(archivoUsuarios)) return;

      string[] lineas = File.ReadAllLines(archivoUsuarios);

      for (int i = 0; i < lineas.Length && cantidadUsuarios < 100; i++)
      {
          string[] datos = lineas[i].Split('|');

          if (datos.Length == 5)
          {
              usuariosNombres[cantidadUsuarios] = datos[0];
              usuariosDnis[cantidadUsuarios] = datos[1];
              usuariosTelefonos[cantidadUsuarios] = datos[2];
              usuariosCorreos[cantidadUsuarios] = datos[3];
              usuariosContrasenas[cantidadUsuarios] = datos[4];
              cantidadUsuarios++;
          }
      }
  }
