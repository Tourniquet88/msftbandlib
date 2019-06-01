using MSFTBandLib;
using MSFTBandLib.Libs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
		public void BandCommand_Create() {
			ushort subscribe = BandCommand.Create(
				Facility.LibraryRemoteSubscription, false, 0
			);
			ushort get_tiles = BandCommand.Create(
				Facility.ModuleInstalledAppList, true, 0
			);
			Assert.AreEqual(36608, subscribe);
			Assert.AreEqual(54400, get_tiles);
		}


		/// <summary>
		/// Test creating a command packet.
		/// </summary>
		[TestMethod]
		public void BandCommand_CreatePacket() {
			byte[] time = BandCommand.CreatePacket(
				Command.GetDeviceTime, 16, null, true
			);
			int[] timeint = Array.ConvertAll(time, c => (int) c);
			int[] timeintexpect = new int[] {
				12, 249, 46, 130, 117, 16, 0, 0, 0, 16, 0, 0, 0
			};
			Console.WriteLine("[{0}]", string.Join(", ", timeint));
			CollectionAssert.AreEqual(timeintexpect, timeint);
		}

	}

}