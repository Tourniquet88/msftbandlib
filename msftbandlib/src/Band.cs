using MSFTBandLib;
using MSFTBandLib.Command;
using MSFTBandLib.Exceptions;
using MSFTBandLib.Includes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSFTBandLib {

/// <summary>
/// Microsoft Band device class
/// 
/// Most methods will throw `BandConnectedNot` from the `Command` 
/// method when trying to access Bluetooth endpoints when not connected.
/// </summary>
public class Band<T> : BandInterface 
where T : class, BandSocketInterface {

	/// <summary>MAC address</summary>
	public string Mac { get; protected set; }

	///	<summary>Bluetooth name</summary>
	public string Name { get; protected set; }

	/// <summary>Get currently connected</summary>
	public bool Connected {
		get => this.Connection.Connected;
		set => throw new Exception("Can't set connection status directly!");
	}

	/// <summary>Band Bluetooth connection</summary>
	public BandConnection<T> Connection { get; protected set; }


	/// <summary>Construct a new device instance.</summary>
	/// <param name="mac">MAC address</param>
	/// <param name="name">Bluetooth name</param>
	public Band(string mac, string name) {
		this.Mac = mac;
		this.Name = name;
		this.Connection = new BandConnection<T>(this);
	}


	/// <summary>Get MAC address.</summary>
	/// <returns>string</returns>
	public string GetMac() {
		return this.Mac;
	}


	/// <summary>Get Bluetooth name.</summary>
	/// <returns>string</returns>
	public string GetName() {
		return this.Name;
	}


	/// <summary>Connect to the Band.</summary>
	/// <returns>Task</returns>
	/// <exception cref="BandConnected">Band already connected.</exception>
	public async Task Connect() {
		if (!this.Connected) {
			await this.Connection.Connect();
		}
		else throw new BandConnected();
	}


	/// <summary>Disconnect from the Band.</summary>
	/// <returns>Task</returns>
	/// <exception cref="BandConnectedNot">Band not connected.</exception>
	public async Task Disconnect() {
		if (this.Connected) {
			await this.Connection.Disconnect();
		}
		else throw new BandConnectedNot();
	}


	/// <summary>Run a command using the Band's `BandConnection`.</summary>
	/// <param name="Command">Command to run</param>
	/// <returns>Task<CommandResponse></returns>
	/// <exception cref="BandConnectedNot">Band not connected.</exception>
	public async Task<CommandResponse> Command(CommandEnum Command) {
		if (!this.Connected) throw new BandConnectedNot();
		else return await this.Connection.Command(Command);
	}


	/// <summary>Get the current device time.</summary>
	/// <returns>Task<DateTime></returns>
	public async Task<Includes.DateTime> GetDeviceTime() {
		var res = await this.Command(CommandEnum.GetDeviceTime);
		ushort[] t = ((CommandResponse)res).GetByteStream().GetUshorts(8);
		List<ushort> times = new List<ushort>(t);
		times.RemoveAt(2);
		return new Includes.DateTime(times.ToArray());
	}


	/// <summary>Get serial number from the Band.</summary>
	/// <returns>Task<String></returns>
	public async Task<string> GetSerialNumber() {
		var res = await this.Command(CommandEnum.GetSerialNumber);
		return ((CommandResponse)res).GetByteStream().GetString();
	}

}

}