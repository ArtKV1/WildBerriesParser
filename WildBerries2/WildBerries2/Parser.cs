using Leaf.xNet;

namespace WildBerries2
{
    static class Parser
    {
        public static List<Offer> Parse()
        {
            HttpRequest request = new HttpRequest();

            /*return ParseCatalogs(request);*/
            
            return ParseSellers(request);

        }

        private static List<Offer> ParseCatalogs(HttpRequest request)
        {
            var catalogs = new List<string>
            {
                "https://catalog.wb.ru/catalog/housecraft8/catalog?TestGroup=pi_100&TestID=348&appType=1&cat=128301&curr=rub&dest=12358075&spp=33&uclusters=1",
                "https://catalog.wb.ru/catalog/jeans/catalog?TestGroup=pi_100&TestID=348&appType=1&cat=8131&curr=rub&dest=12358075&spp=33&uclusters=1",
                "https://catalog.wb.ru/catalog/electronic22/catalog?TestGroup=pi_100&TestID=348&appType=1&curr=rub&dest=12358075&sort=popular&spp=33&subject=515&uclusters=1"
            };

            var offers = new List<Offer>();

            foreach (var catalog in catalogs) 
            {
                offers.AddRange(ParseIdsFromPages(catalog, request));
            }

            Console.WriteLine($"Общее количество товаров: {offers.Count}");
            return offers;
        }

        private static List<Offer> ParseSellers(HttpRequest request)
        {

            var page = "https://catalog.wb.ru/sellers/catalog?TestGroup=pi_100&TestID=348&appType=1&curr=rub&dest=12358075&sort=popular&spp=33&uclusters=1&supplier=";
            var offers = new List<Offer>();

            List<long> sellersIds = new List<long>
            {
                682244,
                122724,
                1180826,
                86075,
                694197,
                68688
            };

            foreach (long id in sellersIds) 
            {
                DateTime currentTime = DateTime.Now;
                Console.WriteLine($"Начали парсить продавца: {id}, время: " + currentTime.ToString("HH:mm:ss"));
                var sellerPage = page + id;
                var pageForTotalOffers = "https://catalog.wb.ru/sellers/v4/filters?TestGroup=pi_100&TestID=348&appType=1&curr=rub&dest=12358075&spp=33&uclusters=1&supplier=" + id;
                offers.AddRange(ParseIdsFromPages(sellerPage, request));
            }

            Console.WriteLine($"Общее количество товаров: {offers.Count}");
            return offers;
        }

        private static List<Offer> ParseIdsFromPages(string page, HttpRequest request)
        {
            var offersId = new List<long>();

            DateTime currentTime = DateTime.Now;
            Console.WriteLine("Парсим айди товаров, время: " + currentTime.ToString("HH:mm:ss"));

            int i = 1;

            while(true)
            {
                Thread.Sleep(100);

                currentTime = DateTime.Now;
                Console.WriteLine($"Перешли на {i} страницу, время: " + currentTime.ToString("HH:mm:ss"));

                var selectedPage = page + $"&page={i}";
                var response = request.Get(selectedPage).ToString();
                var ids = GetterOffersAndSellersIdsFromJson.FromJson(response)?.Data.Products.Select(x => x.Id).ToList() ?? new List<long>();

                if (ids.Count == 0)
                {
                    currentTime = DateTime.Now;
                    Console.WriteLine($"На странице {i} нет товаров, время: " + currentTime.ToString("HH:mm:ss"));
                    break;
                }

                i++;

                offersId.AddRange(ids);
            }

            var offers = new List<Offer>();
            int counter = 1;

            foreach (var id in offersId) 
            {
                currentTime = DateTime.Now;
                Console.WriteLine($"Парсим товар: {id}, время:" + currentTime.ToString("HH:mm:ss") + $", номер товара: {counter}");
                offers.Add(ParseOffer(id, request));
                if (offers.Count == 10)
                    break;
                counter++;
            }

            return offers;
        }

