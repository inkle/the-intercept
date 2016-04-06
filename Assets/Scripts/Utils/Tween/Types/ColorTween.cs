using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColorTween : TypeTween<Color> {

	public ColorTween () : base () {}
	public ColorTween (Color myStartValue) : base (myStartValue) {}
	public ColorTween (Color myStartValue, Color myTargetValue, float myLength) : base (myStartValue, myTargetValue, myLength) {}
	public ColorTween (Color myStartValue, Color myTargetValue, float myLength, AnimationCurve myLerpCurve) : base (myStartValue, myTargetValue, myLength, myLerpCurve) {}

	protected override Color Lerp (Color myLastTarget, Color myTarget, float myLerp) {
		return Color.Lerp(myLastTarget, myTarget, easingCurve.Evaluate(myLerp));
	}

	protected override void SetDeltaValue (Color myLastValue, Color myCurrentValue) {
		deltaValue = myCurrentValue - myLastValue;
	}
	
	//----------------- IOS GENERIC INHERITANCE EVENT CRASH BUG WORKAROUND ------------------
	//http://angrytroglodyte.net/cave/index.php/blog/11-unity-ios-doesn-t-like-generic-events
	public new event OnChangeEvent OnChange;
	public new event OnInterruptEvent OnInterrupt;
	public new event OnCompleteEvent OnComplete;
	
	protected override void ChangedCurrentValue () {
//		base.ChangedCurrentValue();
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