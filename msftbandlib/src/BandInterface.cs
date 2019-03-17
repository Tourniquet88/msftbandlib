using System;
using System.Threading.Tasks;

namespace MSFTBandLib {

/// <summary>
/// Microsoft Band connection interface
/// </summary>
public interface BandInterface {

    /// <summary>
    /// Create a new connection to a given Band.
    /// </summary>
    /// <param name="band">Band instance</param>
    /// <returns>Task</returns>
    Task Connect(Band band);

    /// <summary>
    /// Disconnect from the Band if connected.
    /// </summary>
    Task Disconnect();

}

}