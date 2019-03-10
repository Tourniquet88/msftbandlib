namespace MSFTBandLib {

/// <summary>
/// Microsoft Band device class
/// </summary>
public class Band {

	/// <summary>MAC address</summary>
	private string mac;


	/// <summary>Construct a new device instance.</summary>
	/// <param name="mac">MAC address</param>
	public Band(string mac) {
		this.mac = mac;
	}


	/// <summary>Get MAC address.</summary>
	/// <returns>string</returns>
	public string getMac() {
		return this.mac;
	}

}

}