using Microsoft.Maui.Hosting;
using SimpleQR.MAUI.Controls;
using SimpleQR.MAUI.Handlers;

namespace SimpleQR.MAUI
{
    public static class HostBuilderExtensions
	{
		public static MauiAppBuilder UseSimpleQR(this MauiAppBuilder builder)
		{
            return builder.ConfigureMauiHandlers(handlers =>
			{
				handlers.AddHandler(typeof(QRCodeView), typeof(QRCodeViewHandler));
			});
		}
	}
}
