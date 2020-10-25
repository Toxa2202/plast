using EPlast.BLL.ExtensionMethods;
using EPlast.DataAccess.Entities;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

using PdfSharp.Pdf.IO;

namespace EPlast.BLL
{
    public class DecisionDocument : PdfDocument
    {
        private readonly Decesion decesion;

        public DecisionDocument(Decesion decesion) : this(decesion, new PdfSettings())
        {
        }

        public DecisionDocument(Decesion decesion, IPdfSettings settings) : base(settings)
        {
            this.decesion = decesion;
        }

        public override void SetDocumentBody(PdfPage page)
        {
            XGraphics gfx = XGraphics.FromPdfPage(page);
            DrawTitle(page, gfx, "Text");
            DrawText(gfx, 1);
            DrawTextAlignment(gfx, 2);
            MeasureText(gfx, 3);
            //var paragraph = section.AddParagraph($"{decesion.Name} від {decesion.Date:dd/MM/yyyy}");

            //paragraph.Format = new ParagraphFormat
            //{
            //    Font = new Font
            //    {
            //        Size = 14
            //    },
            //    SpaceAfter = "3cm",
            //    SpaceBefore = "5cm",
            //    Alignment = ParagraphAlignment.Right
            //};

            //paragraph = section.AddParagraph(decesion.Description);
            //paragraph.Format = new ParagraphFormat
            //{
            //    Font = new Font
            //    {
            //        Size = 12
            //    },
            //    SpaceAfter = "1cm",
            //};

            //paragraph = section.AddParagraph($"Поточний статус: {decesion.DecesionStatusType.GetDescription()}");
            //paragraph.Format = new ParagraphFormat
            //{
            //    Font = new Font
            //    {
            //        Size = 14
            //    },
            //    SpaceBefore = "5cm",
            //    Alignment = ParagraphAlignment.Right
            //};
        }
    }
}