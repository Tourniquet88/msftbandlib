using System;
using System.Threading.Tasks;
using MSFTBandLib.Command;

namespace MSFTBandLib.Time {

/// <summary>Time service</summary>
public class TimeService {

	/// <summary>Band connection instance</summary>
	protected BandInterface Band;


	/// <summary>Time service</summary>
	/// <param name="Band">Band connection instance</param>
	/// <returns>public</returns>
	public TimeService(BandInterface Band) {
		this.Band = Band;
	}


	/// <summary>Get device time.</summary>
	/// <returns>Task<byte[]></returns>
	public async Task<byte[]> GetDeviceTime() {
		return await this.Band.Command(Command.Command.GetDeviceTime);
	}
	
}

}