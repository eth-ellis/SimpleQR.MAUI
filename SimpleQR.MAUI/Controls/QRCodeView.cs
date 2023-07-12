using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace SimpleQR.MAUI.Controls
{
    public partial class QRCodeView : View, IQRCodeView
	{
        public static readonly BindableProperty SizeProperty = BindableProperty.Create(nameof(Size), typeof(int), typeof(QRCodeView), propertyChanged: (bindable, oldValue, newValue) =>
		{
			var size = (int)newValue;
			var qrCodeView = (QRCodeView)bindable;

			qrCodeView.WidthRequest = size;
			qrCodeView.HeightRequest = size;
        });

        public int Size
        {
            get => (int)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        public static readonly BindableProperty ForegroundColorProperty = BindableProperty.Create(nameof(ForegroundColor), typeof(Color), typeof(QRCodeView), defaultValue: Colors.Black);

		public Color ForegroundColor
		{
			get => (Color)GetValue(ForegroundColorProperty);
			set => SetValue(ForegroundColorProperty, value);
		}

		public new static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(QRCodeView), defaultValue: Colors.White);

		public new Color BackgroundColor
		{
			get => (Color)GetValue(BackgroundColorProperty);
			set => SetValue(BackgroundColorProperty, value);
		}

		public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(string), typeof(QRCodeView));

		public string Value
		{
			get => (string)GetValue(ValueProperty);
			set => SetValue(ValueProperty, value);
		}
	}
}
