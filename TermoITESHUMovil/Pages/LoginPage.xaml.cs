using BIZ;
using COMMON.Modelos;
using COMMON.Entidades;
using TermoITESHUMovil.Pages;

namespace TermoITESHUMovil.Pages;

public partial class LoginPage : ContentPage
{
	UsuarioManager usuarioManager;
	public LoginPage()
	{
		InitializeComponent();
		usuarioManager = new UsuarioManager();
	}
	private void btnIniciarSesion_Clicked(object sender, EventArgs e)
	{
		if(string.IsNullOrEmpty(entEmail.Text) || string.IsNullOrEmpty(entPassword.Text))
		{
			DisplayAlert("TermoITESHU", "Por favor llene los datos", "Ok");
		}
		else
		{
			try
			{
				var usr = usuarioManager.Login(new LoginModel()
				{
					Email = entEmail.Text,
					Password = entPassword.Text
				});
				if (usr != null)
				{
					Navigation.PushAsync(new DispositivosPage(usr.Id));
				}
				else
				{
					DisplayAlert("TermoITESHU", $"Error: {usuarioManager.Error}", "Ok");
				}
			}
			catch (Exception ex)
			{
				DisplayAlert("TermoITESHU", $"Error: {ex.Message}", "Ok");
			}
		}
	}
	private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
	{
		Navigation.PushAsync(new NuevaCuentaPage());
	}
}