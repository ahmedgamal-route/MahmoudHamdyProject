namespace Store.Data.Entities.IDentityEntities
{
    public class Address
    {
        public long  Id { get; set; }
        public string FisrtName { get; set; }
        public string LastName { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string Postalcode { get; set; }
        public string AppUserId { get; set; }
        public AppUser appUser { get; set; }
    }
}