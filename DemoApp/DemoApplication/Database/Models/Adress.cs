using DemoApplication.Database.Models.Common;

namespace DemoApplication.Database.Models
{
    public class Adress : BaseEntity<int>, IAuditable
    {
        public string AcseptorName { get; set; }
        public string AcseptorLastname { get; set; }
        public string AdressName { get; set; }

        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

    }
}
