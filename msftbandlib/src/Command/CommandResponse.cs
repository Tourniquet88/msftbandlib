using MSFTBandLib.Includes;
using System.Collections.Generic;
using System.Linq;

namespace MSFTBandLib.Command {

/// <summary>Command response class</summary>
public class CommandResponse {

	/// <summary>Status bytes</summary>
	public byte[] Status = new byte[6];

	/// <summary>Received data byte sequences</summary>
	public List<byte[]> Data = new List<byte[]>();

	/// <summary>Response status bytes sequence length</summary>
	public const int RESPONSE_STATUS_LENGTH = 6;


	/// <summary>
	/// Add a new response bytes sequence to the response.
	/// 
	/// Detects if the sequence is a status bytes sequence 
	/// 	and sets the status bytes accordingly if so, 
	/// 	adding any remaining bytes after the status bytes 
	/// 	to the response data list.
	/// 	
	/// The bytes are appended as a new item in the data list.
	/// </summary>
	/// <param name="bytes">bytes</param>
	public void AddResponse(byte[] bytes) {

		// Response is a status byte sequence
		// TODO: Properly handle status; get error etc.
		if (CommandResponse.ResponseBytesAreStatus(bytes)) {

			// Store the status sequence
			this.Status = bytes;

			// Response may still contain data after status bytes
			if (bytes.Length > RESPONSE_STATUS_LENGTH) {
				this.Data.Add(bytes.Skip(RESPONSE_STATUS_LENGTH).ToArray());
			}

		}
		else this.Data.Add(bytes);
		System.Diagnostics.Debug.WriteLine("Adding");

	}


	/// <summary>
	/// Get the data associated with the response.
	/// 
	/// Returns the data bytes array in the `Data` list 
	/// 	of received data sequences at the given index.
	/// </summary>
	/// <param name="index">index</param>
	/// <returns>byte[]</returns>
	public byte[] GetData(int index=0) {
		return this.Data[index];
	}


	/// <summary>
	/// Get the data associated with the response as a `ByteStream`.
	/// 
	/// Returns a `ByteStream` for the data bytes array in 
	/// 	 the `Data` list of received data sequences 
	/// 	 at the given index.
	/// </summary>
	/// <param name="index">index</param>
	/// <returns>ByteStream</returns>
	public ByteStream GetDataStream(int index=0) {
		return new ByteStream(this.GetData(index));
	}


	/// <summary>
	/// Get whether an array of response bytes starts 
	/// 	with a Band status byte sequence.
	/// </summary>
	/// <param name="bytes">bytes</param>
	/// <returns>bool</returns>
	public static bool ResponseBytesAreStatus(byte[] bytes) {
		int[] ints = bytes.Select(b => (int) b).ToArray();
		return (ints[0] == 254 && ints[1] == 166);
	}

}

}