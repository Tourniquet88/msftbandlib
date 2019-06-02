using System;
using System.Collections.Generic;
using MSFTBandLib.Command;

namespace MSFTBandLib.Time {

/// <summary>Time response class</summary>
public class TimeResponse {

	/// <summary>Base response</summary>
	public CommandResponse CommandResponse;


	/// <summary>Create from `CommandResponse`.</summary>
	/// <param name="CommandResponse">CommandResponse</param>
	/// <returns>public</returns>
	public TimeResponse(CommandResponse CommandResponse) {
		this.CommandResponse = CommandResponse;
	}


	/// <summary>
	/// Get time data from response.
	/// 
	/// Returns an array of `ushort`:
	/// 	- 0 => year (YYYY)
	/// 	- 1 => month (M)
	/// 	- 2 => unknown
	/// 	- 3 => day of month (D)
	/// 	- 4 => hour (H)
	/// 	- 5 => minute (M)
	/// 	- 6 => second (S)
	/// 	- 7 => unknown, ms?
	/// </summary>
	/// <returns>ushort[]</returns>
	public ushort[] GetTimeData() {
		return this.CommandResponse.GetDataStream().GetUshorts(8);
	}


	/// <summary>Create a new `TimeDomain` from the response data.</summary>
	/// <returns>TimeDomain</returns>
	public TimeDomain CreateTimeDomain() {
		List<ushort> TimeData = new List<ushort>(this.GetTimeData());
		TimeData.RemoveAt(2);
		return new TimeDomain(TimeData.ToArray());
	}

}

}