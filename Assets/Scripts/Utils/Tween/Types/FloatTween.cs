using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloatTween : TypeTween<float> {

	public FloatTween () : base () {}
	public FloatTween (float myCurrentValue) : base (myCurrentValue) {}
	public FloatTween (float myStartValue, float myTargetValue, float myLength) : base (myStartValue, myTargetValue, myLength) {}
	public FloatTween (float myStartValue, float myTargetValue, float myLength, AnimationCurve myLerpCurve) : base (myStartValue, myTargetValue, myLength, myLerpCurve) {}

	protected override float Lerp (float myLastTarget, float myTarget, float myLerp) {
		return Mathf.Lerp(myLastTarget, myTarget, easingCurve.Evaluate(myLerp));
	}

	protected override void SetDeltaValue (float myLastValue, float myCurrentValue) {
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