        private static Offer ParseOffer(long productId, HttpRequest request)
        {
            var pageForOfferBasket = GetPageToBasket(productId);
            var pageForCardJson = pageForOfferBasket + "/info/ru/card.json";
            var pageForCardDetail = $"https://card.wb.ru/cards/v1/detail?spp=30&nm={productId}";

            var response = request.Get(pageForCardJson).ToString();

            GetterProductCardFromJson getterProductInfoFromJson = GetterProductCardFromJson.FromJson(response);

            response = request.Get(pageForCardDetail).ToString();

            GetterProductCartDetailFromJson getterProductCartDetailFromJson = GetterProductCartDetailFromJson.FromJson(response);

            var sellercode = getterProductInfoFromJson.Selling.SupplierId;
            var groupCode = getterProductInfoFromJson.ImtId;
            var offerCode = productId;
            var name = getterProductCartDetailFromJson.Data.Products[0].Name;
            var color = getterProductInfoFromJson.NmColorsNames;
            var composition = string.Join(", ", getterProductInfoFromJson.Compositions?.Select(name => name.Name) ?? Array.Empty<string>());
            var oldprice = getterProductCartDetailFromJson.Data.Products[0].PriceU / 100;
            var sale = getterProductCartDetailFromJson.Data.Products[0].Sale;
            var price = getterProductCartDetailFromJson.Data.Products[0].SalePriceU / 100;
            var rating = getterProductCartDetailFromJson.Data.Products[0].Rating;
            var feedbacksCount = getterProductCartDetailFromJson.Data.Products[0].Feedbacks;
            var brand = getterProductInfoFromJson.Selling.BrandName;
            var linkToOffer = $"https://www.wildberries.ru/catalog/{productId}/detail.aspx";
            var description = getterProductInfoFromJson.Description;
            var linksToImages = GetLinksToImages(getterProductCartDetailFromJson.Data.Products[0].Pics, productId, pageForOfferBasket);
            DateTime currentTime = DateTime.Now;
            var datetime = currentTime.ToString("dd.MM.yyyy HH:mm:ss");


            Offer offer = new Offer
            {
                SellerCode = sellercode,
                GroupCode = groupCode,
                OfferCode = offerCode,
                Name = name,
                Color = color,
                Сomposition = composition,
                OldPrice = oldprice,
                Sale = sale,
                Price = price,
                Rating = rating,
                FeedbacksCount = feedbacksCount,
                Brand = brand,
                LinkToOffer = linkToOffer,
                Description = description,
                LinksToImages = linksToImages,
                DateTime = datetime
            };

            return offer;
        }

        private static string GetPageToBasket(long productId)
        {

            var tempForProductId = productId.ToString();
            var vol = Math.Floor(productId / Math.Pow(10, 5));
            var part = Math.Floor(productId / Math.Pow(10, 3));
            var basket = "";

            if (vol >= 0 && vol <= 143)
            {
                basket = "01";
            }
            else if (vol >= 144 && vol <= 287)
            {
                basket = "02";
            }
            else if (vol >= 288 && vol <= 431)
            {
                basket = "03";
            }
            else if (vol >= 432 && vol <= 719)
            {
                basket = "04";
            }
            else if (vol >= 720 && vol <= 1007)
            {
                basket = "05";
            }
            else if (vol >= 1008 && vol <= 1061)
            {
                basket = "06";
            }
            else if (vol >= 1062 && vol <= 1115)
            {
                basket = "07";
            }
            else if (vol >= 1116 && vol <= 1169)
            {
                basket = "08";
            }
            else if (vol >= 1170 && vol <= 1313)
            {
                basket = "09";
            }
            else if (vol >= 1314 && vol <= 1601)
            {
                basket = "10";
            }
            else if (vol >= 1602 && vol <= 1655)
            {
                basket = "11";
            }
            else if (vol >= 1656 && vol <= 1919)
            {
                basket = "12";
            }
            else if (vol >= 1920 && vol <= 2045)
            {
                basket = "13";
            }

            var page = $"https://basket-{basket}.wb.ru/vol{vol}/part{part}/{productId}";

            return page;
        }

        private static List<string> GetLinksToImages(long picsCount, long productId, string page)
        {
            var linksToImages = new List<string>();
            for (int i = 1; i <= picsCount; i++)
            {
                linksToImages.Add($"{page}/images/big/{i}.webp");
            }
            return linksToImages;
        }
    }
}
