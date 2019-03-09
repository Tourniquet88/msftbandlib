namespace MSFTBandLib.Libs {

/**
 * Notification constants
 *
 * @static
 * @package MSFTBandLib
 * @subpackage Libs
 * @author James Walker
 * @copyright James Walker 2019
 * @license MIT
 */
public static class Notifications {

	/**
	 * SMS 
	 * 
	 * @static
	 * @readonly
	 * @var {byte[]}
	 */
	public static readonly byte[] SMS = new byte[] {0x01, 0x00};

	/**
	 * Email
	 * 
	 * @static
	 * @readonly
	 * @var {byte[]}
	 */
	public static readonly byte[] EMAIL = new byte[] {0x02, 0x00};

	/**
	 * Incoming call
	 * 
	 * @static
	 * @readonly
	 * @var {byte[]}
	 */
	public static readonly byte[] CALL_INCOMING = new byte[] {0x0b, 0x00};

	/**
	 * Answered call
	 * 
	 * @static
	 * @readonly
	 * @var {byte[]}
	 */
	public static readonly byte[] CALL_ANSWERED = new byte[] {0x0c, 0x00};

	/**
	 * Missed call
	 * 
	 * @static
	 * @readonly
	 * @var {byte[]}
	 */
	public static readonly byte[] CALL_MISSED = new byte[] {0x0d, 0x00};

	/**
	 * End call
	 * 
	 * @static
	 * @readonly
	 * @var {byte[]}
	 */
	public static readonly byte[] CALL_HANGUP = new byte[] {0x0e, 0x00};

	/**
	 * Voicemail
	 * 
	 * @static
	 * @readonly
	 * @var {byte[]}
	 */
	public static readonly byte[] VOICEMAIL = new byte[] {0x0f, 0x00};

	/**
	 * Add to calendar
	 * 
	 * @static
	 * @readonly
	 * @var {byte[]}
	 */
	public static readonly byte[] CALENDAR_ADD = new byte[] {0x10, 0x00};

	/**
	 * Clear calendar
	 * 
	 * @static
	 * @readonly
	 * @var {byte[]}
	 */
	public static readonly byte[] CALENDAR_CLEAR = new byte[] {0x11, 0x00};

	/**
	 * Message
	 * 
	 * @static
	 * @readonly
	 * @var {byte[]}
	 */
	public static readonly byte[] MESSAGE = new byte[] {0x12, 0x00};

	/**
	 * Generic dialog
	 * 
	 * @static
	 * @readonly
	 * @var {byte[]}
	 */
	public static readonly byte[] DIALOG = new byte[] {0x64, 0x00};

	/**
	 * Generic update
	 * 
	 * @static
	 * @readonly
	 * @var {byte[]}
	 */
	public static readonly byte[] UPDATE = new byte[] {0x65, 0x00};

	/**
	 * Clear a tile
	 * 
	 * @static
	 * @readonly
	 * @var {byte[]}
	 */
	public static readonly byte[] TILE_CLEAR = new byte[] {0x66, 0x00};

	/**
	 * Clear a page
	 * 
	 * @static
	 * @readonly
	 * @var {byte[]}
	 */
	public static readonly byte[] PAGE_CLEAR = new byte[] {0x67, 0x00};

}

}