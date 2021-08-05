namespace BuyHouse.Services.Properties.Models
{
    public class PropertyDetailsServiceModel : PropertyServiceListModel
    {
        public string Description { get; set; }

        public int AgentId { get; set; }

        public int? Floor { get; set; }

        public int? Floors { get; set; }

        public int? BedRoom { get; set; }

        public int? Bath { get; set; }

        public string UserId { get; set; }

        public string AgentName { get; set; }
    }
}
