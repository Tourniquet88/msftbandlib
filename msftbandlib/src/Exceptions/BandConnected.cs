using System;

namespace MSFTBandLib.Exceptions {

/// <summary>Band connected exception</summary>
public class BandConnected : Exception {

	/// <summary>Constructor.</summary>
	public BandConnected() : base("Band is already connected.") {}

}

}