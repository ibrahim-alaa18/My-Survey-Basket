namespace MySurveyBasket.Entities
{
    public sealed class Poll:AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public int IsPublished { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }

        public ICollection<Question> Questions { get; set; } = [];
        public ICollection<Vote> Votes { get; set; } = [];
    }
}
