using System.Threading.Tasks;

namespace MSFTBandLib {

/// <summary>
/// Band socket base implementation
/// </summary>
public abstract class BandSocket : BandSocketInterface {

	/// <summary>Socket port</summary>
	protected int port;

	/// <summary>Device MAC address</summary>
	protected string address;


	/// <summary>Construct a new socket.</summary>
	/// <param name="address">Device MAC address</param>
	/// <param name="port">Socket port</param>
	public BandSocket(string address, int port) {
		this.port = port;
		this.address = address;
	}


	/// <summary>Connect to the device (open socket).</summary>
	public abstract Task connect();


	/// <summary>Close the connection socket.</summary>
	public abstract Task disconnect();


	/// <summary>Receive bytes up to a specified buffer size.</summary>
	/// <param name="buffer">Buffer size to use</param>
	/// <returns>Task<byte[]></returns>
	public abstract Task<byte[]> receive(int buffer);


	/// <summary>Send bytes to the device.</summary>
	/// <param name="packet">Bytes to send</param>
	/// <returns>Task</returns>
	public abstract Task send(byte[] packet);

}

}