using System;
using System.Drawing;
using System.Drawing.Imaging;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace QRCode_Generator.Helper
{
    public static class QRCodeHelper
    {
        public static string TextToQrCode(string texto)
        {
            var writer = new ZXing.BarcodeWriterPixelData
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Width = 180,
                    Height = 180,
                    Margin = 0,
                    QrVersion = 10,
                    ErrorCorrection = ErrorCorrectionLevel.L
                }
            };
            var pixelData = writer.Write(texto);
            using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppRgb))
            {
                using (var ms = new System.IO.MemoryStream())
                {
                    var bitmapData = bitmap.LockBits(new Rectangle(0, 0, pixelData.Width, pixelData.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);
                    try
                    {
                        System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                    }
                    finally
                    {
                        bitmap.UnlockBits(bitmapData);
                    }

                    bitmap.Save(ms, ImageFormat.Png);
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
    }
}
