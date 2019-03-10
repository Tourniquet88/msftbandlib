using System.Threading.Tasks;

namespace MSFTBandLib {

/// <summary>
/// Band socket interface
/// </summary>
interface BandSocketInterface {

	/// <summary>Receive bytes up to a specified buffer size.</summary>
	/// <param name="buffer">Buffer size to use</param>
	/// <returns>Task<byte[]></returns>
	Task<byte[]> receive(int buffer);

	/// <summary>Send bytes to the device.</summary>
	/// <param name="packet">Bytes to send</param>
	/// <returns>Task</returns>
	Task send(byte[] packet);

}

}