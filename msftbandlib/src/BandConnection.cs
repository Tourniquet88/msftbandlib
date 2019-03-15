using System;
using System.Threading.Tasks;
using MSFTBandLib.Libs;

namespace MSFTBandLib {

/// <summary>
/// Microsoft Band connection class
/// </summary>
public class BandConnection<T> where T : BandSocket {

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
		this.Cargo = Activator.CreateInstance(typeof (T));
		this.Push = Activator.CreateInstance(typeof (T));
	}


	/// <summary>Connect to the Band on Cargo and Push.</summary>
	/// <returns>Task</returns>
	public async Task Connect() {
		await this.Cargo.connect(this.Band, this.Band.CARGO);
		await this.Push.connect(this.Band, this.Band.PUSH);
	}


	/// <summary>Disconnect all open band sockets.</summary>
	/// <returns>Task</returns>
	public async Task Disconnect() {
		await this.Cargo.disconnect();
		await this.Push.disconnect();
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