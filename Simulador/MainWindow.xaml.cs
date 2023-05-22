using BIZ;
using COMMON.Entidades;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

namespace Simulador
{
    public partial class MainWindow : Window
    {
        DispatcherTimer Timer;
        UsuarioManager usuarioManager;
        DispositivoManager dispositivoManager;
        List<DispositivoSimulado> dispositivosSimulados;
        string mqttServer = "";
        int mqttPort = 1883;
        private EventHandler Timer_Tick;

        public MainWindow()
        {
            InitializeComponent();
            usuarioManager = new UsuarioManager();
            dispositivoManager = new DispositivoManager();
            cmbUsuarios.ItemsSource = usuarioManager.ObtenerTodos;
            cmbUsar.Items.Clear();
            cmbUsar.Items.Add("Actuales");
            cmbUsar.Items.Add("Generar nuevos");
            dispositivosSimulados = new List<DispositivoSimulado>(); 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Usuario user = cmbUsuarios.SelectedItem as Usuario;
            if (user != null) 
            { 
                switch(cmbUsar.SelectedItem) 
                {
                    case "Actuales":
                        var disp = user.Dispositivos;
                        if(disp.Count > 0) 
                        {
                           foreach(var item in disp)
                            {
                                dispositivosSimulados.Add(new DispositivoSimulado(item, -1, 30, 0, 100, 0, 1500, mqttServer, mqttPort));
                            }
                            IniciarTimer();
                        }
                        else
                        {
                            MessageBox.Show("El usuario no tiene dispositivos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        break;
                    case "Generar nuevos":
                        int num = 0;
                        if(int.TryParse(txtNumDispositivos.Text, out num))
                        {
                            for(int i = 0; i < num; i++)
                            {
                                dispositivosSimulados.Add(new DispositivoSimulado(NuevoDispositivo(i), -1, 30, 0, 100, 0, 1500, mqttServer, mqttPort));
                            }
                            IniciarTimer();
                        }
                        else
                        {
                            MessageBox.Show("Indique el numero de dispositivos tempolares a simular", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        break;
                    default:
                        MessageBox.Show("Primero seleccione el tipo de dispositivos a usar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }
            }
            else
            {
                MessageBox.Show("Primero seleccione el usuario", "Error", MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
        
        private void IniciarTimer()
        {
            lstDispositivos.ItemsSource = null;
            lstDispositivos.ItemsSource = dispositivosSimulados;
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 10);
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }







        private Dispositivo NuevoDispositivo(int i)
        {
            Dispositivo d = new Dispositivo();
            d.Id = Guid.NewGuid().ToString();
            d.Ubicacion = "Ubicación " + i;
            d.Habilitado = true;
            d.EstatusRele1 = false;
            d.EstatusRele2 = false;
            d.EstatusRele3 = false;
            d.EstatusRele4 = false;
            d.FechaColocacion = DateTime.Now;
            d.Nombre = "Temp" + i;
            d.NombreRele1 = "Rele 1";
            d.NombreRele2 = "Rele 2";
            d.NombreRele3 = "Rele 3";
            d.NombreRele4 = "Rele 4";
            return d;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            foreach(var item in dispositivosSimulados)
            {
                item.Detener();
            }
            dispositivosSimulados.Clear();
        }
    }
}
