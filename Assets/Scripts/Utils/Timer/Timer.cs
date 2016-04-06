using UnityEngine;
using System.Collections;

[System.Serializable]
/// <summary>
/// Timer class.
/// </summary>
public class Timer {
	public delegate void OnStartEvent();
	public delegate void OnRepeatEvent();
	public delegate void OnCompleteEvent();

	public TimerState timerState = TimerState.Stopped;

	public float currentTime = 0f;
	public bool useTargetTime = true;
	[SerializeField]
	private float _targetTime = 0f;
	public float targetTime {
		get {
			return _targetTime;
		}
		set {
			_targetTime = value;
		}
	}

	public int currentRepeats = 0;
	public int targetRepeats = 1;
	public bool repeatForever = false;

	public event OnStartEvent OnStart;
	public event OnRepeatEvent OnRepeat;
	public event OnCompleteEvent OnComplete;

	public Timer () {
		useTargetTime = false;
	}

	public Timer (float myTargetTime) {
		Set(myTargetTime);
	}

	public Timer (float myTargetTime, int myTargetRepeats) {
		Set(myTargetTime, myTargetRepeats);
	}

	public Timer (float myTargetTime, bool myRepeatForever) {
		Set(myTargetTime, myRepeatForever);
	}

	public virtual void Set (float myTargetTime, int myTargetRepeats = 1) {
		targetTime = myTargetTime;
		targetRepeats = myTargetRepeats;
		useTargetTime = (targetTime >= 0 && targetRepeats > 0);
	}

	public virtual void Set (float myTargetTime, bool myRepeatForever) {
		targetTime = myTargetTime;
		repeatForever = myRepeatForever;
		useTargetTime = (targetTime > 0);
	}

	/// <summary>
	/// Starts the timer
	/// </summary>
	public virtual void Start () {
		timerState = TimerState.Playing;
		if(OnStart != null) OnStart();
	}

	/// <summary>
	/// Pauses the updating of the timer
	/// </summary>
	public virtual void Pause () {
		timerState = TimerState.Paused;
	}

	/// <summary>
	/// Stops the timer, resetting it.
	/// </summary>
	public virtual void Stop () {
		Reset();
		timerState = TimerState.Stopped;
	}

	/// <summary>
	/// Resets the time and repeat count. Does not change the play state.
	/// </summary>
	public virtual void Reset () {
		currentTime = 0;
		currentRepeats = 0;
	}

	/// <summary>
	/// Update the timer using the delta time.
	/// </summary>
	public virtual void Loop(){
		Loop(Time.deltaTime);
	}

	/// <summary>
	/// Update the timer using a given delta time.
	/// </summary>
	public virtual void Loop(float _deltaTime){
		if(timerState == TimerState.Playing) {
			UpdateTimer(_deltaTime);
		}
	}

	/// <summary>
	/// Returns the normalized time, between the range 0,1. Does not take repeats into account.
	/// </summary>
	public virtual float GetNormalizedTime () {
		return currentTime/targetTime;
	}

	/// <summary>
	/// Update the timer using a given delta time.
	/// </summary>
	protected virtual void UpdateTimer (float _deltaTime) {
		currentTime += _deltaTime;
		if(useTargetTime && currentTime > targetTime) {
			ReachTargetTime();
		}
	}

	/// <summary>
	/// Called when the current time reaches the target time.
	/// </summary>
	protected virtual void ReachTargetTime () {
		currentRepeats++;
		if(currentRepeats < targetRepeats || repeatForever) {
			currentTime = 0;
			if(OnRepeat != null)OnRepeat();
		} else {
			Stop();
			if(OnComplete != null)OnComplete();
		}
	}
	
	public override string ToString () {
		return string.Format("{0}: State: {1}, Time: {2}, Repeats: {3}", GetType(), timerState, currentTime, currentRepeats);
	}
}
