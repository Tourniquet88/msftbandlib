using MSFTBandLib.Libs;
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

    /// <summary>Read data from the device.</summary>
    /// <param name="command">Command</param>
    /// <param name="ResponseSize">Expected response size</param>
    /// <param name="args">Arguments to sends</param>
    /// <returns>Task<byte[]></returns>
    Task<byte[]> Read(
        Command command, int ResponseSize=0, byte[] args=null
    );

}

}