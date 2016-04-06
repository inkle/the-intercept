using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Ink.Runtime;

[RequireComponent(typeof(Button))]
public class ChevronButtonView : StoryElementView {

	public Image image {
		get {
			return GetComponent<Image>();
		}
	}
	public Button button {
		get {
			return GetComponent<Button>();
		}
	}

	protected override void OnChangeColor (Color currentValue) {
		image.color = currentValue;
	}

	public void Render () {
		StartCoroutine(FadeIn(0.5f));
	}

	public void OnClick () {
		StartCoroutine(FadeOut(0.5f));
		Main.Instance.gameState.ClickChevronButton();
	}

	private IEnumerator FadeIn (float fadeTime) {
		button.interactable = false;
		colorTween.Tween(new Color(1,1,1,0), new Color(image.color.r, image.color.g, image.color.b, 1), fadeTime);
		while(colorTween.tweening) {
			colorTween.Loop();
			yield return null;
		}
		button.interactable = true;
	}

	private IEnumerator FadeOut (float fadeTime) {
		colorTween.Tween(image.color, new Color(image.color.r, image.color.g, image.color.b, 0), fadeTime);
		while(colorTween.tweening) {
			colorTween.Loop();
			yield return null;
		}
	}

	protected override void Update () {
		if(Input.GetKeyDown("0") || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) {
			OnClick();
		}
		base.Update();
	}
}