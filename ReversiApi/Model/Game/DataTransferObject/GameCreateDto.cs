namespace ReversiApi.Model.Game.DataTransferObject;

public class GameCreateDto
{
    public string? Description { get; init; }

    /// <summary>
    /// Determines if this DTO is valid configured.
    /// </summary>
    /// <returns>True if the data is valid.</returns>
    public bool ValidData()
    {
        return this.Description != null;
    }

}