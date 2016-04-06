using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoryElementView : UIMonoBehaviour {
	public LayoutElement layoutElement {
		get {
			return GetComponent<LayoutElement>();
		}
	}
	public Text text {
		get {
			return GetComponent<Text>();
		}
	}
	public string content;


	public ColorTween colorTween = new ColorTween();
	public delegate void OnFinishReadingEvent();
	public event OnFinishReadingEvent OnFinishReading;

	protected virtual void Awake () {
		colorTween.OnChange += OnChangeColor;
	}

	protected virtual void OnChangeColor (Color currentValue) {
		text.color = currentValue;
	}

	protected virtual void Update () {
		if(colorTween.tweening) {
			colorTween.Loop();
		}
	}

	public virtual void LayoutText (string content) {
		this.content = content.Trim();
		Canvas.ForceUpdateCanvases();

		TextGenerator generator = new TextGenerator();
		TextGenerationSettings settings = text.GetGenerationSettings(new Vector2(rectTransform.sizeDelta.x, 0));
		settings.updateBounds = true;
		settings.scaleFactor = 1;
		generator.Populate(content, settings);
		layoutElement.preferredHeight = generator.rectExtents.height;
	}

	protected virtual void CompleteTyping () {
		if(OnFinishReading != null)
			OnFinishReading();
	}
}
