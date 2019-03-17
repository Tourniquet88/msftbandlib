using MSFTBandLib;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Rfcomm;

namespace MSFTBandLib.UWP {

/// <summary>
/// MSFTBandLib UWP implementation
/// </summary>
public class MSFTBandLibUWP : BandClient {

	/// <summary>
	/// Get a connection to a given Band.
	/// 
	/// Constructs a new connection instance and connects to the Band.
	/// </summary>
	/// <param name="band">Band instance</param>
	/// <returns>BandConnection<BandSocket></returns>
	public async Task<BandInterface> GetConnection(Band band) {
		BandInterface BandConnection = new BandConnection<BandSocketUWP>();
		await BandConnection.Connect(band);
		return BandConnection;
	}


	/// <summary>
	///	Get list of all available paired Bands.
	/// </summary>
	/// <returns>Task<List<Band>></returns>
	public async Task<List<Band>> GetPairedBands() {
		string selector;
		RfcommServiceId cargo;
		DeviceInformationCollection devices;
		List<Band> bands = new List<Band>();
		cargo = RfcommServiceId.FromUuid(Band.CARGO);
		selector = RfcommDeviceService.GetDeviceSelector(cargo);
		devices = await DeviceInformation.FindAllAsync(selector);
		foreach (DeviceInformation device in devices) {
			BluetoothDevice bt;
			bt = await BluetoothDevice.FromIdAsync(device.Id);
			bands.Add(new Band(bt.HostName.ToString(), bt.Name));
		}
		return bands;
	}

}

}