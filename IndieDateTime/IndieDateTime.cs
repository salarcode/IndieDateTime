﻿namespace System;

/// <summary>
///		Provides independent DateTime by calcuating the offset to the server date time
/// </summary>
public class IndieDateTime
{
	private double _offset;

	/// <summary>
	/// Provides shared IndieDateTime version
	/// </summary>
	public static IndieDateTime Shared { get; } = new();

	/// <summary>
	/// 
	/// </summary>
	public IndieDateTime()
	{
		_offset = 0;
	}

	/// <summary>
	/// Gets a System.DateTime object that is set to the independent date time.
	/// </summary>
	public DateTime Now
	{
		get
		{
			if (_offset == 0)
				return DateTime.Now;

			return DateTime.Now.AddMilliseconds(_offset);
		}
	}

	/// <summary>
	/// Gets a System.DateTimeOffset object that is set to the independent date time.
	/// </summary>
	public DateTimeOffset NowDto
	{
		get
		{
			if (_offset == 0)
				return DateTimeOffset.Now;

			return DateTimeOffset.Now.AddMilliseconds(_offset);
		}
	}

	/// <summary>
	/// Sync date time, will calculate the offset
	/// </summary>
	/// <param name="serverDatetime">The server data time</param>
	/// <param name="toleranceMs">How much of difference in MS is allowed to be captured</param>
	public void Sync(DateTime serverDatetime, uint toleranceMs = 0)
	{
		var diff = DateTime.Now - serverDatetime;
		if (toleranceMs > 0)
		{
			if (diff.TotalMilliseconds > toleranceMs)
			{
				_offset = diff.TotalMilliseconds;
			}
		}
		else
		{
			_offset = diff.TotalMilliseconds;
		}
	}

	/// <summary>
	/// Sync date time, will calculate the offset
	/// </summary>
	/// <param name="serverDatetime">The server data time</param>
	/// <param name="toleranceMs">How much of difference in MS is allowed to be captured</param>
	public void Sync(DateTimeOffset serverDatetime, uint toleranceMs = 0)
	{
		var diff = DateTimeOffset.Now - serverDatetime;
		if (toleranceMs > 0)
		{
			if (diff.TotalMilliseconds > toleranceMs)
			{
				_offset = diff.TotalMilliseconds;
			}
		}
		else
		{
			_offset = diff.TotalMilliseconds;
		}
	}
}
