using MSFTBandLib;
using MSFTBandLib.Libs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSFTBandLibTests {

	/// <summary>MSFTBandLibTest</summary>
	[TestClass]
	public class MSFTBandLibTest {

		/// <summary>
		/// Test getting command values from Facility.
		/// 
		/// Test values are derived from `libmsftband`.
		/// </summary>
		[TestMethod]
		public void BandCommand_GetCommandUshortFromFacility() {
			ushort subscribe = BandCommand.GetCommandUshortFromFacility(
				Facility.LibraryRemoteSubscription, false, 0
			);
			ushort get_tiles = BandCommand.GetCommandUshortFromFacility(
				Facility.ModuleInstalledAppList, true, 0
			);
			Assert.AreEqual(subscribe, 36608);
			Assert.AreEqual(get_tiles, 54400);
		}

	}

}