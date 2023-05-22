using COMMON.Entidades;
using Newtonsoft.Json;
using System.Text;

namespace BIZ
{
    public abstract class GenericManager<T> where T : Base
    {
        protected string urlBase = @"";
        protected HttpClient http;
        public string Error { get;protected set; }
        public GenericManager()
        {
            http = new HttpClient();
            http.BaseAddress = new Uri(urlBase);
            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("aplication/json"));
         }

        private async Task<List<T>> ObtenerTodosAsync()
        {
            HttpResponseMessage response= await http.GetAsync(typeof(T).Name).ConfigureAwait(false);
            var content=await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if(response.IsSuccessStatusCode)
            {
                Error = "";
                return JsonConvert.DeserializeObject<List<T>>(content);
            }
            else
            {
                Error=content.ToString();
                return null;
            }
        }
        private async Task<T> ObtenerPorIdAsync(string id)
        {
            HttpResponseMessage response=await http.GetAsync(typeof(T).Name + "/"+id).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if(response.IsSuccessStatusCode)
            {
                Error = "";
                return JsonConvert.DeserializeObject<T>(content);
            }
            else 
            { 
                Error=content.ToString();
                return null;
            }
        }
        private async Task<T> InsertarAsync(T item)
        {
            var c = JsonConvert.SerializeObject(item);
            var body = new StringContent(c, Encoding.UTF8,"application/json");
            HttpResponseMessage response=await http.PostAsync(typeof(T).Name,body).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait (false);
            if(response.IsSuccessStatusCode)
            {
                Error = "";
                return JsonConvert.DeserializeObject<T>(content);
            }
            else 
            {
                Error=content.ToString();
                return null;
            }
        }
        private async Task<T> ActualizarAsync(T item)
        {
            var c = JsonConvert.SerializeObject(item);
            var body = new StringContent(c, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await http.PutAsync(typeof(T).Name, body).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                Error = "";
                return JsonConvert.DeserializeObject<T>(content);
            }
            else
            {
                Error = content.ToString();
                return null;
            }
        }
        private async Task<T> EliminarAsync(string id)
        {
            HttpResponseMessage response = await http.DeleteAsync(typeof(T).Name+"/"+id).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                Error = "";
                return JsonConvert.DeserializeObject<T>(content);
            }
            else
            {
                Error = content.ToString();
                return null;
            }
        }

        public List<T> ObtenerTodos
        {
            get => ObtenerTodosAsync().Result;
        }
        public T ObtenerPorId(string id) => ObtenerPorIdAsync(id).Result;
        public T Insertar(T item) => InsertarAsync(item).Result;
        public T Actualizar(T item)=>ActualizarAsync(item).Result;
        public T Eliminar(string id) => EliminarAsync(id).Result;



    }
}
