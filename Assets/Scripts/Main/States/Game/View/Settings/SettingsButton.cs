using UnityEngine;
using System.Collections;

public class SettingsButton : MonoBehaviour {

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
		if(Input.GetKeyDown(KeyCode.Escape)) {
			Main.Instance.paused = !Main.Instance.paused;
		}
	}

	public void FadeIn () {
		alphaTween.Tween(0, 1, 5, AnimationCurve.EaseInOut(0,0,1,1));
	}

	public void Show () {
		canvasGroup.alpha = 1;
	}

	public void Hide () {
		alphaTween.Stop();
		canvasGroup.alpha = 0;
	}

	public void OnClickSettingsButton () {
		Main.Instance.paused = true;
	}
}
