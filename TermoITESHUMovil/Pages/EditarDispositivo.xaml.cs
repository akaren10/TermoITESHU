using BIZ;
using COMMON.Entidades;
using TermoITESHUMovil.Pages;

namespace TermoITESHUMovil.Pages;

public partial class EditarDispositivo : ContentPage
{
	Dispositivo dispositivo;
	Usuario usuario;
	bool esEdicion;
	UsuarioManager usuarioManager;
	public EditarDispositivo(Usuario user, string idDispositivo)
	{
        InitializeComponent();
        usuarioManager = new UsuarioManager();
		usuario = user;
		if (string.IsNullOrEmpty(idDispositivo))
		{
			dispositivo = new Dispositivo();
			dispositivo.Id = Guid.NewGuid().ToString();
			dispositivo.FechaHora = DateTime.Now;
			dispositivo.FechaColocacion = DateTime.Now;
			Title = "Nuevo dispositivo";
			esEdicion = false;
		}
		else
		{
			dispositivo = user.Dispositivos.Where(d => d.Id == idDispositivo).SingleOrDefault();
			esEdicion = true;
			Title = $"Editar {dispositivo.Nombre}";
		}
			btnEliminar.IsVisible = esEdicion;
			BindingContext = dispositivo;
	}


    private void btnGuardar_Clicked(object sender, EventArgs e)
	{
		if (!esEdicion) 
		{
			usuario.Dispositivos.Add(dispositivo);		
		}
		if (usuarioManager.Actualizar(usuario) != null)
		{
			DisplayAlert("TermoITESHU", "Dispositivo guardado correctamente", "Ok");
			Navigation.PopAsync();
		}
		else
		{
			DisplayAlert("Error", usuarioManager.Error, "Ok");
		}
	}
	private async void btnEliminar_Clicked(Object sender, EventArgs e)
	{
		if (usuario.Dispositivos.Remove(dispositivo))
		{
			if(usuarioManager.Actualizar(usuario) != null)
			{
				await DisplayAlert("TermoITESHU", "Dispositivo Eliminado Correctamente", "Ok");
				await Navigation.PopAsync();
			}
			else
			{
				await DisplayAlert("Error", usuarioManager.Error, "Ok");
			}
		}
	}

}

public class btnEliminar
{
    public static bool IsVisible { get; internal set; }
}