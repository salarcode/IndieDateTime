using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace System
{
	/// <summary>
	/// Provides independent DateTime
	/// </summary>
	public class IndieDateTime
	{
		private TimeSpan _passedTime;
		private DateTime _baseSyncDatetime;
		private Timer _syncTimer;
		private readonly int _syncIntervalMs;

		public IndieDateTime()
			: this(500)
		{ }

		/// <param name="updateInterval">Time update interval in milliseconds. Default is 500 ms.</param>
		public IndieDateTime(int updateInterval)
		{
			if (updateInterval <= 0)
			{
				throw new ArgumentOutOfRangeException("updateInterval", "Update interval should be greater than zero.");
			}
			_syncIntervalMs = updateInterval;
			_passedTime = new TimeSpan();
			_baseSyncDatetime = DateTime.Now;
			_syncTimer = new Timer(SyncTimerCallback, null, _syncIntervalMs, _syncIntervalMs);
		}
		~IndieDateTime()
		{
			if (_syncTimer != null)
			{
				_syncTimer.Dispose();
			}
			_syncTimer = null;
		}


		void SyncTimerCallback(object state)
		{
			// sync the passed time
			_passedTime = _passedTime.Add(new TimeSpan(0, 0, 0, _syncIntervalMs));
		}

		/// <summary>
		/// Sync date time, this dateTime will be used as the base.
		/// </summary>
		public void Sync(DateTime baseSyncDatetime)
		{
			_passedTime = new TimeSpan();
			_baseSyncDatetime = baseSyncDatetime;

			// reset the timer!
			_syncTimer.Change(_syncIntervalMs, _syncIntervalMs);
		}

		/// <summary>
		/// Gets a System.DateTime object that is set to the independent date time.
		/// </summary>
		public DateTime Now
		{
			get { return _baseSyncDatetime.Add(_passedTime); }
		}
	}
}
