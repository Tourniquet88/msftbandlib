using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MSFTBandLib;
using MSFTBandLibUWP;
using MSFTBandLibUWPMain = MSFTBandLibUWP.MSFTBandLibUWP;

namespace MSFTBandLibUWPApp {

/// <summary>
/// MSFTBandLibUWPApp main page.
/// </summary>
public sealed partial class MainPage : Page {

	/// <summary>Device ID of currently selected device in list</summary>
	string DevicesListSelectedId;

	/// <summary>Band connection</summary>
	BandConnection<BandSocketUWP> BandConnection;


	/// <summary>Construct the page.</summary>
	public MainPage() {
		this.InitializeComponent();
		this.Devices();
	}


	/// <summary>Display device details.</summary>
	private async void Devices() {
		var dev = await DeviceInformation.FindAllAsync();
		foreach (DeviceInformation device in dev) {
			if (device.Name.Contains("MS Band")) {
				this.DevicesList.Items.Add(device);
			}
		}
	}


	/// <summary>
	/// Handle selection change in the devices list.
	/// 
	/// Assigns the selected device ID to `DevicesListSelectedId`.
	/// </summary>
	/// <param name="sender">Sender</param>
	/// <param name="e">Item click arguments</param>
	private void DevicesList_Select(
		object sender, ItemClickEventArgs e) {

		DeviceInformation item = e.ClickedItem as DeviceInformation;
		this.DevicesListSelectedId = item.Id;
	}


	/// <summary>Clicked "Connect" button.</summary>
	/// <param name="sender">Sender</param>
	/// <param name="e">Routed event arguments</param>
	private async void ConnectBtn_Click(object sender, RoutedEventArgs e) {
		if (this.DevicesListSelectedId == null) {
			await this.dialog("Select a device", "No device selected.");
			return;
		}
		try {
			await this.dialog("Connecting", this.DevicesListSelectedId);
			await this.dialog("Connected!", "Connected.");
			this.DevicesList.IsEnabled = false;
			this.ConnectBtn.IsEnabled = false;
		}
		catch (Exception exception) {
			string msg = "Message: " + exception.Message + "\n";
			msg += "Source: " + exception.Source + "\n";
			msg += "Stack trace:\n" + exception.StackTrace;
			await this.dialog("Connection failed", msg);
		}
	}


	/// <summary>
	/// Connect to a Band.
	/// 
	/// Gets a connection instance and then opens the connection.
	/// </summary>
	/// <param name="mac">Band MAC address</param>
	/// <returns>BandConnection</returns>
	private async Task<BandConnection<BandSocketUWP>> connect(string mac) {
		Band b = new Band(mac);
		BandConnection<BandSocketUWP> c = MSFTBandLibUWPMain.connection(b);
		await c.connect();
		return c;
	}


	/// <summary>Display a dialog.</summary>
	/// <param name="title">Title</param>
	/// <param name="content">Content</param>
	private async Task dialog(string title, string content) {
		ContentDialog dialog = new ContentDialog {
			Title = title,
			Content = content,
			PrimaryButtonText = "OK"
		};
		await dialog.ShowAsync();
	}

}

}