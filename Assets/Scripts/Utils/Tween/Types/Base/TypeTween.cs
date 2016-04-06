using UnityEngine;

public abstract class TypeTween<T> {
	
	private T _currentValue;
	public T currentValue {
		get {
			return _currentValue;
		} set {
			_currentValue = value;
			ChangedCurrentValue();
		}
	}
	public T deltaValue;
	
	public bool tweening = false;
	public T targetValue;
	public T startValue;
	public Timer tweenTimer;
	
	public AnimationCurve easingCurve;
	
	public delegate void OnChangeEvent(T currentValue);
	public event OnChangeEvent OnChange;
	
	//Called when a new tween is started while another is in progress.
	public delegate void OnInterruptEvent();
	public event OnInterruptEvent OnInterrupt;
	
	public delegate void OnCompleteEvent();
	public event OnCompleteEvent OnComplete;
	
	public TypeTween () {
		
	}
	
	public TypeTween (T myCurrentValue) {
		currentValue = myCurrentValue;
	}
	
	public TypeTween (T myStartValue, T myTargetValue, float myTweenTime) {
		Tween(myStartValue, myTargetValue, myTweenTime);
	}
	
	public TypeTween (T myStartValue, T myTargetValue, float myTweenTime, AnimationCurve _easingCurve) {
		Tween(myStartValue, myTargetValue, myTweenTime, _easingCurve);
	}
	
	public void Reset () {
		Reset(default(T));
	}
	
	public void Reset (T myCurrentValue) {
		Stop();
		currentValue = myCurrentValue;
	}
	
	public void Reset (T myStartValue, T myTargetValue, float myTweenTime) {
		Stop();
		Tween(myStartValue, myTargetValue, myTweenTime);
	}
	
	public void Reset (T myStartValue, T myTargetValue, float myTweenTime, AnimationCurve _easingCurve) {
		Stop();
		Tween(myStartValue, myTargetValue, myTweenTime, _easingCurve);
	}
	
	/// <summary>
	/// Starts a new tween between the current value and the target value over a specified time
	/// </summary>
	/// <param name="myTargetValue">The value to tween towards.</param>
	/// <param name="myTargetTime">The time over which the tween will occur.</param>
	public virtual void Tween(T myTargetValue, float myTweenTime){
		Tween(currentValue, myTargetValue, myTweenTime);
	}
	
	/// <summary>
	/// Starts a new tween between the current value and the target value over a specified time, using an easing curve
	/// </summary>
	/// <param name="myTargetValue">The value to tween towards.</param>
	/// <param name="myTargetTime">The time over which the tween will occur.</param>
	/// <param name="myLerpCurve">The easing curve fro the tween</param>
	public virtual void Tween(T myTargetValue, float myTweenTime, AnimationCurve myLerpCurve){
		Tween(currentValue, myTargetValue, myTweenTime, myLerpCurve);
	}
	
	/// <summary>
	/// Starts a new tween between the start value and the target value over a specified time
	/// </summary>
	/// <param name="myStartValue">The start value of the tween.</param>
	/// <param name="myTargetValue">The value to tween towards.</param>
	/// <param name="myTargetTime">The time over which the tween will occur.</param>
	public virtual void Tween(T myStartValue, T myTargetValue, float myTweenTime){
		Tween(myStartValue, myTargetValue, myTweenTime, AnimationCurve.Linear(0, 0, 1, 1));
	}
	
	/// <summary>
	/// Starts a new tween between the start value and the target value over a specified time, using an easing curve
	/// </summary>
	/// <param name="myStartValue">The start value of the tween.</param>
	/// <param name="myTargetValue">The value to tween towards.</param>
	/// <param name="myTargetTime">The time over which the tween will occur.</param>
	/// <param name="myLerpCurve">The easing curve fro the tween</param>
	public virtual void Tween(T myStartValue, T myTargetValue, float myTweenTime, AnimationCurve myLerpCurve){
		if(tweening) {
			Interrupt();
		}
		
		if (myTweenTime > 0.0f) {
			tweenTimer = new Timer(myTweenTime);
			tweenTimer.OnComplete += TweenComplete;
			tweenTimer.Start();
			
			deltaValue = default(T);
			currentValue = myStartValue;
			startValue = myStartValue;
			targetValue = myTargetValue;
			easingCurve = myLerpCurve;
			tweening = true;
		} else {
			currentValue = myTargetValue;
			startValue = myStartValue;
			targetValue = myTargetValue;
		}
		
	}
	
	/// <summary>
	/// Updates the tween with deltaTime
	/// </summary>
	public virtual T Loop () {
		return Loop(Time.deltaTime);
	}
	
	/// <summary>
	/// Updates the tween with a custom deltaTime
	/// </summary>
	public virtual T Loop (float myDeltaTime) {
		if(tweening){
			SetValue(GetValueAtNormalizedTime(tweenTimer.GetNormalizedTime()));
			tweenTimer.Loop(myDeltaTime);
		}
		return currentValue;
	}
	
	/// <summary>
	/// Called when the tween is completed
	/// </summary>
	protected virtual void TweenComplete () {
		SetValue(GetValueAtNormalizedTime(1));
		if(OnComplete != null) OnComplete();
		Stop();
	}
	
	/// <summary>
	/// Stop tweening
	/// </summary>
	public virtual void Stop () {
		tweening = false;
		deltaValue = default(T);
		startValue = default(T);
		currentValue = default(T);
		targetValue = default(T);
	}
	
	public virtual void Interrupt () {
		Stop ();
		if(OnInterrupt != null) OnInterrupt();
	}
	
	/// <summary>
	/// Returns the current value of the tween at a specified time
	/// </summary>
	public T GetValueAtTime(float myTime){
		return GetValueAtNormalizedTime(myTime/tweenTimer.targetTime);
	}
	
	/// <summary>
	/// Returns the current value of the tween at a normalized specified time
	/// </summary>
	public T GetValueAtNormalizedTime(float myNormalizedTime){
		return Lerp(startValue, targetValue, myNormalizedTime);
	}
	
	protected abstract T Lerp (T myLastTarget, T myTarget, float lerp);
	
	/// <summary>
	/// Sets the current value of the tween
	/// </summary>
	protected virtual void SetValue (T myValue) {
		T lastValue = currentValue;
		currentValue = myValue;
		SetDeltaValue(lastValue, currentValue);
	}
	
	protected abstract void SetDeltaValue (T myLastValue, T myCurrentValue);
	
	protected virtual void ChangedCurrentValue () {
		if(!tweening) 
			return;
		if(OnChange != null) OnChange(currentValue);
	}
}