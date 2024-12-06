using FluentResults;

namespace BoardGame.Infrastractures;

public class DataNotFoundError : Error
{
    public DataNotFoundError()
        : base("Data not found!")
    {
        Metadata.Add("ErrorCode", "1011");
    }
}
