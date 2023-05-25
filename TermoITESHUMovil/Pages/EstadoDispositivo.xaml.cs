using BIZ;
using COMMON.Entidades;
using COMMON.Modelos;

namespace TermoITESHUMovil.Pages;

public partial class EstadoDispositivo : ContentPage
{
	Usuario usuario;
	Dispositivo dispositivo;
	string idDispositivo;
	UsuarioManager manager;
	MqttService mqtt;
	string nombreCliente;
	string comando = "";
	bool[] estadoReles = { false, false, false, false };
	IDispatcherTimer timer;
	DatosGrafica datosGrafica;

	public EstadoDispositivo(Usuario usr, string id)
	{
		InitializeComponent();
		usuario = usr;
		idDispositivo = id;
		manager = new UsuarioManager();
		timer = Application.Current.Dispatcher.CreateTimer();
		timer.Interval = new TimeSpan(0, 0, 1);
		timer.Tick += Timer_Tick;
		timer.Start();
		datosGrafica = new DatosGrafica();
	}


	private void Timer_Tick(object sender, EventArgs e)
	{
		if (comando != "")
		{
			string[] datos = comando.Split(',');
			if (datos.Length == 7)
			{
				lblTemp.Text = datos[0];
				lblHum.Text = datos[1];
				lblLum.Text = datos[2];
				btnRele1.Source = datos[3] == "False" ? "off.png" : "on.png";
				btnRele2.Source = datos[4] == "False" ? "off.png" : "on.png";
				btnRele3.Source = datos[5] == "False" ? "off.png" : "on.png";
				btnRele4.Source = datos[6] == "False" ? "off.png" : "on.png";

				estadoReles[0] = datos[3] == "True";
				estadoReles[1] = datos[4] == "True";
				estadoReles[2] = datos[5] == "True";
				estadoReles[3] = datos[6] == "True";
				datosGrafica.AgregarDatos(DateTime.Now, float.Parse(datos[0]), float.Parse(datos[1]), float.Parse(datos[2]));
				Grafico.Html = datosGrafica.GenerarHTML();
			}
			comando = "";
		}
	}
	protected override void OnAppearing()
	{
		base.OnAppearing();
		mqtt = null;
		usuario = manager.ObtenerPorId(usuario.Id);
		dispositivo = usuario.Dispositivos.SingleOrDefault(d => d.Id == idDispositivo);
		if (dispositivo == null)
		{
			//si el dispositivo fue eliminado no se encontrara el dispositivio, por lo que regresamos a la lista de dispositivos
			Navigation.PopAsync();
		}
		BindingContext = dispositivo;
		string[] datos = dispositivo.Id.Split('-');
		nombreCliente = datos[0];
		mqtt = new MqttService("equipo4.eastus.cloudapp.azure.com", 1883, nombreCliente, "out");
		mqtt.MensajeRecibido += Mqtt_MensajeRecibido;
	}
	private void Mqtt_MensajeRecibido(object sender, string e)
	{
		comando = e;
	}
	private void btnEditar_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new EditarDispositivo(usuario, dispositivo.Id));
	}
	private void TapGestureRecognizer1_Tapped(object sender, EventArgs e)
	{
		if (estadoReles[0])
		{
			mqtt.Publicar("L10", $"{nombreCliente}/in");
			btnRele1.Source = "off.png";
		}
		else
		{
			mqtt.Publicar("L11", $"{nombreCliente}/in");
			btnRele1.Source = "on.png";
		}

	}
	private void TapGestureRecognizer2_Tapped(object sender, EventArgs e)
	{
		if (estadoReles[1])
		{
			mqtt.Publicar("L20", $"{nombreCliente}/in");
			btnRele2.Source = "off.png";
		}
		else
		{
			mqtt.Publicar("L21", $"{nombreCliente}/in");
			btnRele2.Source = "on.png";
		}
	}
	private void TapGestureRecognizer3_Tapped(object sender, EventArgs e)
	{
		if (estadoReles[2])
		{
			mqtt.Publicar("L30", $"{nombreCliente}/in");
			btnRele3.Source = "off.png";
		}
		else
		{
			mqtt.Publicar("L31", $"{nombreCliente}/in");
			btnRele3.Source = "on.png";
		}
	}
	private void TapGestureRecognizer4_Tapped(object sender, EventArgs e)
	{
		if (estadoReles[3])
		{
			mqtt.Publicar("L40", $"{nombreCliente}/in");
			btnRele2.Source = "off.png";
		}
		else
		{
			mqtt.Publicar("L41", $"{nombreCliente}/in");
			btnRele2.Source = "on.png";
		}
	}

	//private void btnHistorico_Clicked(object sender, EventArgs e)
	//{
	//	Navigation.PushAsync(new HistoricoPage(idDispositivo));
	//}

}