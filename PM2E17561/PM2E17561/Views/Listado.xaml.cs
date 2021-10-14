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
    public partial class Listado : ContentPage
    {
        public Listado()
        {
            InitializeComponent();
            Title = "Listado de localizadores";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            cargarListado();
        }

        public async void cargarListado()
        {
            var lista = await App.BaseDatos.ObtenerListaLocalizacion();
            lstLocalizacion.ItemsSource = lista;
        }

        private async void lstLocalizacion_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                //var item = (Localizacion)e.SelectedItem;
                Localizacion local = (Localizacion)e.SelectedItem;

                //await DisplayAlert("s", l.codigo.ToString(), "ok");
                UpdateOrDelete ventana = new UpdateOrDelete(local);
                await Navigation.PushAsync(ventana);


                lstLocalizacion.SelectedItem = null;

            }

        }
    }
}