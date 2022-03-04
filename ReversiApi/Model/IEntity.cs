namespace ReversiApi.Model;

public interface IEntity
{
    public const int IdUndefined = 0;
    
    int Id { get; set; }
}