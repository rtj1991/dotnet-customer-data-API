

using customer_data_webAPI.Models;

public class DistanceCalculationContainer : IDistanceCalculationContainer
{
    private readonly CustomerDBContext _DBContext;
    public DistanceCalculationContainer(CustomerDBContext DBContext)
    {
        this._DBContext = DBContext;

    }
    public async Task<double> GetDistance(int id, double longitude, double latitude)
    {
        try
        {
            var _csutomer = await _DBContext.Customers.FindAsync(id);

            if (_csutomer != null)
            {
                double rlat1 = Math.PI * latitude / 180;
                double rlat2 = (double)(Math.PI * _csutomer.Latitude / 180);

                double theta = (double)(longitude - _csutomer.Longitude);

                double rtheta = Math.PI * theta / 180;
                double dist =
                    Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                    Math.Cos(rlat2) * Math.Cos(rtheta);
                dist = Math.Acos(dist);
                dist = dist * 180 / Math.PI;
                dist = dist * 60 * 1.1515;

                return dist * 1.609344;
            }
            throw new NullReferenceException("Getting Null while fetching Customer details");

        }
        catch (ArgumentNullException e)
        {

            throw new ArgumentNullException("Getting Error while Argument Pass " + e.Message);
        }


    }
}