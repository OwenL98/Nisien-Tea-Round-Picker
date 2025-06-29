namespace Common.Facades;

public class RandomFacade : IRandomFacade
{
    public int GetRandomNumber(int maxValue)
    {
        const int minValue = 0;
        var randomNumber = new Random().Next(
            minValue: minValue, 
            maxValue: maxValue);
        
        return randomNumber;
    }
}