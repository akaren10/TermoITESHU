using System.Text;

namespace COMMON.Modelos
{
    public class DatosGrafica
    {
        public List<PuntoGrafica> puntosTemperatura {  get; set; }
        public List<PuntoGrafica> puntosHumedad { get; set; }
        public List<PuntoGrafica> puntosLuminosidad { get; set; }
        public DatosGrafica()
        {
            puntosHumedad = new List<PuntoGrafica>();
            puntosLuminosidad= new List<PuntoGrafica>();
            puntosTemperatura = new List<PuntoGrafica>();
        }
        public void AgregarDatos(DateTime fechahora, float temp, float hum, float lum)
        {
            puntosTemperatura.Add(new PuntoGrafica(fechahora.ToString(), temp));
            puntosHumedad.Add(new PuntoGrafica(fechahora.ToString(), hum));
            puntosLuminosidad.Add(new PuntoGrafica(fechahora.ToString(), lum));

        }
       public string GenerarHTML()
        {
            StringBuilder html = new StringBuilder();
            html.Append("<html><head><script src='https://cdn.plot.ly/plotly-2.20.0.min.js'></script></head><body><div id='myDiv'></div></script>");
            //temperatura
            html.Append("var trace1 = {x: [");
            foreach (var item in puntosTemperatura) 
                 html.Append($"'{item.X}',");
                 html.Append("], y: [");
            foreach (var item in puntosTemperatura) 
                 html.Append($"{item.Y},");
                 html.Append("],type: 'scatter', name: \"Temperatura\"};");
            //humedad
            html.Append("var trace2 = {x:[");
            foreach (var item in puntosHumedad)
                html.Append($"'{item.X}',");
                html.Append("], y: [");
            foreach (var item in puntosHumedad)
                html.Append($"'{item.Y}',");
                html.Append("], type:'scatter', name : \"Humedad\"};");
            //luminosidad
            html.Append("var trace3 = {x: [");
            foreach(var item in puntosLuminosidad)
                html.Append($"'{item.X}',");
                html.Append("], y: [");
            foreach (var item in puntosLuminosidad)
                html.Append($"'{item.Y}',");
                html.Append("], type:'scatter', name:\"Luminosidad\"};");
            html.Append("var data = [trace1, trace2, trace3];Plotly.newPlot('myDiv',data);</script></body></html>");
            return html.ToString();
        }
    }
}
