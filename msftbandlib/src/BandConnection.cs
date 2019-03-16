using System;
using System.Threading.Tasks;

namespace MSFTBandLib {

/// <summary>
/// Microsoft Band connection class
/// </summary>
public class BandConnection<T> where T : class, BandSocket {

	/// <summary>Band instance</summary>
	private Band Band;

	/// <summary>Band main service socket</summary>
	private BandSocket Cargo;

	/// <summary>Band push service socket</summary>
	private BandSocket Push;


	/// <summary>
	/// Create a new connection to a given Band.
	/// 
	/// Socket instances are created for Cargo and Push using the 
	/// given socket type for the connection type, which must 
	/// implement `BandSocket`.
	/// </summary>
	/// <param name="band">Band instance</param>
	public BandConnection(Band band) {
		this.Band = band;
		this.Cargo = Activator.CreateInstance(typeof(T), new object[]{}) as T;
		this.Push = Activator.CreateInstance(typeof(T), new object[]{}) as T;
	}


	/// <summary>Connect to the Band on Cargo and Push.</summary>
	/// <returns>Task</returns>
	public async Task Connect() {
		await this.Cargo.Connect(this.Band, Band.CARGO.ToString());
		await this.Push.Connect(this.Band, Band.PUSH.ToString());
	}


	/// <summary>Disconnect all open band sockets.</summary>
	/// <returns>Task</returns>
	public async Task Disconnect() {
		await this.Cargo.Disconnect();
		await this.Push.Disconnect();
	}


	/// <summary>
	/// Get the Band instance.
	/// </summary>
	/// <returns>Band</returns>
	public Band GetBand() {
		return this.Band;
	}

}

}