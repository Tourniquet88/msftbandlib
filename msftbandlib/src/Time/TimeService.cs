using System;
using System.Threading.Tasks;
using MSFTBandLib;
using MSFTBandLib.Includes;

namespace MSFTBandLib.Time {

/// <summary>Time service</summary>
public class TimeService : BandService {
	
	/// <summary>Time service</summary>
	/// <param name="band">Band connection instance</param>
	public TimeService(BandInterface band) : base(band) {}
	

	/// <summary>Get device time.</summary>
	/// <returns>Task<TimeDomain></returns>
	public async Task<TimeDomain> GetDeviceTime() {
		ByteArray bytes;
		bytes = await this.Band.Command(Command.Command.GetDeviceTime);
		return TimeDomain.CreateFromBandBytes(bytes);
	}
	
}

}