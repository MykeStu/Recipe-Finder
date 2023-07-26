namespace Back_End_Capstone_MS.Models
{
    public class RecipeIngredient
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public string Amount { get; set; }
    }
}
