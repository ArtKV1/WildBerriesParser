using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using System.Text;
using CsvHelper.TypeConversion;

namespace WildBerries2
{
    internal class FileSaver
    {
        public class ListToStringConverter<T> : ITypeConverter
        {
            public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
            {
                if (value is List<T> list)
                {
                    return string.Join("\n", list);
                }
                return null;
            }

            public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
            {
                return null;
            }
        }
        public static void SaveToCSV(List<Offer> parsedData)
        {
            string path = @"C:\Users\ArtKV\Videos\WildBerries.csv";
            using (var writer = new StreamWriter(path, true, Encoding.GetEncoding("windows-1251")))
            {
                var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                {
                    HasHeaderRecord = true,
                    Delimiter = ";"
                };
                using (var csv = new CsvWriter(writer, csvConfig))
                {
                    csv.WriteRecords(parsedData);
                }
            }
            Console.WriteLine("Файл успешно сохранён");
        }
    }
}
