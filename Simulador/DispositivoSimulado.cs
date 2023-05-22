using BIZ;
using COMMON.Entidades;
using System;
using System.Windows.Threading;


namespace Simulador
{
    public class DispositivoSimulado
    {
        LecturaManager lecturaManager;
        DispatcherTimer timerLectura, timerPost;
        public Dispositivo dispositivo { get; private set; }
        MqttService mqtt;
        Random r;
        public string NombreCliente { get; private set; }
        public event EventHandler<Lectura> LecturaActualizada;
         public Lectura Lectura { get; private set; }
         public int MinTemp { get; private set; }
         public int MaxTemp { get; private set;}
         public int MinHum  { get; private set; }
         public int MaxHum { get; private set; }
         public int MinLum { get; private set; }
         public int MaxLum { get; private set; }

        public DispositivoSimulado(Dispositivo dispositivo, int minTemp, int maxTemp, int minHum, int maxHum, int munLum, int maxLum, string mqttServer, int mqttPort)
        {
            this.dispositivo = dispositivo;
            lecturaManager = new LecturaManager();
            MinTemp = minTemp;
            MaxTemp = maxTemp;
            MinHum = minHum;
            MaxHum = maxHum;
            MinLum = MinLum;
            MaxLum = MaxLum;
            timerLectura = new DispatcherTimer();
            timerLectura.Interval = new TimeSpan(0, 0, 10);
            timerLectura.Tick += Timer_Tick;
            timerLectura.Start();
            timerPost = new DispatcherTimer();
            timerPost.Interval = new TimeSpan (0, 10, 0);
            timerPost.Tick += TimerPost_Tick;
            timerPost.Start();
            Lectura = new Lectura();
            r = new Random(DateTime.Now.Millisecond);
            string[] datos = dispositivo.Id.Split (',');
            NombreCliente = datos[0];
            mqtt = new MqttService(mqttServer, mqttPort, NombreCliente);
            mqtt.MensajeRecibido += Mqtt_MensajeRecibido;
        }
        private void Mqtt_MensajeRecibido(object? sender, string e)
        {
            switch (e)
            {
                case "L11":
                    Lectura.EstatusRele1 = true;
                break;
                case "L10":
                    Lectura.EstatusRele1 = false;
                break;
                case "L21":
                    Lectura.EstatusRele1 = true;
                    break;
                case "L20":
                    Lectura.EstatusRele1 = false;
                    break;
                case "L31":
                    Lectura.EstatusRele1 = true;
                    break;
                case "L30":
                    Lectura.EstatusRele1 = false;
                    break;
                case "L41":
                    Lectura.EstatusRele1 = true;
                    break;
                case "L40":
                    Lectura.EstatusRele1 = false;
                    break;
                default:
                    break;
            }
        }
        public void Detener()
        {
            timerLectura.Stop();
            timerPost.Stop();
        }
        private void TimerPost_Tick(object? sender, EventArgs e)
        {
            lecturaManager.Insertar(Lectura);
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            Lectura.Humedad = MinHum + float.Parse(((MaxHum - MinHum) * r.NextDouble()).ToString());
            Lectura.Temperatura = MinTemp + float.Parse(((MaxTemp - MinTemp) * r.NextDouble()).ToString());
            Lectura.Luminosidad = MinLum + float.Parse(((MaxLum - MinLum) * r.NextDouble()).ToString());
            string trama = $"{Lectura.Temperatura},{Lectura.Humedad},{Lectura.Luminosidad},{Lectura.EstatusRele1},{Lectura.EstatusRele2},{Lectura.EstatusRele3},{Lectura.EstatusRele4}";
            mqtt.Publicar(trama, $"{NombreCliente}/out");
        }
    }
}
