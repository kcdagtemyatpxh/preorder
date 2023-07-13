namespace Ef7FirstLook.Catalog.Header.Dtos
{
    public class HeaderCreateRequest
    {
        public string name { get; set; }

        public string address { get; set; }

        public DateTime deliveryDate { get; set; }
    }
}
