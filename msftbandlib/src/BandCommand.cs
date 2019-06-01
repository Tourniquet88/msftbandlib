using MSFTBandLib.Libs;
using System.IO;

namespace MSFTBandLib {

/// <summary>Band command</summary>
public static class BandCommand {

	/// <summary>
	/// Create a new command given a Band facility.
	/// 
	/// You must specify the facility index and whether it is a TX bit.
	/// 
	/// This method uses bitwise operations to convert the facility/index 
	/// 	integers into a `ushort` which is returned as the command.
	/// 
	/// Reminders:
	///  - `<<` shifts the left operand's value left by 
	/// 	the number of bits specified by the right operand.
	///  - `|` copies a bit if it exists in either of its operands.
	///  
	/// Returns a `ushort` for use as the command.
	/// </summary>
	/// <param name="facility">Facility</param>
	/// <param name="tx">TX bit</param>
	/// <param name="index">Index</param>
	/// <returns>ushort</returns>
	public static ushort Create(Facility facility, bool tx, int index) {
		return (ushort) ((int) facility << 8 | (tx ? 1 : 0) << 7 | index);
	}


	/// <summary>Create a command packet.</summary>
	/// <param name="command">Commnad</param>
	/// <param name="DataSize">Size of data which will be sent</param>
	/// <param name="args">Arguments to send</param>
	/// <param name="argsPrependSize">Prepend size of arguments</param>
	/// <returns>byte[]</returns>
	public static byte[] CreatePacket(
		ushort command, int DataSize=0,
		byte[] args=null, bool argsPrependSize=false) {

		MemoryStream str = new MemoryStream();
		BinaryWriter writer = new BinaryWriter(str);
		if (argsPrependSize) {
			if (args == null) writer.Write(new byte[8]);
			else writer.Write(new byte[(8 +  args.Length)]);
		}
		writer.Write((ushort) 12025);
		writer.Write(command);
		writer.Write(DataSize);
		if (args != null) writer.Write(args);
		return str.ToArray();
	}

}

}