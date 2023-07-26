﻿using Microsoft.Identity.Client;
using System;

namespace Back_End_Capstone_MS.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RecipeId { get; set; }
        public string Message { get; set; }
        public DateTime DateCreated { get; set; }
        public int Likes { get; set; }
    }
}
