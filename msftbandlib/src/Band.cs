using System;
using MSFTBandLib.Libs;

namespace MSFTBandLib {

/// <summary>
/// Microsoft Band device class
/// </summary>
public class Band {

	/// <summary>MAC address</summary>
	protected string Address;

	///	<summary>Band name</summary>
	protected string Name;

	/// <summary>Cargo service UUID</summary>
	public static readonly Guid CARGO = Guid.Parse(Services.CARGO);

	/// <summary>Cargo 2 service UUID</summary>
	public static readonly Guid CARGO2 = Guid.Parse(Services.CARGO2);

	/// <summary>Cargo 3 service UUID</summary>
	public static readonly Guid CARGO3 = Guid.Parse(Services.CARGO3);

	/// <summary>Cargo 4 service UUID</summary>
	public static readonly Guid CARGO4 = Guid.Parse(Services.CARGO4);

	/// <summary>Push service UUID</summary>
	public static readonly Guid PUSH = Guid.Parse(Services.PUSH);


	/// <summary>Construct a new device instance.</summary>
	/// <param name="address">MAC address</param>
	public Band(string mac, string name) {
		this.Address = mac;
		this.Name = name;
	}


	/// <summary>Get MAC address.</summary>
	/// <returns>string</returns>
	public string GetAddress() {
		return this.Address;
	}


	/// <summary>Gets the name of the Band.</summary>
	/// <returns>string</returns>
	public string GetName() {
		return this.Name;
	}

}

}