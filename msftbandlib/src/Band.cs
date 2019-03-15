namespace MSFTBandLib {

/// <summary>
/// Microsoft Band device class
/// </summary>
public class Band {

	/// <summary>MAC address</summary>
	protected string Address;

	/// <summary>Cargo service UUID</summary>
	public const string CARGO = "a502ca97-2ba5-413c-a4e0-13804e47b38f";

	/// <summary>Cargo 2 service UUID</summary>
	public const string CARGO2 = "a502ca99-2ba5-413c-a4e0-13804e47b38f";

	/// <summary>Cargo 3 service UUID</summary>
	public const string CARGO3 = "a502ca9a-2ba5-413c-a4e0-13804e47b38f";

	/// <summary>Cargo 4 service UUID</summary>
	public const string CARGO4 = "a502ca9b-2ba5-413c-a4e0-13804e47b38f";

	/// <summary>Push service UUID</summary>
	public const string PUSH = "c742e1a2-6320-5abc-9643-d206c677e580";


	/// <summary>Construct a new device instance.</summary>
	/// <param name="address">MAC address</param>
	public Band(string mac) {
		this.Address = address;
	}


	/// <summary>Get MAC address.</summary>
	/// <returns>string</returns>
	public string GetAddress() {
		return this.Address;
	}

}

}