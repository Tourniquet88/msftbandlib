using System;
using System.Threading.Tasks;
using MSFTBandLib;
using MSFTBandLib.Command;
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
		CommandEnum Command = CommandEnum.GetDeviceTime;
		CommandResponse Response = await this.Band.Command(Command);
		TimeResponse ResponseTime = new TimeResponse(Response);
		return ResponseTime.CreateTimeDomain();
	}
	
}

}