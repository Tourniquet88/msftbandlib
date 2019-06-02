using System;
using System.IO;

namespace MSFTBandLib.Includes {

/// <summary>
/// Byte array include
/// 
/// This is a thin wrapper around `MemoryStream` and `BinaryWriter`; 
/// 	you should write directly by accessing `BinaryWriter` 
/// 	(avoid having to reimplement all `BinaryWriter.Write(...)` 
/// 	overloads due to types).
/// </summary>
public class ByteArray : IDisposable {

	///	<summary>Disposed</summary>
	public bool disposed  { get; protected set; }

	/// <summary>Memory stream</summary>
	public MemoryStream MemoryStream;

	/// <summary>Binary writer</summary>
	public BinaryWriter BinaryWriter;


	/// <summary>Construct.</summary>
	public ByteArray() {
		this.MemoryStream = new MemoryStream();
		this.BinaryWriter = new BinaryWriter(this.MemoryStream);
	}


	/// <summary>Dispose of the resources.</summary>
	public void Dispose() {
		this.Dispose(true);
		GC.SuppressFinalize(this);
	}


	/// <summary>Dispose of the resources.</summary>
	/// <param name="disposing">Disposing (not used)</param>
	protected virtual void Dispose(bool disposing) {
		this.BinaryWriter.Dispose();
		this.MemoryStream.Dispose();
		this.disposed = true;
	}


	/// <summary>Get the current byte array.</summary>
	/// <returns>byte[]</returns>
	public byte[] GetBytes() {
		return this.MemoryStream.ToArray();
	}

}

}