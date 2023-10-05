namespace FortuneCookie.Logic.Abstraction;

public interface IPredictionService
{
    Task<string> GetPrediction();
}