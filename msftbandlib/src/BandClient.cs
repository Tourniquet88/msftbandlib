using System.Threading.Tasks;

namespace MSFTBandLib {

/// <summary>
/// Band client interface
/// </summary>
/// <typeparam name="T">Band connection socket</typeparam>
public interface BandClient<T> where T : class, BandSocket {

    /// <summary>
    /// Get a connection to a given Band instance.
    /// </summary>
    /// <param name="band">Band instance</param>
    /// <returns>BandConnection<T></returns>
    Task<BandConnection<T>> GetConnection(Band band);

    /// <summary>
    /// Get an array of all available paired Bands which can be connected to.
    /// </summary>
    /// <returns>Band[]</returns>
    Task<Band[]> GetPairedBands();

}

}