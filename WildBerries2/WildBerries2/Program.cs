using System.Text;
using WildBerries2;

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

List<Offer> offers = Parser.Parse();

FileSaver.SaveToCSV(offers);
