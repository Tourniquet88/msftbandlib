using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking;
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

	/// <summary>Device selection</summary>
	BluetoothDevice Device;

	/// <summary>Active Microsoft Band connection object</summary>
	BandConnection<BandSocketUWP> BandConnection;


	/// <summary>Construct the page.</summary>
	public MainPage() {
		this.InitializeComponent();
		this.InitializePage();
	}


	/// <summary>Initialise the page.</summary>
	private async void InitializePage() {
		await this.PopulateDeviceList();
	}


	/// <summary>Clicked "About" button.</summary>
	/// <param name="sender">Sender</param>
	/// <param name="e">Routed event arguments</param>
	private void AboutBtn_Click(object sender, RoutedEventArgs e) {
		this.AboutDialog();
	}


	/// <summary>Clicked "Connect" button.</summary>
	/// <param name="sender">Sender</param>
	/// <param name="e">Routed event arguments</param>
	private async void DevicesBtn_Click(object sender, RoutedEventArgs e) {
		await this.PopulateDeviceList();
	}


	/// <summary>
	/// Handle selection change in the devices list.
	/// 
	/// Assigns the selected device ID to `DevicesListSelectedId`.
	/// </summary>
	/// <param name="sender">Sender</param>
	/// <param name="e">Item click arguments</param>
	private async void DevicesList_Select(
		object sender, TappedRoutedEventArgs e) {

		ScrollViewer i;
		DeviceInformation DeviceInfo;
		RfcommDeviceServicesResult services;
		i = sender as ScrollViewer;
		DeviceInfo = i.DataContext as DeviceInformation;
		this.Device = await BluetoothDevice.FromIdAsync(DeviceInfo.Id);
		services = await this.Device.GetRfcommServicesAsync();
		string s = "Hostname: " + this.Device.HostName.ToString() + "\n";
		foreach (RfcommDeviceService service in services.Services) {
			s += "ServiceId: " + service.ServiceId.Uuid.ToString() + "\n";
		}
		await this.Dialog(this.Device.Name, s);
	}


	/// <summary>
	/// Connect to a Band.
	/// 
	/// Gets a connection instance and then opens the connection.
	/// </summary>
	/// <param name="mac">Band MAC address</param>
	/// <returns>BandConnection</returns>
	private async Task<BandConnection<BandSocketUWP>> Connect(string mac) {
		Band b = new Band(mac);
		BandConnection<BandSocketUWP> c = MSFTBandLibUWPMain.connection(b);
		await c.connect();
		return c;
	}


	/// <summary>Display device details.</summary>
	private async Task PopulateDeviceList() {
		bool devices = false;
		this.DevicesBtn.IsEnabled = false;
		this.DevicesList.IsEnabled = false;
		this.ProgressRing.IsActive = true;
		this.ScanDevicesHelpTxt.Visibility = Visibility.Collapsed;

		string p = BluetoothDevice.GetDeviceSelectorFromPairingState(true);
		var dev = await DeviceInformation.FindAllAsync(p);
		this.DevicesList.Items.Clear();
		foreach (DeviceInformation device in dev) {
			devices = true;
			this.DevicesList.Items.Add(device);
		}
		if (!devices) {
			this.ScanDevicesHelpTxt.Text = "No devices found.";
			this.ScanDevicesHelpTxt.Visibility = Visibility.Visible;
		}
		this.DevicesBtn.IsEnabled = true;
		this.DevicesList.IsEnabled = true;
		this.ProgressRing.IsActive = false;
	}


	/// <summary>Display a dialog.</summary>
	/// <param name="title">Title</param>
	/// <param name="content">Content</param>
	private async Task Dialog(string title, string content) {
		ContentDialog dialog = new ContentDialog {
			Title = title,
			Content = content,
			PrimaryButtonText = "OK"
		};
		await dialog.ShowAsync();
	}


	/// <summary>Display the "About" dialog.</summary>
	private async void AboutDialog() {
		string msg = "";
		IDictionary<string, string> versions = this.GetVersions();
		msg += "MSFTBandLib v" + versions["mbl"] + "\n";
		msg += "MSFTBandLibUWP v" + versions["mbl_uwp"] + "\n";
		msg += "MSFTBandLibUWPApp v" + versions["app"] + "\n";
		msg += "©James Walker 2019. Licensed under the MIT License.";
		await this.Dialog("MSFTBandLib", msg);
	}


	/// <summary>
	/// Get assemblies for the application and Band 
	///  libraries from Reflection.
	///  
	/// Returns a string-indexed dictionary with keys for `app`, `mbl` 
	///  and `mbl_uwp`, containing the assemblies for the application, 
	///  MSFTBandLib library and MSFTBandLibUWP library respectively.
	/// </summary>
	/// <returns>IDictionary<string,Assembly></returns>
	private IDictionary<string, Assembly> GetAssemblies() {
		IDictionary<string, Assembly> a = new Dictionary<
			string, Assembly
		>();
		a["app"] = this.GetType().GetTypeInfo().Assembly;
		a["mbl"] = (
			typeof(MSFTBandLib.MSFTBandLib)
		).GetTypeInfo().Assembly;
		a["mbl_uwp"] = (
			typeof(MSFTBandLibUWP.MSFTBandLibUWP)
		).GetTypeInfo().Assembly;
		return a;
	}


	/// <summary>
	/// Get application and Band library version details.
	/// 
	/// Returns a string-indexed dictionary with keys for `app`, `mbl` 
	///  and `mbl_uwp`, containing the version strings for the app, 
	///  MSFTBandLib library and MSFTBandLibUWP library respectively.
	/// </summary>
	/// <returns>IDictionary<string,string></returns>
	private IDictionary<string, string> GetVersions() {
		IDictionary<string, string> v = new Dictionary<string, string>();
		IDictionary<string, Assembly> a = this.GetAssemblies();
		v["app"] = "TODO";
		v["mbl"] = "TODO";
		v["mbl_uwp"] = "TODO";
		v["app"] = a["app"].GetName().Version.ToString();
		v["mbl"] = a["mbl"].GetName().Version.ToString();
		v["mbl_uwp"] = a["mbl_uwp"].GetName().Version.ToString();
		return v;
	}

}

}