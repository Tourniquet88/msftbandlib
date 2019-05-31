using MSFTBandLib.Libs;

namespace MSFTBandLib {

/// <summary>Band command class</summary>
public class BandCommand {

	/// <summary>Command bits</summary>
	public ushort command;


	/// <summary>Create a command instance.</summary>
	/// <param name="command">Command bits</param>
	public BandCommand(ushort command) {
		this.command = command;
	}


	/// <summary>
	/// Get command bits given a facility, TX bit and index.
	/// 
	/// We use bitwise processing here.
	/// 
	/// Reminders:
	///  - `<<` shifts the left operand's value left by 
	/// 	the number of bits specified by the right operand.
	///  - `|` copies a bit if it exists in either of its operands.
	/// </summary>
	/// <param name="facility">Facility</param>
	/// <param name="tx">TX bit</param>
	/// <param name="index">Index</param>
	/// <returns>ushort</returns>
	public static ushort GetCommandUshortFromFacility(
		Facility facility, bool tx, int index) {

		return (ushort) ((int) facility << 8 | (tx ? 1 : 0) << 7 | index);
	}


	/// <summary>
	/// Create a new command instance given a facility, TX bit and index.
	/// </summary>
	/// <param name="facility">Facility</param>
	/// <param name="tx">TX bit</param>
	/// <param name="index">Index</param>
	/// <returns>BandCommand</returns>
	public static BandCommand CreateFromFacility(
		Facility facility, bool tx, int index) {

		ushort command = BandCommand.GetCommandUshortFromFacility(
			facility, tx, index
		);
		return new BandCommand(command);
	}

}

}