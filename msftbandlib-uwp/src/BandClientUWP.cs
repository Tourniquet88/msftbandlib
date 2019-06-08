using MSFTBandLib;
using MSFTBandLib.Libs;
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
public class BandClientUWP : BandClientInterface {

	/// <summary>
	///	Get list of all available paired Bands.
	/// </summary>
	/// <returns>Task<List<Band>></returns>
	public async Task<List<BandInterface>> GetPairedBands() {
		string selector;
		RfcommServiceId cargo;
		DeviceInformationCollection devices;
		List<BandInterface> bands = new List<BandInterface>();

		// Get devices
		cargo = RfcommServiceId.FromUuid(Guid.Parse(Services.CARGO));
		selector = RfcommDeviceService.GetDeviceSelector(cargo);
		devices = await DeviceInformation.FindAllAsync(selector);

		// Create Band instances
		foreach (DeviceInformation device in devices) {
			BluetoothDevice bt;
			bt = await BluetoothDevice.FromIdAsync(device.Id);
			bands.Add(new Band<BandSocketUWP>(bt.HostName.ToString(), bt.Name));
		}
		return bands;
	}

}

}