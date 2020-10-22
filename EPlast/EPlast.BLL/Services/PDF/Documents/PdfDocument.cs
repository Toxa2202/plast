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
            SetDocumentBody(section);

            return document;
        }

        public abstract void SetDocumentBody(Section section);

        public virtual void DefineStyles(PdfSharp.Pdf.PdfDocument document)
        {
            //var style = document.[settings.StyleName];
            //style.Font.Name = settings.FontName;
        }
    }
}