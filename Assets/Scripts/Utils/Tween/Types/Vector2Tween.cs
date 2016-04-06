using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Vector2Tween : TypeTween<Vector2> {

	public Vector2Tween () : base () {}
	public Vector2Tween (Vector2 myStartValue) : base (myStartValue) {}
	public Vector2Tween (Vector2 myStartValue, Vector2 myTargetValue, float myLength) : base (myStartValue, myTargetValue, myLength) {}
	public Vector2Tween (Vector2 myStartValue, Vector2 myTargetValue, float myLength, AnimationCurve myLerpCurve) : base (myStartValue, myTargetValue, myLength, myLerpCurve) {}

	protected override Vector2 Lerp (Vector2 myLastTarget, Vector2 myTarget, float lerp) {
		return Vector2.Lerp(startValue, targetValue, easingCurve.Evaluate(lerp));
	}

	protected override void SetDeltaValue (Vector2 myLastValue, Vector2 myCurrentValue) {
		deltaValue = myCurrentValue - myLastValue;
	}
	
	//----------------- IOS GENERIC INHERITANCE EVENT CRASH BUG WORKAROUND ------------------
	//http://angrytroglodyte.net/cave/index.php/blog/11-unity-ios-doesn-t-like-generic-events
	public new event OnChangeEvent OnChange;
	public new event OnInterruptEvent OnInterrupt;
	public new event OnCompleteEvent OnComplete;
	
	protected override void ChangedCurrentValue () {
		base.ChangedCurrentValue();
		if(!tweening) 
			return;
		if(OnChange != null) OnChange(currentValue);
	}
	
	protected override void TweenComplete () {
		base.TweenComplete();
		if(OnComplete != null)OnComplete();
	}
	
	public override void Interrupt () {
		base.Interrupt();
		if(OnInterrupt != null) OnInterrupt();
	}
}