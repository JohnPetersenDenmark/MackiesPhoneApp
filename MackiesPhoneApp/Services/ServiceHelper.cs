using Microsoft.Maui;
using Microsoft.Maui.Controls; // For MauiApplication
using System;



namespace MackiesPhoneApp.Services
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Maui.Controls;
    using System;

    public static class ServiceHelper
    {
        public static T GetService<T>() where T : class
        {
            var services = Application.Current?.Handler?.MauiContext?.Services;
            return services?.GetService(typeof(T)) as T;
        }
    }
}
