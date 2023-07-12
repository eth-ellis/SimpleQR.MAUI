using Microsoft.Maui;
using Microsoft.Maui.Graphics;

namespace SimpleQR.MAUI.Controls
{
    public interface IQRCodeView : IView
    {
        int Size { get; }

        Color ForegroundColor { get; }

        Color BackgroundColor { get; }

        string Value { get; }
    }
}
