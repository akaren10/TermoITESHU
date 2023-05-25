using COMMON.Entidades;
using COMMON.Modelos;
using Newtonsoft.Json;
using System.Text;

namespace BIZ
{
    public class LecturaManager:GenericManager<Lectura>
    {
        public List<Lectura> ObtenerLecturasDeDispositivo(string idDispositivo, int cuantas) => ObtenerLecturasDeDispositivoAsync(idDispositivo, cuantas).Result; 
        public List<Lectura> ObtenerHistoricoDeLecturas(IntervaloModel datos) => ObtenerHistoricoDeLecturasAsync(datos).Result;

        private async Task<List<Lectura>> ObtenerLecturasDeDispositivoAsync(string idDispositivo, int cuantas)
        {
            if (string.IsNullOrWhiteSpace(idDispositivo))
            {
                Error = "Debe indicar el id del dispositivo";
                return null;
            }
            HttpResponseMessage response= await http.GetAsync($"{typeof(Lectura).Name}/{idDispositivo} /{cuantas}").ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if(response.IsSuccessStatusCode)
            {
                Error = "";
                return JsonConvert.DeserializeObject<List<Lectura>>(content);
            }
            else
            {
                Error = content.ToString();
                return null;
            }
        }

        private async Task<List<Lectura>> ObtenerHistoricoDeLecturasAsync(IntervaloModel datos)
        {
            var c = JsonConvert.SerializeObject(datos);
            var body = new StringContent(c, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await http.PostAsync($"{typeof(Lectura).Name}/ObtenerHistoricoDeLecturas",body).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait (false);
            if(response.IsSuccessStatusCode)
            {
                Error = "";
                return JsonConvert.DeserializeObject<List<Lectura>>(content);
            }
            else
            {
                Error = content.ToString();
                return null;
            }

        }
    }
}
