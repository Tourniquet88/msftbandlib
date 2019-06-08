using MSFTBandLib.Command;
using MSFTBandLib.Includes;
using System.Threading.Tasks;

namespace MSFTBandLib {

/// <summary>Band interface</summary>
public interface BandInterface {

	/// <summary>Get MAC address.</summary>
	/// <returns>string</returns>
	string GetMac();

	/// <summary>Get Bluetooth name.</summary>
	/// <returns>string</returns>
	string GetName();

	/// <summary>Connect to the Band.</summary>
	/// <returns>Task</returns>
	Task Connect();

	/// <summary>Disconnect from the Band.</summary>
	/// <returns>Task</returns>
	Task Disconnect();

	/// <summary>Run a command.</summary>
	/// <param name="Command">Command to run</param>
	/// <returns>Task<CommandResponse></returns>
	Task<CommandResponse> Command(CommandEnum Command);

	/// <summary>Get the current device time.</summary>
	/// <returns>Task<DateTime></returns>
	Task<DateTime> GetDeviceTime();

	/// <summary>Get serial number from the Band.</summary>
	/// <returns>Task<string></returns>
	Task<string> GetSerialNumber();

}

}