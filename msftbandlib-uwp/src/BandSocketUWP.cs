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
public class BandSocketUWP : BandSocketInterface {

	/// <summary>Socket</summary>
	protected StreamSocket Socket;

	/// <summary>Socket data reader</summary>
	protected DataReader DataReader;

	/// <summary>Socket data writer</summary>
	protected DataWriter DataWriter;
	
	/// <summary>Currently connected</summary>
	public bool Connected { get; protected set; }

	///	<summary>Disposed</summary>
	public bool Disposed { get; protected set; }


	/// <summary>Dispose of the socket.</summary>
	public void Dispose() {
		this.Dispose(true);
		GC.SuppressFinalize(this);
	}


	/// <summary>Dispose of the socket.</summary>
	/// <param name="disposing">Disposing (not used)</param>
	protected virtual void Dispose(bool disposing) {
		if (!this.Disposed) {
			this.Socket.Dispose();
			this.DataReader.Dispose();
			this.DataWriter.Dispose();
			this.Disposed = true;
		}
	}


	/// <summary>Connect to the device (open socket).</summary>
	/// <param name="string">Band address</summary>
	///	<param name="uuid">RFCOMM service UUID</summary>
	/// <returns>Task</returns>
	/// <exception cref="BandSocketConnected">Already connected</exception>
	public async Task Connect(string mac, Guid uuid) {
		HostName host;
		RfcommDeviceService service;
		if (this.Connected) throw new BandSocketConnected();

		/// Get Band connection
		host = new HostName(mac);
		service = await GetRfcommDeviceServiceForHostFromUuid(host, uuid);

		// Create socket and attempt connection
		this.Socket = new StreamSocket();
		await this.Socket.ConnectAsync(
			host, service.ConnectionServiceName,
			BluetoothEncryptionAllowNullAuthentication
		);

		// Connect reader and writer
		this.DataReader = new DataReader(this.Socket.InputStream) {
			InputStreamOptions = InputStreamOptions.Partial
		};
		this.DataWriter = new DataWriter(this.Socket.OutputStream);

		// Connected!
		this.Connected = true;
	}


	/// <summary>Close the connection socket.</summary>
	/// <returns>Task</returns>
	/// <exception cref="BandSocketConnectedNot">Not connected.</exception>
	public async Task Disconnect() {
		if (this.Connected) {
			await Task.Run(() => {
				this.DataReader.DetachStream();
				this.DataReader.Dispose();
				this.DataWriter.DetachStream();
				this.DataWriter.Dispose();
				this.Socket.Dispose();
				this.Connected = false;
			});
		}
		else throw new BandSocketConnectedNot();
	}


	/// <summary>Send a command packet to the device.</summary>
	/// <param name="packet">Command packet</param>
	/// <returns>Task</returns>
	/// <exception cref="BandSocketConnectedNot">Not connected.<exception>
	public async Task Send(CommandPacket packet) {
		if (!this.Connected) throw new BandSocketConnectedNot();
		this.DataWriter.WriteBytes(packet.GetBytes());
		await this.DataWriter.StoreAsync();
	}


	/// <summary>
	/// Receive bytes up to a specified buffer size.
	/// 
	/// Receives and adds bytes to a single `CommandResponse` object 
	/// until no more bytes are received or a Band status has been 
	/// received (indicating end of data).
	/// </summary>
	/// <param name="buffer">buffer</param>
	/// <returns>Task<CommandResponse></returns>
	/// <exception cref="BandSocketConnectedNot">Not connected.<exception>
	public async Task<CommandResponse> Receive(uint buffer) {
		return await Task.Run(async () => {
			CommandResponse response = new CommandResponse();
			if (!this.Connected) throw new BandSocketConnectedNot();

			// Keep receiving until we've got status or no data
			while (true) {
				uint bytes = 0;
				response = await Task.Run(async () => {
					bytes = await this.DataReader.LoadAsync(buffer);
					response.AddResponse(this.ReadBytes(bytes));
					return response;
				});
				if (response.StatusReceived() || bytes == 0) break;
			}

			return response;
		});
	}


	/// <summary>
	/// Send command packet to device and get a response.
	/// 
	/// Refer to `Send(...)` and `Receive(...)`.
	/// </summary>
	/// <param name="packet">Command packet</param>
	/// <param name="buffer">Buffer size to receive from</param>
	/// <returns>Task<CommandResponse></returns>
	/// <exception cref="BandSocketConnectedNot">Not connected.<exception>
	public async Task<CommandResponse> Request(
		CommandPacket packet, uint buffer) {

		if (this.Connected) {
			await this.Send(packet);
			return await this.Receive(buffer);
		}
		else throw new BandSocketConnectedNot();
	}


	/// <summary>
	/// Read a given number of bytes from the `DataReader` 
	/// instance and return as a byte array.
	/// 	
	/// This does not load data from the socket – it must 
	/// already be buffered in the `DataReader`.
	/// </summary>
	/// <param name="count">Bytes to read</param>
	/// <returns>byte[]</returns>
	protected byte[] ReadBytes(uint count) {
		byte[] bytes = new byte[count];
		this.DataReader.ReadBytes(bytes);
		return bytes;
	}


	///	<summary>
	///	Get an `RfcommDeviceService` instance for a given RFCOMM 
	///	service UUID of a given hostname.
	/// </summary>
	/// <param name="host">Hostname of device</param>
	/// <param name="uuid">RFCOMM service UUID</param>
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