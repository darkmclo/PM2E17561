﻿
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace PM2E17561.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Mapa : ContentPage
    {
        public Mapa(double lat, double lon,string des_corta,string des_larga)
        {
            InitializeComponent();

            Position position = new Position(lat, lon);
            MapSpan mapSpan = new MapSpan(position, 0.01, 0.01);
            Xamarin.Forms.Maps.Map map = new Xamarin.Forms.Maps.Map(mapSpan);

            Pin pin = new Pin
            {
                Label = des_corta,
                Address = des_larga,
                Type = PinType.Place,
                Position = position
            };
            map.Pins.Add(pin);

            Content = map;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    Title = "GPS Inactivo";

                }
                else
                {
                    Title = "GPS Activo";
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
            catch (System.Exception)
            {
                // Unable to get location
            }
        }

    }
}