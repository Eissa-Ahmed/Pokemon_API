namespace Pokemon.Model.ModelsDTO
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Rate { get; set; }
        public int ReviewerId { get; set; }
        public int PokemonId { get; set; }
    }
}
