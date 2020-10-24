using Actividad.Models;
using Actividad.Services;
using Actividad.ViewModels;
using System;
using Xamarin.Forms;

namespace Actividad.Views
{
    public partial class LeerBorrarPage : ContentPage
    {
        public LeerBorrarPage() => this.InitializeComponent();

        protected override void OnAppearing() => this.ListViewPersonas.ItemsSource = PersonaService.Listar();

        private async void ListaPersonas_OnItemSelected(object transmisor, SelectedItemChangedEventArgs argumentos)
        {
            if (argumentos.SelectedItem is null) return;

            Persona personaSeleccionada = (Persona)argumentos.SelectedItem;

            this.ListViewPersonas.SelectedItem = null;

            await this.Navigation.PushAsync(new CrearEditarPage
            {
                Title = "Editar",
                BindingContext = new CrearEditarViewModel
                {
                    Persona = new Persona
                    {
                        Id = personaSeleccionada.Id,
                        Nombre = personaSeleccionada.Nombre,
                        Correo = personaSeleccionada.Correo,
                        Telefono = personaSeleccionada.Telefono
                    }
                }
            });
        }

        private async void ButtonAgregar_OnClicked(object transmisor, EventArgs argumentos) =>
            await this.Navigation.PushAsync(new CrearEditarPage
            {
                Title = "Crear"
            });

        private async void ButtonBorrar_OnClicked(object transmisor, EventArgs argumentos)
        {
            if (!await this.DisplayAlert("Borrar", "¿Realmente desea borrar a la persona?", "Sí", "No")) return;

            PersonaService.Borrar(((Button)transmisor).CommandParameter.ToString());

            this.ListViewPersonas.ItemsSource = PersonaService.Listar();
            this.ListViewPersonas.SelectedItem = null;
        }
    }
}
