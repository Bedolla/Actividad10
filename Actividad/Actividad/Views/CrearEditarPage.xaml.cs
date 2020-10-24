using Actividad.Models;
using Actividad.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Actividad.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrearEditarPage : ContentPage
    {
        public CrearEditarPage() => this.InitializeComponent();

        protected override void OnAppearing() => this.ButtonGuardar.Text = this.Title;

        private async void ButtonGuardar_OnClicked(object transmisor, EventArgs argumentos)
        {
            if (this.Title.Equals("Crear"))
            {
                if
                (
                    String.IsNullOrWhiteSpace(this.EntryNombre.Text) ||
                    String.IsNullOrWhiteSpace(this.EntryCorreo.Text) ||
                    String.IsNullOrWhiteSpace(this.EntryTelefono.Text)
                )
                    return;

                Persona personaPorCrear = new Persona
                {
                    Id = Guid.NewGuid(),
                    Nombre = this.EntryNombre.Text.Trim(),
                    Correo = this.EntryCorreo.Text.Trim(),
                    Telefono = this.EntryTelefono.Text.Trim()
                };

                if (PersonaService.NoExiste(personaPorCrear))
                {
                    PersonaService.Crear(personaPorCrear);

                    this.LabelInformacion.Text = "Persona creada";

                    this.EntryNombre.Text = String.Empty;
                    this.EntryCorreo.Text = String.Empty;
                    this.EntryTelefono.Text = String.Empty;
                }
                else
                {
                    await this.DisplayAlert("Ya existe", "Ya hay una persona con ese Nombre o Correo.", "Entendido");
                }
            }
            else
            {
                if
                (
                    String.IsNullOrWhiteSpace(this.EntryNombre.Text) ||
                    String.IsNullOrWhiteSpace(this.EntryCorreo.Text) ||
                    String.IsNullOrWhiteSpace(this.EntryTelefono.Text)
                )
                    return;

                Persona personaPorEditar = new Persona
                {
                    Id = new Guid(this.EntryId.Text),
                    Nombre = this.EntryNombre.Text.Trim(),
                    Correo = this.EntryCorreo.Text.Trim(),
                    Telefono = this.EntryTelefono.Text.Trim()
                };

                if (PersonaService.NoExiste(personaPorEditar))
                {
                    PersonaService.Editar(personaPorEditar);

                    this.LabelInformacion.Text = "Persona editada";

                    this.EntryNombre.Text = String.Empty;
                    this.EntryCorreo.Text = String.Empty;
                    this.EntryTelefono.Text = String.Empty;

                    this.ButtonGuardar.Text = this.Title = "Crear";
                }
                else
                {
                    await this.DisplayAlert("Ya existe", "Ya hay una persona con ese Nombre o Correo.", "Entendido");
                }
            }
        }
    }
}
