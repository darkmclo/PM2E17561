using PM2E17561.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PM2E17561
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Title = "Registros de datos";

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Datos_Ubicacion();
        }

        private async void btnsalvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    await DisplayAlert("Error", "GPS no esta activo", "Ok");
                }
                else
                {
                    if(String.IsNullOrWhiteSpace(txtdescripLarga.Text) == true)
                    {
                        await DisplayAlert("Error", "Debe escribir la ubicacion", "Ok");
                    }
                    else
                    {
                        if (String.IsNullOrWhiteSpace(txtdescripCorta.Text) == true)
                        {
                            await DisplayAlert("Error", "Debe escribir la ubicacion corta", "Ok");
                        }
                        else
                        {
                            Guardar_Datos();
                        }
                        
                    }

                     
                }

                /*
                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    txtLatitud.Text = location.Latitude.ToString(); 
                }
                */
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

        private async void btnListado_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Listado());
        }

        private  void reload_Clicked(object sender, EventArgs e)
        {
            Datos_Ubicacion();
        }


        public async void Datos_Ubicacion()
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


        public async void Guardar_Datos()
        {
            var localizacion = new Models.Localizacion
            {
                latitud = Convert.ToDouble(txtLatitud.Text),
                longitud = Convert.ToDouble(txtLongitud.Text),
                descripcion_larga = txtdescripLarga.Text,
                descripcion_corta = txtdescripCorta.Text 
            };

            var resultado = await App.BaseDatos.GrabarLocalizacion(localizacion);

            if (resultado == 1)
            {
                await DisplayAlert("Mensaje", "Registro exitoso!!!", "ok");
                clear();
                Datos_Ubicacion();
            }
            else
            {
                await DisplayAlert("Error", "No se pudo Guardar", "ok");
            }
        }

        public void clear()
        {
            txtLatitud.Text = txtLongitud.Text = txtdescripCorta.Text = txtdescripLarga.Text = String.Empty;
        }

    }
}
