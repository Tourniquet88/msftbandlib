using MSFTBandLib;
using MSFTBandLib.Exceptions;
using System;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

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
		host = new HostName(band.GetAddress());
		service = await GetRfcommDeviceServiceForHostFromUuid(host, uuid);
		this.socket = new StreamSocket();
		await this.socket.ConnectAsync(
			host, service.ConnectionServiceName,
			SocketProtectionLevel.BluetoothEncryptionAllowNullAuthentication
		);
		this.socketReader = new DataReader(this.socket.InputStream);
		this.socketReader.InputStreamOptions = InputStreamOptions.Partial;
		this.socketWriter = new DataWriter(this.socket.OutputStream);
		this.connected = true;
	}


	/// <summary>Close the connection socket.</summary>
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


	/// <summary>Receive bytes up to a specified buffer size.</summary>
	/// <param name="buffer">Buffer size to use</param>
	/// <returns>Task<byte[]></returns>
	/// <exception cref="BandNotConnectedException">No Band.<exception>
	public async Task<byte[]> Receive(int buffer) {
		if (!this.connected) throw new BandNotConnectedException();
		uint len = await this.socketReader.LoadAsync((uint) buffer);
		byte[] bytes = new byte[len];
		this.socketReader.ReadBytes(bytes);
		return bytes;
	}


	/// <summary>Send bytes to the device.</summary>
	/// <param name="packet">Bytes to send</param>
	/// <returns>Task</returns>
	/// <exception cref="BandNotConnectedException">No Band.<exception>
	public async Task Send(byte[] packet) {
		if (!this.connected) throw new BandNotConnectedException();
		this.socketWriter.WriteBytes(packet);
		await this.socketWriter.StoreAsync();
	}


	/// <summary>Send bytes to the device and get a response.</summary>
	/// <param name="packet">Bytes to send</param>
	/// <param name="buffer">Buffer size to receive from</param>
	/// <returns>Task<byte[]></returns>
	public async Task<byte[]> SendReceive(byte[] packet, int buffer) {
		await this.Send(packet);
		byte[] response = await this.Receive(buffer);
		return response;
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

		BluetoothDevice device;
		RfcommServiceId id;
		RfcommDeviceServicesResult services;
		id = RfcommServiceId.FromUuid(uuid);
		device = await BluetoothDevice.FromHostNameAsync(host);
		services = await device.GetRfcommServicesForIdAsync(id);
		return services.Services[0];
	}

}

}