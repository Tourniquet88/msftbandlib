using MSFTBandLib.Libs;

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

}

}