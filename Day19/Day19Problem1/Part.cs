namespace Day19Problem1
{
    public class Part
    {
        public Dictionary<char, long> Ratings { get; init; }
        public RuleStepResult FinalResult { get; set; }
        public long RatingSum
        {
            get
            {
                return Ratings.Values.Sum();
            }
        }

        public Part(Dictionary<char, long> ratings)
        {
            Ratings = ratings;
        }
    }
}
