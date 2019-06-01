using MSFTBandLib.Libs;
using System;
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
	/// <param name="command">Command</param>
	/// <param name="DataSize">Size of data to send/receive</param>
	/// <param name="args">Arguments to send</param>
	/// <param name="argsLenPrefix">Prepend size of arguments</param>
	/// <returns>byte[]</returns>
	public static byte[] CreatePacket(
		Command command, int DataSize=0,
		byte[] args=null, bool argsPrepend=false) {

		byte[] arguments = args;
		MemoryStream str = new MemoryStream();
		BinaryWriter writer = new BinaryWriter(str);

		// When no arguments given, we use expected data/response size
		if (args == null) arguments = GetCommandDefaultArgs(DataSize);

		// When prepending length of arguments, encode it as byte in array
		if (argsPrepend) writer.Write(GetArgsLenBytes(arguments.Length));

		// Write the command packet
		// - 12025 (TODO: What is this for?)
		// - Command `ushort`
		// - Expected data/response size, 
		writer.Write((ushort) 12025);
		writer.Write((ushort) command);
		writer.Write(DataSize);

		// Write arguments if present; otherwise 
		// 	we have to repeat the data/response size
		if (arguments.Length > 0) {
			writer.Write(arguments);
		}
		else writer.Write(DataSize);

		return str.ToArray();
	}


	/// <summary>
	/// Get byte array to prepend to command packet when including 
	/// 	length of arguments as a prefix.
	/// </summary>
	/// <param name="length">Length of arguments</param>
	/// <returns>byte[]</returns>
	public static byte[] GetArgsLenBytes(int length) {
		return new byte[] { (byte) (8 + length) };
	}


	/// <summary>
	/// Get default command arguments bytes to use when none given 
	/// 	for a command; we have to add the expected data size 
	/// 	as a single byte in the bytes array.
	/// </summary>
	/// <param name="DataSize">Size of data to send/receive</param>
	/// <returns>byte[]</returns>
	public static byte[] GetCommandDefaultArgs(int DataSize) {
		MemoryStream str = new MemoryStream();
		BinaryWriter writer = new BinaryWriter(str);
		writer.Write(DataSize);
		return str.ToArray();
	}

}

}