using MSFTBandLib;
using MSFTBandLib.Exceptions;
using System;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;

namespace MSFTBandLib.UWP {

/// <summary>
/// Band socket UWP implementation
/// </summary>
public class BandSocketUWP : BandSocket {

	/// <summary>Currently connected</summary>
	private bool connected = false;

	/// <summary>Socket</summary>
	protected StreamSocket socket;


	/// <summary>Connect to the device (open socket).</summary>
	public async Task Connect(Band band, string service) {
		this.socket = new StreamSocket();
		await this.socket.ConnectAsync(
			new HostName(band.GetAddress()), service,
			SocketProtectionLevel.BluetoothEncryptionAllowNullAuthentication
		);
		this.connected = true;
	}


	/// <summary>Close the connection socket.</summary>
	public async Task Disconnect() {
		await Task.Run(() => this.socket.Dispose());
		this.connected = false;
	}


	/// <summary>Receive bytes up to a specified buffer size.</summary>
	/// <param name="buffer">Buffer size to use</param>
	/// <returns>Task<byte[]></returns>
	/// <exception cref="BandNotConnectedException">No Band.<exception>
	public async Task<byte[]> Receive(int buffer) {
		if (!this.connected) throw new BandNotConnectedException();
		byte[] b = await Task.Run(() => {
			byte[] c = { 0x00 };
			return c;
		});
		return b;
	}


	/// <summary>Send bytes to the device.</summary>
	/// <param name="packet">Bytes to send</param>
	/// <returns>Task</returns>
	/// <exception cref="BandNotConnectedException">No Band.<exception>
	public async Task Send(byte[] packet) {
		if (!this.connected) throw new BandNotConnectedException();
		await Task.Run(() => null);
	}

}

}