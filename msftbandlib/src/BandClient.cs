using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSFTBandLib {

/// <summary>
/// Band client interface
/// </summary>
public interface BandClient {

	/// <summary>
	/// Get a connection to a given Band instance.
	/// </summary>
	/// <param name="band">Band instance</param>
	/// <returns>Task<BandInterface></returns>
   Task<BandInterface> GetConnection(Band band);

	/// <summary>
	/// Get an array of all available paired Bands.
	/// </summary>
	/// <returns>Task<List<Band>></returns>
	Task<List<Band>> GetPairedBands();

}

}