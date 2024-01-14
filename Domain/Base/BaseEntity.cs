namespace Domain.Base
{
    public abstract class BaseEntity: ITrackCreationTime
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
