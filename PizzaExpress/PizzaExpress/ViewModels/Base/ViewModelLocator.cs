﻿using Autofac;
using PizzaExpress.Services.Navigation;
using PizzaExpress.Services.PizzaService;
using PizzaExpress.Services.RequestProvider;
using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;

namespace PizzaExpress.ViewModels.Base
{
    public static class ViewModelLocator
    {
        private static IContainer _container;

        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(AutoWireViewModelProperty, value);
        }

        static ViewModelLocator()
        {
            var builder = new ContainerBuilder();

            // View models
            builder.RegisterType<MainViewModel>();
            builder.RegisterType<PizzaToppingViewModel>();

            // Services
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<RequestProvider>().As<IRequestProvider>().SingleInstance();
            builder.RegisterType<PizzaService>().As<IPizzaService>().SingleInstance();

            if (_container != null)
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
