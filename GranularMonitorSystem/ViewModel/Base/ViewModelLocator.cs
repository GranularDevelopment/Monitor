using System;
using Autofac;
using Xamarin.Forms;
using System.Reflection;
using System.Globalization;
using GranularMonitorSystem.Services.API.Requests;
using GranularMonitorSystem.Services.API.Identity;
using GranularMonitorSystem.Services.API.Server;
using GranularMonitorSystem.Services.API.Website;
using GranularMonitorSystem.Services.API.Dashboard;

namespace GranularMonitorSystem
{
    public static class ViewModelLocator
    {
        private static Autofac.IContainer _container;

        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(ViewModelLocator.AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(ViewModelLocator.AutoWireViewModelProperty, value);
        }

        public static bool UseMockService { get; set; }

        public static void RegisterDependencies(bool useMockServices)
        {

            var builder = new ContainerBuilder();

           
            //View models
            builder.RegisterType<DashboardViewModel>();
            builder.RegisterType<LoginViewModel>();
            builder.RegisterType<TaskViewModel>();

            builder.RegisterType<WebsiteViewModel>();
            builder.RegisterType<EditWebsiteViewModel>();

            builder.RegisterType<ServerViewModel>();
            //Sub View models
            builder.RegisterType<EditServerViewModel>();
            builder.RegisterType<ServerCpuViewModel>();
            builder.RegisterType<ServerIOViewModel>();
            builder.RegisterType<ServerDiskSpaceViewModel>();
            builder.RegisterType<ServerMemoryViewModel>();

            //Service  

            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<Requests>().As<IRequests>();
            builder.RegisterType<DialogService>().As<IDialogService>();
            builder.RegisterType<IdentityService>().As<IIdentityService>();

            if (useMockServices)
            {
                throw new NotImplementedException();
            }
            else
            {

                builder.RegisterType<ServerService>().As<IServerService>();
                builder.RegisterType<WebsiteService>().As<IWebsiteService>();
                builder.RegisterType<DashboardService>().As<IDashboardService>();
            }

            if(_container != null)
            {
                _container.Dispose();
            }
            _container = builder.Build();

        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;
            if (view == null)
            {
                return;
            }

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }
            var viewModel = _container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}
