namespace ContosoPizza.Models
{
    public class QueryParameters
    {
        public string ? Ingredient { get; set; }
        public bool ? IsGlutenFree { get; set; }
        public string ? Name { get; set; }
        public int ? Page { get; set; }
        public int ? MaxItems { get; set; }


    }
}
