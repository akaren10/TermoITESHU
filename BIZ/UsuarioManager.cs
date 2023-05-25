using COMMON.Entidades;
using COMMON.Modelos;
using Newtonsoft.Json;
using System.Text;

namespace BIZ
{
    public class UsuarioManager:GenericManager<Usuario>
    {
        public bool CrearCuenta(Usuario user, string password2, out string mensaje)
        {
            if(user == null)
            {
                mensaje = "Error al obtener los datos";
                return false;
            }
            if(user.Password != password2)
            {
                mensaje = "Los Passwords deben coincidir";
            }
            if(Insertar(user) != null)
            {
                mensaje = "Cuenta creada correctamente";
                return true;
            }
            else
            {
                mensaje = Error;
                return false;
            }
        }

        public Usuario Login(LoginModel model) => LoginAsync(model).Result;

        private async Task<Usuario> LoginAsync(LoginModel model)
        {
            var c = JsonConvert.SerializeObject(model);
            var body = new StringContent(c, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await http.PostAsync($"{typeof(Usuario).Name}/Login", body).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if(response.IsSuccessStatusCode)
            {
                Error = "";
                return JsonConvert.DeserializeObject<Usuario>(content);
            }
            else
            {
                Error = "Usuario y/o Contraseña no validos...";
                return null;
            }
        }
        
    }
}
