using MSFTBandLib;
using MSFTBandLib.Libs;

namespace MSFTBandLib {

/// <summary>Commands</summary>
public static class Commands {

	/// Facility.LibraryConfiguration

	public static readonly ushort GetSerialNumber = BandCommand.Create(
		Facility.LibraryConfiguration, true, 8
	);


	/// Facility.LibraryTime

	public static readonly ushort GetDeviceTime = BandCommand.Create(
		Facility.LibraryTime, true, 2
	);

	public static readonly ushort SetDeviceTime = BandCommand.Create(
		Facility.LibraryTime, false, 1
	);


	/// Facility.ModuleFireballUI

	public static readonly ushort GetMeTileImage = BandCommand.Create(
		Facility.ModuleFireballUI, true, 14
	);

	public static readonly ushort SetMeTileImage = BandCommand.Create(
		Facility.ModuleFireballUI, false, 17
	);

	public static readonly ushort SetUIScreen = BandCommand.Create(
		Facility.ModuleFireballUI, false, 0
	);


	/// Facility.ModuleInstalledAppList

	public static readonly ushort GetTile = BandCommand.Create(
		Facility.ModuleInstalledAppList, true, 7
	);

	public static readonly ushort GetTiles = BandCommand.Create(
		Facility.ModuleInstalledAppList, true, 0
	);

	public static readonly ushort GetTilesDefaults = BandCommand.Create(
		Facility.ModuleInstalledAppList, true, 4
	);

	public static readonly ushort SetTile = BandCommand.Create(
		Facility.ModuleInstalledAppList, false, 6
	);

	public static readonly ushort SetTiles = BandCommand.Create(
		Facility.ModuleInstalledAppList, false, 1
	);

	public static readonly ushort SyncStartStripStart = BandCommand.Create(
		Facility.ModuleInstalledAppList, false, 2
	);

	public static readonly ushort SyncStartStripEnd = BandCommand.Create(
		Facility.ModuleInstalledAppList, false, 3
	);


	/// Facility.ModuleThemeColor

	public static readonly ushort SetThemeColor = BandCommand.Create(
		Facility.ModuleThemeColor, false, 0
	);

}

}