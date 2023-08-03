using System;
using System.Collections.Generic;
//using System.Collections.Generic;

namespace Back_End_Capstone_MS.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Difficulty { get; set; }
        public string Name { get; set; }
        public string Instructions { get; set; }
        public string Ingredients { get; set; }
        public DateTime DateCreated { get; set; }
        public string ImageUrl { get; set; }
        public List<Comment> Comments { get; set; }
        public List<RecipeLike> RecipeLikes { get; set; }
    }
}
