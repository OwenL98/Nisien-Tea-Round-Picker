namespace Common.Facades;

public class RandomFacade : IRandomFacade
{
    public int GetRandomNumber(int maxValue)
    {
        var randomNumber = new Random().Next(minValue: 0, maxValue: maxValue);
        
        return randomNumber;
    }
}