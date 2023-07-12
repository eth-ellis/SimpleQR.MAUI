# Introduction

A simple solution for generating QR Codes in .NET MAUI.

# Setup

1. Install the `SimpleQR.MAUI` NuGet package into your project.

2. Include `UseSimpleQR` in your `MauiProgram.cs` builder.

``` cs
using SimpleQR.MAUI;

...

builder
  .UseMauiApp<App>()
  .UseSimpleQR()
```

# Usage

## Display a QR Code in your view

``` xaml
xmlns:qr="clr-namespace:SimpleQR.MAUI;assembly=SimpleQR.MAUI"
```

``` xaml
<qr:QRCodeView
    Size="300"
    ForegroundColor="Black"
    BackgroundColor="White"
    Value="SimpleQR.MAUI" />
```

## Generate a QR Code in your code

``` cs
var imageSource = QRCode.GenerateQRCode(300, Colors.Black, Colors.White, "SimpleQR.MAUI");
```

#### Example

``` xaml
<Image
    x:Name="image"
    WidthRequest="300"
    HeightRequest="300" />
```

``` cs
this.image.Source = QRCode.GenerateQRCode(300, Colors.Black, Colors.White, "SimpleQR.MAUI");
```

#### Example

Could also convert the ImageSource to a file and save/export.
