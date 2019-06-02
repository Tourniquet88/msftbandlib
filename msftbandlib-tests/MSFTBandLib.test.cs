using MSFTBandLib;
using MSFTBandLib.Command;
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
		public void Command_CommandHelper_Create() {
			ushort subscribe = CommandHelper.Create(
				Facility.LibraryRemoteSubscription, false, 0
			);
			ushort get_tiles = CommandHelper.Create(
				Facility.ModuleInstalledAppList, true, 0
			);
			Assert.AreEqual(36608, subscribe);
			Assert.AreEqual(54400, get_tiles);
		}


		[TestMethod]
		///	<summary>
		///	Test getting command data size attribute value.
		///	</summary>
		public void Command_CommandHelper_GetCommandDataSize() {
			int time = CommandHelper.GetCommandDataSize(
				Command.GetDeviceTime
			);
			Assert.AreEqual(16, time);
		}


		/// <summary>
		/// Test creating a command packet.
		/// </summary>
		[TestMethod]
		public void Command_CommandPacket() {
			CommandPacket packet = new CommandPacket(Command.GetDeviceTime);
			byte[] bytes = packet.GetBytes();
			int[] ints = Array.ConvertAll(bytes, b => (int) b);
			int[] intsExpect = new int[] {
				12, 249, 46, 130, 117, 16, 0, 0, 0, 16, 0, 0, 0
			};
			Console.WriteLine("[{0}]", string.Join(", ", ints));
			CollectionAssert.AreEqual(intsExpect, ints);
		}

	}

}