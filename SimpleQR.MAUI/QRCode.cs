using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using ZXing;

#if WINDOWS
using System.Runtime.InteropServices.WindowsRuntime;
#endif

#if ANDROID
using System.IO;
#endif

namespace SimpleQR.MAUI
{
    public sealed class QRCode
    {
        public static ImageSource GenerateQRCode(int size, Color foreground, Color background, string value)
        {
            var writer = new BarcodeWriter();

            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options.Width = size;
            writer.Options.Height = size;
            writer.ForegroundColor = foreground;
            writer.BackgroundColor = background;
            writer.Options.NoPadding = true;
            writer.Options.Margin = 0;

            var image = writer.Write(value);

#if IOS || MACCATALYST
            return ImageSource.FromStream(() => image.AsPNG().AsStream());
#elif ANDROID
            var stream = new MemoryStream();
            image.Compress(Android.Graphics.Bitmap.CompressFormat.Png, 100, stream);
            stream.Position = 0;

            return ImageSource.FromStream(() => stream);
#elif WINDOWS
            return ImageSource.FromStream(() => image.PixelBuffer.AsStream());
#endif
        }
    }
}
