namespace WebConnectMongoDBAndSQL.Model
{
    public class Address
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Foreign key
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
