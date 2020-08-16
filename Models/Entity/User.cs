using System;

namespace Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime AddedDate { get; set; }
        public byte UserType { get; set; }
        public string QRCode { get; set; }
    }
}
