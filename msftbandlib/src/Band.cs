using System;
using MSFTBandLib.Libs;

namespace MSFTBandLib {

/// <summary>
/// Microsoft Band device class
/// </summary>
public class Band {

	/// <summary>MAC address</summary>
	public string Address { get; protected set; }

	///	<summary>Band name</summary>
	public string Name { get; protected set; }

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

}

}