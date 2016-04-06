using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Ink.Runtime;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(GraphicPulser))]
public class ChoiceView : StoryElementView {
	public ChoiceGroupView choiceGroupView;
	public Choice choice;

	public Button button {
		get {
			return GetComponent<Button>();
		}
	}
	public GraphicPulser pulser {
		get {
			return GetComponent<GraphicPulser>();
		}
	}

	private FloatTween alphaTween = new FloatTween();
	private Vector2Tween positionTween = new Vector2Tween();


	protected override void Awake () {
		alphaTween.OnChange += ChangeAlphaTween;
		positionTween.OnChange += ChangePositionTween;
		text.color = new Color(1,1,1,0);
		button.interactable = false;
		pulser.enabled = false;
	}

	void ChangePositionTween (Vector2 currentValue) {
		rectTransform.anchoredPosition = currentValue;
	}

	void ChangeAlphaTween (float currentValue) {
		text.color = new Color(text.color.r, text.color.g, text.color.b, currentValue);
	}

	public void LayoutText (Choice choice) {
		this.choice = choice;
		base.LayoutText(choice.text);
	}

	public void Render () {
		text.text = content;
		base.LayoutText (content);
		StartCoroutine(FadeIn(2f));
	}

	public IEnumerator FadeIn (float fadeTime) {
		pulser.enabled = false;
		button.interactable = true;
		alphaTween.Tween(0, 1, fadeTime);
		positionTween.Tween(rectTransform.anchoredPosition + new Vector2(0,-20), rectTransform.anchoredPosition, fadeTime, new AnimationCurve(new Keyframe[] {new Keyframe(0,0,3.14f,3.14f), new Keyframe(1,1,0,0)}));
		while(alphaTween.tweening || positionTween.tweening) {
			if(alphaTween.tweening)
				alphaTween.Loop();
			if(positionTween.tweening)
				positionTween.Loop();
			yield return null;
		}
		pulser.enabled = true;
	}

	private void MakeChoice () {
		choiceGroupView.MakeChoice (choice);
	}

	public void OnClick () {
		MakeChoice();
	}

	public IEnumerator FadeOut (float fadeTime) {
		StopCoroutine("FadeIn");
		pulser.enabled = false;
		alphaTween.Tween(text.color.a, 0, fadeTime);
		while(alphaTween.tweening) {
			alphaTween.Loop();
			yield return null;
		}
	}

	protected override void Update () {
		if(Input.GetKeyDown((choice.index+1).ToString())) {
			MakeChoice();
		}
		base.Update();
	}
}