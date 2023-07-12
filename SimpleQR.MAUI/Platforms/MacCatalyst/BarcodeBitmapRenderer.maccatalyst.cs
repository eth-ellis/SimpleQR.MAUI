using CoreGraphics;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Platform;
using UIKit;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;

namespace SimpleQR.MAUI
{
    public class BarcodeWriter : BarcodeWriter<UIImage>, IBarcodeWriter
	{
		BarcodeBitmapRenderer bitmapRenderer;

		public BarcodeWriter()
		{
			this.Renderer = this.bitmapRenderer = new BarcodeBitmapRenderer();
		}

		public Color ForegroundColor
		{
			get => new UIColor(this.bitmapRenderer.ForegroundColor).AsColor();
			set => this.bitmapRenderer.ForegroundColor = value.AsCGColor();
		}

		public Color BackgroundColor
		{
			get => new UIColor(this.bitmapRenderer.BackgroundColor).AsColor();
			set => this.bitmapRenderer.BackgroundColor = value.AsCGColor();
		}
	}

	internal class BarcodeBitmapRenderer : IBarcodeRenderer<UIImage>
	{
		public CGColor ForegroundColor { get; set; } = new CGColor(1.0f, 1.0f, 1.0f);
		
		public CGColor BackgroundColor { get; set; } = new CGColor(0f, 0f, 0f);

		public UIImage Render(BitMatrix matrix, BarcodeFormat format, string content)
		{
			return Render(matrix, format, content, new EncodingOptions());
		}

		public UIImage Render(BitMatrix matrix, BarcodeFormat format, string content, EncodingOptions options)
		{
			UIGraphics.BeginImageContext(new CGSize(matrix.Width, matrix.Height));

			var context = UIGraphics.GetCurrentContext();

			for (var x = 0; x < matrix.Width; x++)
			{
				for (var y = 0; y < matrix.Height; y++)
				{
					context.SetFillColor(matrix[x, y] ? this.ForegroundColor : this.BackgroundColor);
					context.FillRect(new CGRect(x, y, 1, 1));
				}
			}

			var image = UIGraphics.GetImageFromCurrentImageContext();

			UIGraphics.EndImageContext();

			return image;
		}
	}
}