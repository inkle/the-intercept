using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntroState : MainState {

	public CanvasGroup group;
	public IntroTypedText inklePresentsText;
	public IntroTypedText theInterceptText;

	public override void Enter () {
		group.gameObject.SetActive(true);
		group.alpha = 1;
		group.interactable = true;
		group.blocksRaycasts = true;
		inklePresentsText.gameObject.SetActive(false);
		theInterceptText.gameObject.SetActive(false);
		group.gameObject.SetActive(true);
		base.Enter ();
	}

	public override void Exit () {
		group.interactable = false;
		group.blocksRaycasts = false;
//		group.gameObject.SetActive(false);
		base.Exit ();
	}

	public void PlayLongIntro () {
		StartCoroutine(DoLongIntro());
	}

	public void PlayShortIntro () {
		StartCoroutine(DoShortIntro());
	}

	// Type Inkle, type game title, and fade in from black
	private IEnumerator DoLongIntro () {
		yield return new WaitForSeconds(2f);

		inklePresentsText.gameObject.SetActive(true);
		while(inklePresentsText.typedText.typing) {
			yield return null;
		}
		yield return new WaitForSeconds(2f);
		inklePresentsText.gameObject.SetActive(false);

		yield return new WaitForSeconds(2);



		theInterceptText.gameObject.SetActive(true);
		while(theInterceptText.typedText.typing) {
			yield return null;
		}
		AudioClipDatabase.Instance.PlayHorrorSting();
		yield return new WaitForSeconds(2.5f);
		theInterceptText.gameObject.SetActive(false);

		yield return new WaitForSeconds(0.5f);
		yield return StartCoroutine(DoShortIntro());
	}

	// Just fade in from black
	private IEnumerator DoShortIntro () {
		yield return new WaitForSeconds(0.5f);
		AudioClipDatabase.Instance.PlayAttachingPaperSound();
		yield return new WaitForSeconds(1);

		FloatTween opacityTween = new FloatTween();
		opacityTween.Tween(1, 0, 5);
		while(opacityTween.tweening) {
			opacityTween.Loop();
			group.alpha = opacityTween.currentValue;
			if(Main.Instance.currentState == this && opacityTween.tweenTimer.GetNormalizedTime() > 0.45f) {
				Complete();
			}
			yield return null;
		}
	}
}

