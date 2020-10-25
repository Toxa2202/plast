using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Reflection.Metadata;


namespace EPlast.BLL
{
    internal interface IPdfDocument
    {
        PdfSharp.Pdf.PdfDocument GetDocument();
    }
}