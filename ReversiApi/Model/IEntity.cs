namespace ReversiApi.Model;

public interface IEntity
{
    public const int IdUndefined = -1;
    
    int Id { get; set; }
}