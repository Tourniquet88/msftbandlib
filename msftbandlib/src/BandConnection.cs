using System;
using System.Threading.Tasks;
using MSFTBandLib.Libs;

namespace MSFTBandLib {

/// <summary>
/// Microsoft Band connection class
/// </summary>
public class BandConnection<T> where T : BandSocket {

	/// <summary>Band instance</summary>
	private Band band;

	/// <summary>Band main service socket</summary>
	private BandSocket socket;

	/// <summary>Band push service socket</summary>
	private BandSocket socketPush;


	/// <summary>
	/// Create a new connection to a given Band.
	/// 
	/// Socket instances are created for receive/push using the 
	/// given socket type for the connection type, which must 
	/// implement `BandSocket`.
	/// </summary>
	/// <param name="band">Band instance</param>
	public BandConnection(Band band) {

		/// Band MAC address
		this.band = band;

		/// Band main service socket
		this.socket = Activator.CreateInstance(typeof (T), new object[] {
			band.getMac(), Network.PORT_MAIN
		}) as T;

		/// Band push service socket
		this.socketPush = Activator.CreateInstance(typeof (T), new object[] {
			band.getMac(), Network.PORT_PUSH
		}) as T;

	}


	/// <summary>Connect all sockets.</summary>
	/// <returns>Task</returns>
	public async Task connect() {
		await this.socket.connect();
		await this.socketPush.connect();
	}


	/// <summary>Disconnect all sockets.</summary>
	/// <returns>Task</returns>
	public async Task disconnect() {
		await this.socket.disconnect();
		await this.socketPush.disconnect();
	}


	/// <summary>
	/// Get the Band instance.
	/// </summary>
	/// <returns>Band</returns>
	public Band getBand() {
		return this.band;
	}

}

}