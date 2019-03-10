namespace MSFTBandLib.Libs {

/// <summary>
/// Notification IDs
/// </summary>
public static class Notifications {

	/// <summary>SMS</summary>
	public static readonly byte[] SMS = new byte[] {0x01, 0x00};

	/// <summary>Email</summary>
	public static readonly byte[] EMAIL = new byte[] {0x02, 0x00};

	/// <summary>Incoming call</summary>
	public static readonly byte[] CALL_INCOMING = new byte[] {0x0b, 0x00};

	/// <summary>Answered call</summary>
	public static readonly byte[] CALL_ANSWERED = new byte[] {0x0c, 0x00};

	/// <summary>Missed call</summary>
	public static readonly byte[] CALL_MISSED = new byte[] {0x0d, 0x00};

	/// <summary>End call</summary>
	public static readonly byte[] CALL_HANGUP = new byte[] {0x0e, 0x00};

	/// <summary>Voicemail</summary>
	public static readonly byte[] VOICEMAIL = new byte[] {0x0f, 0x00};

	/// <summary>Add to calendar</summary>
	public static readonly byte[] CALENDAR_ADD = new byte[] {0x10, 0x00};

	/// <summary>Clear calendar</summary>
	public static readonly byte[] CALENDAR_CLEAR = new byte[] {0x11, 0x00};

	/// <summary>Message</summary>
	public static readonly byte[] MESSAGE = new byte[] {0x12, 0x00};

	/// <summary>Generic dialog</summary>
	public static readonly byte[] DIALOG = new byte[] {0x64, 0x00};

	/// <summary>Generic update</summary>
	public static readonly byte[] UPDATE = new byte[] {0x65, 0x00};

	/// <summary>Clear a tile</summary>
	public static readonly byte[] TILE_CLEAR = new byte[] {0x66, 0x00};

	/// <summary>Clear a page</summary>
	public static readonly byte[] PAGE_CLEAR = new byte[] {0x67, 0x00};

}

}