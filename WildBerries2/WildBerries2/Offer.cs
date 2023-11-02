using static WildBerries2.FileSaver;
using CsvHelper.Configuration.Attributes;

namespace WildBerries2
{
    public class Offer
    {
        [Name("Код продавца")]
        public long SellerCode { get; set; }

        [Name("Код группы")]
        public long GroupCode { get; set; }

        [Name("Код товара")]
        public long OfferCode { get; set; }

        [Name("Название")]
        public string Name { get; set; }

        [Name("Цвет")]
        public string Color { get; set; }

        [Name("Состав")]
        public string Сomposition { get; set; }

        [Name("Цена без скидки")]
        public long? OldPrice { get; set; }

        [Name("Процент скидка")]
        public long? Sale { get; set; }

        [Name("Цена со скидкой")]
        public long Price { get; set; }

        [Name("Рейтинг")]
        public long Rating { get; set; }

        [Name("Количество отзывов")]
        public long FeedbacksCount { get; set; }

        [Name("Бренд")]
        public string Brand { get; set; }

        [Name("Ссылка на товар")]
        public string LinkToOffer { get; set; }


        [Name("Описание")]
        public string Description { get; set; }

        [TypeConverter(typeof(ListToStringConverter<string>)), Name("Изображения")]
        public List<string> LinksToImages { get; set; }

        [Name("Дата обновления")]
        public string DateTime { get; set; }

    }
}
