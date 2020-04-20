
namespace httpgrpc.services.restaurants.Command
{
    public class AddRestaurantCommand
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
