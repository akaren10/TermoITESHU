using MQTTnet;
using MQTTnet.Client;
using System.Text;

namespace BIZ
{
    public class MqttService
    {
        public string MqttServer { get; set; }
        public int MqttPort { get; set; }
        public string NombreCliente { get; set; }

        IMqttClient mqttClient;
        MqttClientOptions options;
        MqttFactory factory;
        public event EventHandler<string> MensajeRecibido;
        public MqttService(string mqttServer, int mqttPort, string nombreCliente)
        {
            MqttServer = mqttServer;
            MqttPort = mqttPort;
            NombreCliente = nombreCliente;
            factory = new MqttFactory();
            mqttClient = factory.CreateMqttClient();
            mqttClient.DisconnectedAsync += MqttClient_DisconnectedAsync;
            mqttClient.ApplicationMessageReceivedAsync += MqttClient_ApplicationMessageReceivedAsync;
            options = new MqttClientOptionsBuilder()
                .WithTcpServer(mqttServer, mqttPort)
                .WithClientId(NombreCliente)
                .WithCleanSession(false)
                .Build();
            ConectarMQTT();
        }
        public async void Publicar(string mensaje, string topic)
        {
            await mqttClient.PublishStringAsync(topic, mensaje);
        }
        private Task MqttClient_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            string cmd = Encoding.UTF8.GetString(arg.ApplicationMessage.Payload);
            MensajeRecibido(this, cmd);
            return Task.CompletedTask;
        } 
        private Task MqttClient_DisconnectedAsync(MqttClientDisconnectedEventArgs arg)
        {
            ConectarMQTT();
            return Task.CompletedTask;
        }
        private async void ConectarMQTT()
        {
            await mqttClient.ConnectAsync(options,CancellationToken.None);
            var suscriptionOptions = factory.CreateSubscribeOptionsBuilder().WithTopicFilter(f => { f.WithTopic($"{NombreCliente}/in"); }).Build();
            while(!mqttClient.IsConnected)
            {
                Thread.Sleep(10);
            }
            var response = await mqttClient.SubscribeAsync(suscriptionOptions,CancellationToken.None);
        }

    }
}
