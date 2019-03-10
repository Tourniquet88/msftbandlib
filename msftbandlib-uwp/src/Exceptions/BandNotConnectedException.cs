using System;

namespace MSFTBandLibUWP.Exceptions {

/// <summary>Band not connected exception</summary>
public class BandNotConnectedException : Exception {

	/// <summary>Constructor.</summary>
	public BandNotConnectedException() : base("Band is not connected.") {}

}

}