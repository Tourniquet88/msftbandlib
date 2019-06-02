using System;

namespace MSFTBandLib.Command {

/// <summary>Command data size attribute</summary>
public class CommandDataSize : Attribute {

	/// <summary>Assigned data size</summary>
	public int DataSize { get; private set; }


	/// <summary>Command data size constructor.</summary>
	/// <param name="DataSize">Assigned data size</param>
	/// <returns>public</returns>
	public CommandDataSize(int DataSize) {
		this.DataSize = DataSize;
	}

}

}