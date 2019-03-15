using System.Threading.Tasks;

namespace MSFTBandLib {

/// <summary>
/// Band socket interface
/// </summary>
interface BandSocket {

	/// <summary>Connect to a Band.</summary>
	/// <param name="band">Band instance</param>
	/// <param name="service">Service UUID</param>
	/// <returns>Task</returns>
	Task Connect(Band band, string service);

	/// <summary>Close the connection.</summary>
	/// <returns>Task</returns>
	Task Disconnect();

	/// <summary>Receive bytes up to a specified buffer size.</summary>
	/// <param name="buffer">Buffer size to use</param>
	/// <returns>Task<byte[]></returns>
	Task<byte[]> Receive(int buffer);

	/// <summary>Send bytes to the device.</summary>
	/// <param name="packet">Bytes to send</param>
	/// <returns>Task</returns>
	Task Send(byte[] packet);

}

}