using MSFTBandLib.Includes;
using System;
using System.Collections.Generic;

namespace MSFTBandLib.Time {

/// <summary>Time domain object</summary>
public class TimeDomain {

	/// <summary>Year</summary>
	public ushort Year;

	/// <summary>Month</summary>
	public ushort Month;

	/// <summary>Day</summary>
	public ushort Day;

	/// <summary>Hour</summary>
	public ushort Hour;

	/// <summary>Minute</summary>
	public ushort Minute;

	/// <summary>Second</summary>
	public ushort Second;


	/// <summary>
	/// Create a new time domain.
	/// 
	/// The `times` array must contain 6 values as
	/// 	YYYY, MM, DD, HH, MM, SS.
	/// </summary>
	/// <param name="times">Array of time values</param>
	/// <returns>public</returns>
	public TimeDomain(ushort[] times) {
		this.Year = times[0];
		this.Month = times[1];
		this.Day = times[2];
		this.Hour = times[3];
		this.Minute = times[4];
		this.Second = times[5];
	}


	/// <summary>Get as string.</summary>
	/// <returns>string</returns>
	public override string ToString() {
		return $"{Year}-{Month}-{Day} {Hour}:{Minute}:{Second}";
	}


	/// <summary>
	/// Create a new time instance from the `ByteArray` returned 
	/// 	from the `GetDeviceTime` Band command response. 
	/// </summary>
	/// <param name="bytes">bytes</param>
	/// <returns>TimeDomain</returns>
	public static TimeDomain CreateFromBandBytes(ByteArray bytes) {
		List<ushort> times = new List<ushort>(bytes.GetUshorts(8));
		times.RemoveAt(2);
		return new TimeDomain(times.ToArray());
	}

}

}