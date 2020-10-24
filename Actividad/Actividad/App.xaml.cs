﻿using Actividad.Views;
using Xamarin.Forms;

namespace Actividad
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();

            this.MainPage = new NavigationPage(new LeerBorrarPage());
        }

        protected override void OnStart() { }

        protected override void OnSleep() { }

        protected override void OnResume() { }
    }
}
