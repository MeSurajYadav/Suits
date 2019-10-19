namespace Server.Models
{
    public class HollydayTeam
    {
        public int Id { get; set; }

        //->
        public int TeamId { get; set; }
        public int HollydayId { get; set; }
        public Hollyday Hollyday { get; set; }
        public Team Team { get; set; }
    }
}