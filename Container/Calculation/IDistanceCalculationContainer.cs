public interface IDistanceCalculationContainer{
    Task<double> GetDistance(int id, double longitude, double latitude);
}