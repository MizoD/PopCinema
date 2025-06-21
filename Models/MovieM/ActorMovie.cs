﻿namespace PopCinema.Models.MovieM
{
    public class ActorMovie
    {
        public int ActorId { get; set; }
        public Actor Actor { get; set; } = null!;
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

    }
}
