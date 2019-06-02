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
		string str = "";
		str += $"{this.Year}-{this.Month}-{this.Day}";
		str += " ";
		str += $"{this.Hour}:{this.Minute}:{this.Second}";
		return str;
	}

}

}