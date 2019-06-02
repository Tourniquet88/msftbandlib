using System;
using System.Linq;
using System.Threading.Tasks;
using MSFTBandLib;
using MSFTBandLib.Command;
using MSFTBandLib.Includes;

namespace MSFTBandLib.Device {

/// <summary>Device service</summary>
public class DeviceService : BandService {

	/// <summary>Device service</summary>
	/// <param name="band">Band connection instance</param>
	public DeviceService(BandInterface band) : base(band) {}


	/// <summary>Get device serial number.</summary>
	/// <returns>Task<String></returns>
	public async Task<String> GetSerialNumber() {
		CommandEnum command = CommandEnum.GetSerialNumber;
		CommandResponse response = await this.Band.Command(command);
		return "foobar";
	}

}

}