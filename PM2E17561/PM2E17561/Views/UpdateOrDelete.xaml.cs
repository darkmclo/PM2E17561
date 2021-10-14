using PM2E17561.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E17561.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateOrDelete : ContentPage
    {
        public UpdateOrDelete(Localizacion local)
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            txtcodigo.Text = local.codigo.ToString();
            txtLatitud.Text = local.latitud.ToString();
            txtLongitud.Text = local.longitud.ToString();
            txtdescripLarga.Text = local.descripcion_larga.ToString();
            txtdescripCorta.Text = local.descripcion_corta.ToString();
        }

   
        

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var localizacion = new Models.Localizacion
            {
                codigo = Convert.ToInt32(txtcodigo.Text),
                latitud = Convert.ToDouble(txtLatitud.Text),
                longitud = Convert.ToDouble(txtLongitud.Text),
                descripcion_larga = txtdescripLarga.Text,
                descripcion_corta = txtdescripCorta.Text
            };

            var resultado = await App.BaseDatos.EliminarLocalizacion(localizacion);

            if (resultado == 1)
            {
                await DisplayAlert("Mensaje", "Registro Eliminado exitoso!!!", "ok");
                await Navigation.PushAsync(new Listado());
            }
            else
            {
                await DisplayAlert("Error", "No se pudo Eliminado", "ok");
            }

             

             

        }

        private void btnModificar_Clicked(object sender, EventArgs e)
        {

        }

        private void btnMapa_Clicked(object sender, EventArgs e)
        {

        }
    }
}