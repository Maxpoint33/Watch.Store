namespace Watch_Store.Models.Entities
{
    public class Watch
    {
        public int Id { get; set; }

        public string BrandName { get; set; }

        public string WatchModel { get; set; }

        public string ReferenceNumber { get; set; }

        public decimal Price { get; set; }

        public string imgUrl { get; set; }

    }
}
