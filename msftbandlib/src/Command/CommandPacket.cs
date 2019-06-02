using MSFTBandLib.Command;
using MSFTBandLib.Includes;
using System;
using System.IO;

namespace MSFTBandLib.Command {

/// <summary>Command packet class</summary>
public class CommandPacket {

	/// <summary>Command</summary>
	protected CommandEnum Command;

	/// <summary>Arguments</summary>
	protected byte[] args = null;


	/// <summary>
	/// Create new command packet.
	/// 
	/// When no arguments are given, uses the command's default arguments.
	/// </summary>
	/// <param name="command">Command</param>
	/// <param name="args">Arguments</param>
	public CommandPacket(CommandEnum command, byte[] args=null) {

		// Command
		this.Command = command;

		// Use default for command when no arguments given
		if (args != null) this.args = args;
		else this.args = this.GetCommandDefaultArgumentsBytes();

	}


	/// <summary>
	/// Get the expected data/response size for the command.
	/// </summary>
	/// <returns>int</returns>
	public int GetCommandDataSize() {
		return CommandHelper.GetCommandDataSize(this.Command);
	}


	/// <summary>
	/// Get array of bytes defining size of the arguments to send.
	/// 
	/// TODO: Why is `8` required as base?
	/// </summary>
	/// <returns>byte[]</returns>
	public byte[] GetArgsSizeBytes() {
		return new byte[] { (byte) (8 + this.args.Length) };
	}


	/// <summary>
	/// Get the array of bytes to use as the default arguments 
	/// 	for the command when no arguments are given.
	/// </summary>
	/// <param name="command">Command</param>
	/// <returns>byte[]</returns>
	public byte[] GetCommandDefaultArgumentsBytes() {
		return CommandHelper.GetCommandDefaultArgumentsBytes(this.Command);
	}


	/// <summary>
	/// Get command packet bytes to send.
	/// 
	/// Packet is structured as:
	/// 	- Expected data/response size bytes
	/// 	- `12025` `ushort` constant (TODO: what is this for?)
	/// 	- Command `ushort`
	/// 	- Expected data/response size as integer
	/// 	- Arguments (when none given explicitly, is data size again)
	/// 	
	/// Returns the array of bytes to send to the Band.
	/// </summary>
	/// <returns>byte[]</returns>
	public byte[] GetBytes() {
		ByteStream bytes = new ByteStream();
		bytes.BinaryWriter.Write(this.GetArgsSizeBytes());
		bytes.BinaryWriter.Write((ushort) 12025);
		bytes.BinaryWriter.Write((ushort) this.Command);
		bytes.BinaryWriter.Write(this.GetCommandDataSize());
		bytes.BinaryWriter.Write(this.args);
		return bytes.GetBytes();
	}

}

}