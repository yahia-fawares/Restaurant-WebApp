namespace MyRestaurant.Models
{
    public class JoinTables
    {
        public UserProduct Product { get; set; }
        public UserCustomer Customer { get; set; }
        public UserCategory Category { get; set; }
        public UserProductCustomer CustomerProduct { get; set; }

    }
}
