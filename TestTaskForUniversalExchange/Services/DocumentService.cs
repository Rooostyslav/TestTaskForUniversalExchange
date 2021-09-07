using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;
using TestTaskForUniversalExchange.EF;
using TestTaskForUniversalExchange.Models;

namespace TestTaskForUniversalExchange.Services
{
	public class DocumentService : IDocumentService
	{
        private readonly UniversalExchangeContext _context;

        public DocumentService(UniversalExchangeContext context)
		{
            _context = context;
		}

        public async Task<FileContentResult> GenerateDocument()
        {
            var applications = await _context.Applications.ToListAsync();

            using (MemoryStream mem = new MemoryStream())
            {
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(mem,
                    DocumentFormat.OpenXml.WordprocessingDocumentType.Document,
                    true))
                {
                    wordDoc.AddMainDocumentPart();

                    Document doc = new Document();
                    Body body = new Body();

                    var titleParagraph = CreateTitleParagraph();
                    body.Append(titleParagraph);

                    foreach (var application in applications)
					{
                        var paragraph = CreateApplicationParagraph(application);
                        body.Append(paragraph);
                    }

                    doc.Append(body); //add body to document

                    wordDoc.MainDocumentPart.Document = doc;
                    wordDoc.Close();
                }

                return new FileContentResult(mem.ToArray(),
                    "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            }
        }

        private Paragraph CreateApplicationParagraph(Application application)
        {
            Paragraph paragraph = new Paragraph();

            ParagraphProperties paragraphProperties = new ParagraphProperties();
            ParagraphStyleId paragraphStyleId = new ParagraphStyleId()
            {
                Val = "Bold"
            };
            Justification justification = new Justification()
            {
                Val = JustificationValues.Start
            };
            ParagraphMarkRunProperties paragraphMarkRunProperties = new ParagraphMarkRunProperties();

            paragraphProperties.Append(paragraphStyleId);
            paragraphProperties.Append(justification);
            paragraphProperties.Append(paragraphMarkRunProperties);

            paragraph.Append(paragraphProperties);

            paragraph.Append(GetBoldText("Фамилия Имя:"));
            paragraph.Append(GetSpace());
            paragraph.Append(GetText($"{application.FullName}"));
            paragraph.Append(GetWrapping());

            paragraph.Append(GetBoldText("Возраст:"));
            paragraph.Append(GetSpace());
            paragraph.Append(GetText($"{application.Age}"));
            paragraph.Append(GetWrapping());

            paragraph.Append(GetBoldText("Опыт:"));
            paragraph.Append(GetSpace());
            paragraph.Append(GetText($"{application.Experience}"));
            paragraph.Append(GetWrapping());

            paragraph.Append(GetBoldText("Електронная почта:"));
            paragraph.Append(GetSpace());
            paragraph.Append(GetText($"{application.Email}"));
            paragraph.Append(GetWrapping());

            paragraph.Append(GetBoldText("Мобильный телефон:"));
            paragraph.Append(GetSpace());
            paragraph.Append(GetText($"{application.MobilePhone}"));
            paragraph.Append(GetWrapping());

            if (application.Notes != null)
            {
                paragraph.Append(GetBoldText("Заметки:"));
                paragraph.Append(GetSpace());
                paragraph.Append(GetText($"{application.Notes}"));
                paragraph.Append(GetWrapping());
            }

            paragraph.Append(GetText("---------------------------------------"));
            paragraph.Append(GetWrapping());

            return paragraph;
        }

        private Run GetWrapping()
		{
            Run run = new Run();
            run.AppendChild(new Break());
            return run;
        }

        private Run GetSpace()
		{
            Run run = new Run();
            run.AppendChild(new Text(" "));
            return run;
        }

        private Run GetText(string text)
        {
            Run run = new Run();
            run.AppendChild(new Text(text));
            return run;
        }

        private Run GetBoldText(string text)
		{
            Run runBold = new Run();
            RunProperties runBoldProperties = new RunProperties();
            runBoldProperties.Append(new Bold());
            runBold.RunProperties = runBoldProperties;
            runBold.AppendChild(new Text(text));
            return runBold;
        }

        private Paragraph CreateTitleParagraph()
        {
            Paragraph paragraph = new Paragraph();

            ParagraphProperties paragraphProperties = new ParagraphProperties();
            ParagraphStyleId paragraphStyleId = new ParagraphStyleId()
            {
                Val = "Normal"
            };
            Justification justification = new Justification()
            {
                Val = JustificationValues.Center
            };
            ParagraphMarkRunProperties paragraphMarkRunProperties = new ParagraphMarkRunProperties();

            paragraphProperties.Append(paragraphStyleId);
            paragraphProperties.Append(justification);
            paragraphProperties.Append(paragraphMarkRunProperties);

            Run run = new Run();
            RunProperties runProperties = new RunProperties();

            Text text = new Text()
            {
                Text = "Все заявки!"
            };

            run.Append(runProperties);
            run.Append(text);
            paragraph.Append(paragraphProperties);
            paragraph.Append(run);

            return paragraph;
        }
    }
}
