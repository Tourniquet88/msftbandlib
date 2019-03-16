using MSFTBandLib;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Rfcomm;

namespace MSFTBandLibUWP {

/// <summary>
/// MSFTBandLib UWP implementation
/// </summary>
public class MSFTBandLibUWP : BandClient<BandSocketUWP> {

	public BandConnection<BandSocket> GetConnection(Band band) {
		BandConnection<BandSocketUWP> connection;
		connection = new BandConnection<BandSocketUWP>(band);
		return (BandConnection<BandSocket>) (object) connection;
	}

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