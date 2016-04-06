using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class IntroTypedText : UIMonoBehaviour {
	public string content;
	public Vector2 letterTypeTime = new Vector2(0.1f, 0.1f);
	public TypedText typedText;
	private Text text {
		get {
			return GetComponent<Text>();
		}
	}

	private void OnEnable () {
		TypedText.TypedTextSettings typedTextSettings = new TypedText.TypedTextSettings();
		typedTextSettings.splitMode = TypedText.TypedTextSettings.SplitMode.Character;
		typedTextSettings.defaultTypeDelay = new TypedText.RandomTimeDelay(letterTypeTime.x, letterTypeTime.y);

		typedText = new TypedText();
		typedText.TypeText(content, typedTextSettings);
		typedText.OnTypeText += OnTypeText;
		text.text = "";

		Canvas.ForceUpdateCanvases();

		TextGenerator textGenerator = new TextGenerator();
		TextGenerationSettings textGeneratorSettings = text.GetGenerationSettings(new Vector2(0, 100));
		textGeneratorSettings.updateBounds = true;
		textGeneratorSettings.scaleFactor = 1;
		textGenerator.Populate(content, textGeneratorSettings);
		rectTransform.sizeDelta = new Vector2(textGenerator.rectExtents.width * 2, rectTransform.sizeDelta.y);
		text.alignment = TextAnchor.MiddleLeft;
	}

	private void OnDisable () {
		text.text = "";
	}

	void OnTypeText (string newText) {
		if(newText != " ")
			AudioClipDatabase.Instance.PlayKeySound();
	}

	private void Update () {
		typedText.Loop();
		text.text = typedText.text;
	}
}