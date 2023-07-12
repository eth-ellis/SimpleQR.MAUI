using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Win2D;
using Microsoft.Maui.Platform;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;

namespace SimpleQR.MAUI
{
    public class BarcodeWriter : BarcodeWriter<WriteableBitmap>, IBarcodeWriter
    {
        BarcodeBitmapRenderer bitmapRenderer;

        public BarcodeWriter()
        {
            this.Renderer = this.bitmapRenderer = new BarcodeBitmapRenderer();
        }

        public Color ForegroundColor
        {
            get => this.bitmapRenderer.ForegroundColor.ToColor();
            set => this.bitmapRenderer.ForegroundColor = value.AsColor();
        }

        public Color BackgroundColor
        {
            get => this.bitmapRenderer.BackgroundColor.ToColor();
            set => this.bitmapRenderer.BackgroundColor = value.AsColor();
        }
    }

    internal class BarcodeBitmapRenderer : IBarcodeRenderer<WriteableBitmap>
    {
        public Windows.UI.Color ForegroundColor { get; set; } = Microsoft.UI.Colors.Black;

        public Windows.UI.Color BackgroundColor { get; set; } = Microsoft.UI.Colors.White;

        public WriteableBitmap Render(BitMatrix matrix, BarcodeFormat format, string content)
        {
            return Render(matrix, format, content, null);
        }

        virtual public WriteableBitmap Render(BitMatrix matrix, BarcodeFormat format, string content, EncodingOptions options)
        {
            var width = matrix.Width;
            var height = matrix.Height;
            var outputContent =
                (options == null || !options.PureBarcode) &&
                !string.IsNullOrEmpty(content) &&
                (
                    format == BarcodeFormat.CODE_39 ||
                    format == BarcodeFormat.CODE_128 ||
                    format == BarcodeFormat.EAN_13 ||
                    format == BarcodeFormat.EAN_8 ||
                    format == BarcodeFormat.CODABAR ||
                    format == BarcodeFormat.ITF ||
                    format == BarcodeFormat.UPC_A ||
                    format == BarcodeFormat.MSI ||
                    format == BarcodeFormat.PLESSEY
                );

            var emptyArea = outputContent ? 16 : 0;
            var pixelsize = 1;

            if (options != null)
            {
                if (options.Width > width)
                {
                    width = options.Width;
                }
                if (options.Height > height)
                {
                    height = options.Height;
                }
                // calculating the scaling factor
                pixelsize = width / matrix.Width;
                if (pixelsize > height / matrix.Height)
                {
                    pixelsize = height / matrix.Height;
                }
            }


            var foreground = new byte[] { this.ForegroundColor.B, this.ForegroundColor.G, this.ForegroundColor.R, this.ForegroundColor.A };
            var background = new byte[] { this.BackgroundColor.B, this.BackgroundColor.G, this.BackgroundColor.R, this.BackgroundColor.A };
            var bitmap = new WriteableBitmap(width, height);

            using (var stream = WindowsRuntimeBufferExtensions.AsStream(bitmap.PixelBuffer))
            {
                for (var y = 0; y < matrix.Height - emptyArea; y++)
                {
                    for (var pixelsizeHeight = 0; pixelsizeHeight < pixelsize; pixelsizeHeight++)
                    {
                        for (var x = 0; x < matrix.Width; x++)
                        {
                            var color = matrix[x, y] ? foreground : background;
                            for (var pixelsizeWidth = 0; pixelsizeWidth < pixelsize; pixelsizeWidth++)
                            {
                                stream.Write(color, 0, 4);
                            }
                        }
                        for (var x = pixelsize * matrix.Width; x < width; x++)
                        {
                            stream.Write(background, 0, 4);
                        }
                    }
                }
                for (var y = matrix.Height * pixelsize - emptyArea; y < height; y++)
                {
                    for (var x = 0; x < width; x++)
                    {
                        stream.Write(background, 0, 4);
                    }
                }
            }

            bitmap.Invalidate();

            return bitmap;
        }
    }
}