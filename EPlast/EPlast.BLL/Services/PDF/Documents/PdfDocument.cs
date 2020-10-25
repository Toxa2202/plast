using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

using PdfSharp.Pdf.IO;
using System;

namespace EPlast.BLL
{
    public abstract class PdfDocument : IPdfDocument
    {
        protected  PdfSharp.Pdf.PdfDocument document;
        private XGraphicsState state;
        private readonly IPdfSettings settings;

        protected PdfDocument() : this(new PdfSettings())
        {
        }

        protected PdfDocument(IPdfSettings settings)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
            document = new PdfSharp.Pdf.PdfDocument();
        }
       protected  void DrawImage(XGraphics gfx, string jpegSamplePath, int x, int y, int width, int height)
        {
            XImage image = XImage.FromFile(jpegSamplePath);
            gfx.DrawImage(image, x, y, width, height);
        }
       protected void DrawText(XGraphics gfx, int number)
       {
           BeginBox(gfx, number, "Text Styles");
           const string facename = "Times New Roman";
//XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always);

           XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.WinAnsi, PdfFontEmbedding.Default);
           XFont fontRegular = new XFont(facename, 20, XFontStyle.Regular, options);
           XFont fontBold = new XFont(facename, 20, XFontStyle.Bold, options);
           XFont fontItalic = new XFont(facename, 20, XFontStyle.Italic, options);
           XFont fontBoldItalic = new XFont(facename, 20, XFontStyle.BoldItalic, options);
// The default alignment is baseline left (that differs from GDI+)
           gfx.DrawString("Times (regular)", fontRegular, XBrushes.DarkSlateGray, 0, 30);
           gfx.DrawString("Times (bold)", fontBold, XBrushes.DarkSlateGray, 0, 65);
           gfx.DrawString("Times (italic)", fontItalic, XBrushes.DarkSlateGray, 0, 100);
           gfx.DrawString("Times (bold italic)", fontBoldItalic, XBrushes.DarkSlateGray, 0, 135);
           EndBox(gfx);
       }
       protected void DrawTextAlignment(XGraphics gfx, int number)
       {
           BeginBox(gfx, number, "Text Alignment");
           XRect rect = new XRect(0, 0, 250, 140);
           XFont font = new XFont("Verdana", 10);
           XBrush brush = XBrushes.Purple;
           XStringFormat format = new XStringFormat();
           gfx.DrawRectangle(XPens.YellowGreen, rect);
           gfx.DrawLine(XPens.YellowGreen, rect.Width / 2, 0, rect.Width / 2, rect.Height);
           gfx.DrawLine(XPens.YellowGreen, 0, rect.Height / 2, rect.Width, rect.Height / 2);
           gfx.DrawString("TopLeft", font, brush, rect, format);
           format.Alignment = XStringAlignment.Center;
           gfx.DrawString("TopCenter", font, brush, rect, format);
           format.Alignment = XStringAlignment.Far;
           gfx.DrawString("TopRight", font, brush, rect, format);
           format.LineAlignment = XLineAlignment.Center;
           format.Alignment = XStringAlignment.Near;
           gfx.DrawString("CenterLeft", font, brush, rect, format);
           format.Alignment = XStringAlignment.Center;
           gfx.DrawString("Center", font, brush, rect, format);
           format.Alignment = XStringAlignment.Far;
           gfx.DrawString("CenterRight", font, brush, rect, format);
           format.LineAlignment = XLineAlignment.Far;
           format.Alignment = XStringAlignment.Near;
           gfx.DrawString("BottomLeft", font, brush, rect, format);
           format.Alignment = XStringAlignment.Center;
           gfx.DrawString("BottomCenter", font, brush, rect, format);
           format.Alignment = XStringAlignment.Far;
           gfx.DrawString("BottomRight", font, brush, rect, format);
           EndBox(gfx);
      
       }
       protected void MeasureText(XGraphics gfx, int number)
       {
           BeginBox(gfx, number, "Measure Text");
           const XFontStyle style = XFontStyle.Regular;
           XFont font = new XFont("Times New Roman", 95, style);
           const string text = "Hallo";
           const double x = 20, y = 100;
           XSize size = gfx.MeasureString(text, font);
           double lineSpace = font.GetHeight(gfx);
           int cellSpace = font.FontFamily.GetLineSpacing(style);
           int cellAscent = font.FontFamily.GetCellAscent(style);
           int cellDescent = font.FontFamily.GetCellDescent(style);
           int cellLeading = cellSpace - cellAscent - cellDescent;
// Get effective ascent
           double ascent = lineSpace * cellAscent / cellSpace;
           gfx.DrawRectangle(XBrushes.Bisque, x, y - ascent, size.Width, ascent);
// Get effective descent
           double descent = lineSpace * cellDescent / cellSpace;
           gfx.DrawRectangle(XBrushes.LightGreen, x, y, size.Width, descent);
// Get effective leading
           double leading = lineSpace * cellLeading / cellSpace;
           gfx.DrawRectangle(XBrushes.Yellow, x, y + descent, size.Width, leading);
// Draw text half transparent
               XColor color = XColors.DarkSlateBlue;
           color.A = 0.6;
           gfx.DrawString(text, font, new XSolidBrush(color), x, y);
           EndBox(gfx);
       }
    public void BeginBox(XGraphics gfx, int number, string title)
       {
           const int dEllipse = 15;
           XRect rect = new XRect(0, 20, 300, 200);
           if (number % 2 == 0)
               rect.X = 300 - 5;
           rect.Y = 40 + ((number - 1) / 2) * (200 - 5);
           rect.Inflate(-10, -10);
           XRect rect2 = rect;
           rect2.Offset(0, 300);
           gfx.DrawRoundedRectangle(new XSolidBrush(XColor.FromName("orange")), rect2, new XSize(dEllipse + 8, dEllipse + 8));
           XLinearGradientBrush brush = new XLinearGradientBrush(rect, XColor.FromName("blue"), XColor.FromName("yellow"), XLinearGradientMode.Vertical);
           gfx.DrawRoundedRectangle(new XPen(XColor.FromName("red")), brush, rect, new XSize(dEllipse, dEllipse));
           rect.Inflate(-5, -5);
           XFont font = new XFont("Verdana", 12, XFontStyle.Regular);
           gfx.DrawString(title, font, XBrushes.Navy, rect, XStringFormats.TopCenter);
           rect.Inflate(-10, -5);
           rect.Y += 20;
           rect.Height -= 20;
           this.state = gfx.Save();
           gfx.TranslateTransform(rect.X, rect.Y);
    
       }
       public void EndBox(XGraphics gfx)
       {
           gfx.Restore(this.state);
       }

       public void DrawTitle(PdfPage page, XGraphics gfx, string title)
       {
           XRect rect = new XRect(new XPoint(), gfx.PageSize);
           rect.Inflate(-10, -15);
           XFont font = new XFont("Verdana", 14, XFontStyle.Bold);
           gfx.DrawString(title, font, XBrushes.MidnightBlue, rect, XStringFormats.TopCenter);
           rect.Offset(0, 5);
           font = new XFont("Verdana", 8, XFontStyle.Italic);
           XStringFormat format = new XStringFormat();
           format.Alignment = XStringAlignment.Near;
           format.LineAlignment = XLineAlignment.Far;
           gfx.DrawString("Created with " + PdfSharp.ProductVersionInfo.Producer, font, XBrushes.DarkOrchid, rect, format);
           font = new XFont("Verdana", 8);
           format.Alignment = XStringAlignment.Center;
           gfx.DrawString(document.PageCount.ToString(), font, XBrushes.DarkOrchid, rect, format);
           document.Outlines.Add(title, page, true);
       }
    public virtual PdfSharp.Pdf.PdfDocument GetDocument()
        {
            PdfPage page = document.AddPage();
            document.Info.Title = settings.Title;
            document.Info.Subject = settings.Subject;
            document.Info.Author = settings.Author;

            DefineStyles(document);

            XGraphics gfx = XGraphics.FromPdfPage(page);

            if (!settings.ImagePath.Contains("Blank"))
            {
                string base64 = "base64:" + settings.ImagePath.Split(',')[1];
                DrawImage(gfx, base64, 50, 50, 600, 250);
                //image.Width = 600;
                //image.RelativeHorizontal = RelativeHorizontal.Page;
                //image.RelativeVertical = RelativeVertical.Page;
            }
            else
            {
                DrawImage(gfx, settings.ImagePath, 40, 20, 84, 250);


                //Image image = section.AddImage(settings.ImagePath);
                //image.Width = 84;
                //image.Left = 40;
                //image.Top = 20;
                //image.RelativeHorizontal = RelativeHorizontal.Page;
                //image.RelativeVertical = RelativeVertical.Page;
            }
            SetDocumentBody(page);

            return document;
        }

        public abstract void SetDocumentBody(PdfPage page);

        public virtual void DefineStyles(PdfSharp.Pdf.PdfDocument document)
        {
            //var style = document.[settings.StyleName];
            //style.Font.Name = settings.FontName;
        }
    }
}