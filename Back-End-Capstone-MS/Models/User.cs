using System;
namespace Back_End_Capstone_MS.Models
{
    public class User
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string FireBaseUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Admin { get; set; } = false;
        public bool Active { get; set; } = true;
    }
}
