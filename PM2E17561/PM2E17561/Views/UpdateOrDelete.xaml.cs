using PM2E17561.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
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

        private async void btnModificar_Clicked(object sender, EventArgs e)
        {


            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    await DisplayAlert("Error", "GPS no esta activo", "Ok");
                    txtLatitud.Text = "00.0";
                    txtLongitud.Text = "00.0";
                }


                if (location != null)
                {
                    //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    txtLatitud.Text = location.Latitude.ToString();
                    txtLongitud.Text = location.Longitude.ToString();

                    var localizacion = new Models.Localizacion
                    {
                        codigo = Convert.ToInt32(txtcodigo.Text),
                        latitud = Convert.ToDouble(txtLatitud.Text),
                        longitud = Convert.ToDouble(txtLongitud.Text),
                        descripcion_larga = txtdescripLarga.Text,
                        descripcion_corta = txtdescripCorta.Text
                    };

                    var resultado = await App.BaseDatos.GrabarLocalizacion(localizacion);

                    if (resultado == 1)
                    {
                        await DisplayAlert("Mensaje", "Registro Modificado exitoso!!!", "ok");
                        await Navigation.PushAsync(new Listado());
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudo Modificado", "ok");
                    }

                }
            }
            catch (FeatureNotSupportedException)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException)
            {

                // Handle not enabled on device exception
            }
            catch (PermissionException)
            {
                // Handle permission exception
            }
            catch (Exception)
            {
                // Unable to get location
            }

            
        }

        private async void btnMapa_Clicked(object sender, EventArgs e)
        {
            double lat = Convert.ToDouble(txtLatitud.Text);
            double lon = Convert.ToDouble(txtLongitud.Text);
            string des_corta = txtdescripLarga.Text;
            string des_larga = txtdescripCorta.Text;

            await Navigation.PushAsync(new Mapa(lat,lon,des_corta,des_larga));
        }
    }
}