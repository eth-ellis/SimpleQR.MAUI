using Android.Graphics;
using Microsoft.Maui.Graphics.Platform;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;

namespace SimpleQR.MAUI
{
    public class BarcodeWriter : BarcodeWriter<Bitmap>, IBarcodeWriter
	{
		BarcodeBitmapRenderer bitmapRenderer;

		public BarcodeWriter()
		{
			this.Renderer = this.bitmapRenderer = new BarcodeBitmapRenderer();
		}

		public Microsoft.Maui.Graphics.Color ForegroundColor
		{
			get => this.bitmapRenderer.ForegroundColor.AsColor();
			set => this.bitmapRenderer.ForegroundColor = value.AsColor();
		}

		public Microsoft.Maui.Graphics.Color BackgroundColor
		{
			get => this.bitmapRenderer.BackgroundColor.AsColor();
			set => this.bitmapRenderer.BackgroundColor = value.AsColor();
		}
	}

	internal class BarcodeBitmapRenderer : IBarcodeRenderer<Bitmap>
	{
		public Color ForegroundColor { get; set; } = Color.Black;

        public Color BackgroundColor { get; set; } = Color.White;

		public Bitmap Render(BitMatrix matrix, BarcodeFormat format, string content)
		{
			return Render(matrix, format, content, new EncodingOptions());
		}

        public Bitmap Render(BitMatrix matrix, BarcodeFormat format, string content, EncodingOptions options)
		{
			var width = matrix.Width;
			var height = matrix.Height;
			var pixels = new int[width * height];
			var outputIndex = 0;
			var fColor = this.ForegroundColor.ToArgb();
			var bColor = this.BackgroundColor.ToArgb();

			for (var y = 0; y < height; y++)
			{
				for (var x = 0; x < width; x++)
				{
					pixels[outputIndex] = matrix[x, y] ? fColor : bColor;
					outputIndex++;
				}
			}

			var bitmap = Bitmap.CreateBitmap(width, height, Bitmap.Config.Argb8888);
			bitmap.SetPixels(pixels, 0, width, 0, 0, width, height);
			return bitmap;
		}
	}
}