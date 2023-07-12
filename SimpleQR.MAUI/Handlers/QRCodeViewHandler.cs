using Microsoft.Maui;
using Microsoft.Maui.Handlers;
using SimpleQR.MAUI.Controls;
using ZXing;

#if IOS || MACCATALYST
using NativePlatformImageView = UIKit.UIImageView;
using NativePlatformImage = UIKit.UIImage;

#elif ANDROID
using NativePlatformImageView = Android.Widget.ImageView;
using NativePlatformImage = Android.Graphics.Bitmap;

#elif WINDOWS
using NativePlatformImageView = Microsoft.UI.Xaml.Controls.Image;
using NativePlatformImage = Microsoft.UI.Xaml.Media.Imaging.WriteableBitmap;
#endif

namespace SimpleQR.MAUI.Handlers
{
    public partial class QRCodeViewHandler : ViewHandler<IQRCodeView, NativePlatformImageView>
	{
        NativePlatformImageView imageView;
        BarcodeWriter writer;

		public static PropertyMapper<IQRCodeView, QRCodeViewHandler> QRCodeMapper = new()
		{
            [nameof(IQRCodeView.Size)] = MapUpdateBarcode,
            [nameof(IQRCodeView.ForegroundColor)] = MapUpdateBarcode,
			[nameof(IQRCodeView.BackgroundColor)] = MapUpdateBarcode,
			[nameof(IQRCodeView.Value)] = MapUpdateBarcode,
		};

		public QRCodeViewHandler() : base(QRCodeMapper)
		{
			this.writer = new BarcodeWriter();
		}

		public QRCodeViewHandler(PropertyMapper mapper = null) : base(mapper ?? QRCodeMapper)
		{
			this.writer = new BarcodeWriter();
		}

		protected override NativePlatformImageView CreatePlatformView()
		{
#if IOS || MACCATALYST
			this.imageView ??= new UIKit.UIImageView { BackgroundColor = UIKit.UIColor.Clear };
#elif ANDROID
			this.imageView = new NativePlatformImageView(Context);
			this.imageView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#elif WINDOWS
			this.imageView = new NativePlatformImageView();
#endif
			return this.imageView;
		}

		protected override void ConnectHandler(NativePlatformImageView nativeView)
		{
			base.ConnectHandler(nativeView);

			UpdateBarcode();
		}

		void UpdateBarcode()
		{
			this.writer.Format = BarcodeFormat.QR_CODE;
			this.writer.Options.Width = (int)this.VirtualView.Size;
			this.writer.Options.Height = (int)this.VirtualView.Size;
			this.writer.ForegroundColor = this.VirtualView.ForegroundColor;
			this.writer.BackgroundColor = this.VirtualView.BackgroundColor;
			this.writer.Options.NoPadding = true;
			this.writer.Options.Margin = 0;

			NativePlatformImage image = this.writer.Write(this.VirtualView.Value);

#if IOS || MACCATALYST
			this.imageView.Image = image;
#elif ANDROID
			this.imageView.SetImageBitmap(image);
#elif WINDOWS
			this.imageView.Source = image;
#endif
		}

		public static void MapUpdateBarcode(QRCodeViewHandler handler, IQRCodeView qrCode)
		{
			handler.UpdateBarcode();
		}
	}
}
