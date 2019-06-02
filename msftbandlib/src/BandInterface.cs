using MSFTBandLib.Command;
using MSFTBandLib.Libs;
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

    /// <summary>
    /// Send command to the device and get response bytes.
    /// </summary>
    /// <param name="command">Command</param>
    /// <param name="args">Arguments to send</param>
    /// <param name="buffer">Receiving buffer size</param>
    /// <returns>Task<byte[]></returns>
    Task<byte[]> Command(
        Command.Command command,
        byte[] args=null, int buffer=Network.BUFFER_SIZE
    );

}

}