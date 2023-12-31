﻿using Pokemon.Model.Models;

namespace Pokemon.Model.IRpo
{
    public interface IReviewRepo
    {
        IEnumerable<Review> GetAll();
        IEnumerable<Review> GetReviewByPokemon(int id);
        Review GetById(int id);
        bool ReviewExist(int id);

        void CreateReview(Review model);
        void DeleteReview(int id);
        void UpdateReview(int id, Review model);

    }
}
