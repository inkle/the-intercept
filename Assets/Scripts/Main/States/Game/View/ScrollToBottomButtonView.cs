using UnityEngine;
using System.Collections;

public class ScrollToBottomButtonView : MonoBehaviour {

	public FloatTween alphaTween = new FloatTween();

	private CanvasGroup canvasGroup {
		get {
			return GetComponent<CanvasGroup>();
		}
	}

	private void Awake () {
		alphaTween.OnChange += OnChangeAlphaTween;
	}

	void OnChangeAlphaTween (float currentValue) {
		canvasGroup.alpha = currentValue;
	}

	private void Update () {
		alphaTween.Loop();
	}

	public void FadeIn () {
		alphaTween.Tween(canvasGroup.alpha, 1, 0.4f, AnimationCurve.EaseInOut(0,0,1,1));
	}

	public void FadeOut () {
		alphaTween.Tween(canvasGroup.alpha, 0, 0.2f, AnimationCurve.EaseInOut(0,0,1,1));
	}

	public void OnClick () {
		Main.Instance.gameState.contentManager.ScrollToBottom(1f);
	}
}
