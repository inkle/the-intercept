using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class EndView : MonoBehaviour {

	private CanvasGroup canvasGroup {
		get {
			return GetComponent<CanvasGroup>();
		}
	}

	public FloatTween alphaTween = new FloatTween();

	public void Init () {
		Hide();
		alphaTween.OnChange += OnChangeAlphaTween;
	}

	void OnChangeAlphaTween (float currentValue) {
		canvasGroup.alpha = alphaTween.currentValue;
	}

	void Update () {
		alphaTween.Loop();
	}

	public void Show () {
		canvasGroup.alpha = 0;
		alphaTween.Tween(canvasGroup.alpha, 1, 2, AnimationCurve.EaseInOut(0,0,1,1));
		gameObject.SetActive(true);
	}

	public void Hide () {
		canvasGroup.alpha = 0;
		gameObject.SetActive(false);
	}
}