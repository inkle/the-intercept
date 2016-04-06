using UnityEngine;
using System.Collections;

public class ChoiceGroupContainerView : MonoBehaviour {

	public FloatTween alphaTween = new FloatTween();

	private CanvasGroup canvasGroup {
		get {
			return GetComponent<CanvasGroup>();
		}
	}

	public void Clear () {
		for (int i = transform.childCount-1; i >= 0; i--) {
			Destroy(transform.GetChild(i).gameObject);
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
}
