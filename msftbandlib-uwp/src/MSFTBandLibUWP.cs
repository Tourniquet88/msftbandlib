using MSFTBandLib;

namespace MSFTBandLibUWP {

/// <summary>
/// MSFTBandLib UWP implementation
/// </summary>
public static class MSFTBandLibUWP {

	/// <summary>Create a new Band connection.</summary>
	/// <param name="band">Band instance</param>
	/// <returns>BandConnection</returns>
	public static BandConnection<BandSocketUWP> connection(Band band) {
		return new BandConnection<BandSocketUWP>(band);
	}

}

}