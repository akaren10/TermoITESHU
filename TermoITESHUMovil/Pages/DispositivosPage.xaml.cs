using BIZ;
using COMMON.Entidades;
using TermoITESHUMovil.Pages;

namespace TermoITESHUMovil.Pages;

public partial class DispositivosPage : ContentPage
{
	string IdUsuario;
	Usuario usr;
	UsuarioManager manager;
	public DispositivosPage(string idUsuario)
	{
        InitializeComponent();
		manager = new UsuarioManager();
		IdUsuario = idUsuario; 
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		usr = manager.ObtenerPorId(IdUsuario);
		BindingContext = usr;
    }

	private void btnAgregarDispositivo_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new EditarDispositivo(usr, ""));
	}

	private void lstDispositivos_ItemTapedd(object sender, ItemTappedEventArgs e)
	{
		Navigation.PushAsync(new EstadoDispositivo(usr, (e.Item as Dispositivo).Id));
	}
}