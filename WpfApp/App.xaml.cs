using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Navigation;
using WpfApp.MVVM.View;
using WpfApp.MVVM.ViewModel;
using WpfApp.Service;
using WpfApp.Service.Interface;
using WpfApp.Stores;

namespace WpfApp
{
    public  partial class App : Application
    {
        public new static App Current => (App)Application.Current;

        public IServiceProvider Services { get; }

        //생성자
        public App()
        {
            Services = ConfigureServices();
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            var mainView = Services.GetService<MainWindow>();
            mainView.Show();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            //Stores
            services.AddSingleton<MainNavigationStore>();

            services.AddSingleton<HomeStore>();


            //ViewModel
            services.AddTransient<HomeViewModel>();
            services.AddTransient<SignInViewModel>();
            services.AddTransient<SignUpViewModel>();
            services.AddTransient<SubMainViewModel>();

            //Serivce
            services.AddSingleton<IServerCommunicationService, ServerCommunicationService>();
            services.AddSingleton<INavigationService, Service.NavigationService>();


            //View
            services.AddSingleton(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<SubMainViewModel>()
            });


            return services.BuildServiceProvider();
        }
    }
}