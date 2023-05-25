using BIZ;
using COMMON.Entidades;
using COMMON.Modelos;


namespace TermoITESHUMovil.Pages
{
	public partial class NuevaCuentaPage : ContentPage
	{
		Usuario user;
		UsuarioManager manager;
		public NuevaCuentaPage()
		{
			InitializeComponent();
			manager = new UsuarioManager();
			user = new Usuario();
			BindingContext = user;
			user.Dispositivos = new List<Dispositivo>();
			user.EsAdministrador = false;
			user.Habilitado = true;
		}
		private void btnCrearCuenta_Clicked(object sender, EventArgs e)
		{
			string mensaje = "";
			if (manager.CrearCuenta(user, entPassword2.Text, out mensaje))
			{
				DisplayAlert("TermoITESHU", mensaje, "Ok");
				Navigation.PopAsync();
			}
			else
			{
				DisplayAlert("Error:", mensaje, "Ok");
			}
		}
	}
}