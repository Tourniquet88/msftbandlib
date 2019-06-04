using MSFTBandLib;
using MSFTBandLib.Command;
using MSFTBandLib.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using static Windows.Networking.Sockets.SocketProtectionLevel;

namespace MSFTBandLib.UWP {

/// <summary>
/// Band socket UWP implementation
/// </summary>
public class BandSocketUWP : BandSocket {

	/// <summary>Currently connected</summary>
	private bool connected = false;

	/// <summary>Socket</summary>
	protected StreamSocket socket;

	/// <summary>Socket data reader</summary>
	protected DataReader socketReader;

	/// <summary>Socket data writer</summary>
	protected DataWriter socketWriter;


	/// <summary>Connect to the device (open socket).</summary>
	/// <param name="band">Band instance</summary>
	///	<param name="uuid">Rfcomm service UUID</summary>
	/// <returns>Task</returns>
	public async Task Connect(Band band, Guid uuid) {
		HostName host;
		RfcommDeviceService service;

		/// Get Band connection
		host = new HostName(band.Address);
		service = await GetRfcommDeviceServiceForHostFromUuid(host, uuid);

		// Create socket and attempt connection
		this.socket = new StreamSocket();
		await this.socket.ConnectAsync(
			host, service.ConnectionServiceName,
			BluetoothEncryptionAllowNullAuthentication
		);

		// Connect reader and writer
		this.socketReader = new DataReader(this.socket.InputStream) {
			InputStreamOptions = InputStreamOptions.Partial
		};
		this.socketWriter = new DataWriter(this.socket.OutputStream);

		// Connected!
		this.connected = true;
	}


	/// <summary>Close the connection socket.</summary>
	/// <returns>Task</returns>
	public async Task Disconnect() {
		await Task.Run(() => {
			this.socketReader.DetachStream();
			this.socketReader.Dispose();
			this.socketWriter.DetachStream();
			this.socketWriter.Dispose();
			this.socket.Dispose();
		});
		this.connected = false;
	}


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
	public async Task<CommandResponse> Receive(uint buffer) {
		return await Task.Run(async () => {
			CommandResponse response = new CommandResponse();
			if (!this.connected) throw new BandNotConnectedException();

			// Keep receiving until we've got status or no data
			while (true) {
				uint bytes = 0;
				response = await Task.Run(async () => {
					bytes = await this.socketReader.LoadAsync(buffer);
					response.AddResponse(this.ReadBytes(bytes));
					return response;
				});
				if (response.StatusReceived() || bytes == 0) break;
			}

			return response;
		});
	}


	/// <summary>Send a command packet to the device.</summary>
	/// <param name="packet">Command packet</param>
	/// <returns>Task</returns>
	/// <exception cref="BandNotConnectedException">No Band.<exception>
	public async Task Send(CommandPacket packet) {
		if (!this.connected) throw new BandNotConnectedException();
		this.socketWriter.WriteBytes(packet.GetBytes());
		await this.socketWriter.StoreAsync();
	}


	/// <summary>
	/// Send command packet to device and get a response.
	/// 
	/// Refer to `Send(...)` and `Receive(...)`.
	/// </summary>
	/// <param name="packet">Command packet</param>
	/// <param name="buffer">Buffer size to receive from</param>
	/// <returns>Task<CommandResponse></returns>
	public async Task<CommandResponse> Request(
		CommandPacket packet, uint buffer) {

		await this.Send(packet);
		return await this.Receive(buffer);
	}


	/// <summary>
	/// Read a given number of bytes from the `SocketReader` 
	/// 	instance and return as a byte array.
	/// 	
	/// This does not load data from the socket – it must 
	/// 	already be buffered in the `SocketReader`.
	/// </summary>
	/// <param name="count">Bytes to read</param>
	/// <returns>byte[]</returns>
	protected byte[] ReadBytes(uint count) {
		byte[] bytes = new byte[count];
		this.socketReader.ReadBytes(bytes);
		return bytes;
	}


	///	<summary>
	///	Get an `RfcommDeviceService` instance for a given Rfcomm 
	///		service UUID of a given hostname.
	/// </summary>
	/// <param name="host">Hostname of device</param>
	/// <param name="uuid">Rfcomm service UUID</param>
	/// <returns>Task<RfcommDeviceService></returns>
	public static async Task<RfcommDeviceService> 
		GetRfcommDeviceServiceForHostFromUuid(HostName host, Guid uuid) {

		RfcommServiceId id;
		BluetoothDevice device;
		id = RfcommServiceId.FromUuid(uuid);
		device = await BluetoothDevice.FromHostNameAsync(host);
		return (await device.GetRfcommServicesForIdAsync(id)).Services[0];
	}

}

}