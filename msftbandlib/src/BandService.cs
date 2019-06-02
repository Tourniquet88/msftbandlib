namespace MSFTBandLib {

/// <summary>Band service</summary>
public class BandService {

	/// <summary>Band connection instance</summary>
	protected BandInterface Band;


	/// <summary>Band service</summary>
	/// <param name="Band">Band connection instance</param>
	public BandService(BandInterface Band) {
		this.Band = Band;
	}

}

}