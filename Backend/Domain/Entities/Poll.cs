namespace Domain.Entities
{
    public class Poll
    {
        public string Id { get; set; }
        public string Question { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsClosed { get; set; }
        public DateTime EndTime { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual User Creator { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
