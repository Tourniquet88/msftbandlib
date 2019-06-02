using System;
using System.Threading.Tasks;
using MSFTBandLib.Command;

namespace MSFTBandLib {

/// <summary>
/// Band socket interface
/// </summary>
public interface BandSocket {

	/// <summary>Connect to a Band.</summary>
	/// <param name="band">Band instance</param>
	/// <param name="uuid">Rfcomm service UUID</param>
	/// <returns>Task</returns>
	Task Connect(Band band, Guid uuid);

	/// <summary>Close the connection.</summary>
	/// <returns>Task</returns>
	Task Disconnect();

	/// <summary>
	/// Receive bytes up to a specified buffer size.
	/// 
	/// Receives and adds bytes to a single `CommandResponse` 
	/// 	object until no more bytes are received or a 
	/// 	Band status has been received (indicating end of data).
	/// </summary>
	/// <param name="buffer">buffer</param>
	/// <returns>Task<CommandResponse></returns>
	/// <exception cref="BandNotConnectedException">No Band.<exception>
	Task<CommandResponse> Receive(uint buffer);

	/// <summary>Send a command packet to the device.</summary>
	/// <param name="packet">Command packet</param>
	/// <returns>Task</returns>
	/// <exception cref="BandNotConnectedException">No Band.<exception>
	Task Send(CommandPacket packet);

	/// <summary>
	/// Send command packet to device and get a response.
	/// 
	/// Refer to `Send(...)` and `Receive(...)`.
	/// </summary>
	/// <param name="packet">Command packet</param>
	/// <param name="buffer">Buffer size to receive from</param>
	/// <returns>Task<CommandResponse></returns>
	Task<CommandResponse> Request(CommandPacket packet, uint buffer);

}

}