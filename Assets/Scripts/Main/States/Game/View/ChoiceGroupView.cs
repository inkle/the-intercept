using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;

public class ChoiceGroupView : UIMonoBehaviour {
	public List<ChoiceView> choiceViews;
	public ChoiceView choiceViewPrefab;

	public VerticalLayoutGroup verticalLayoutGroup {
		get {
			return GetComponent<VerticalLayoutGroup>();
		}
	}
	public ContentSizeFitter contentSizeFitter {
		get {
			return GetComponent<ContentSizeFitter>();
		}
	}

	public virtual void LayoutChoices (IList<Choice> choices) {
		foreach(Choice choice in choices) {
			LayoutChoice (choice);
		}
		verticalLayoutGroup.enabled = false;
		contentSizeFitter.enabled = false;
		foreach(ChoiceView choiceView in choiceViews) {
			choiceView.rectTransform.sizeDelta = new Vector2(choiceView.rectTransform.sizeDelta.x, choiceView.rectTransform.sizeDelta.y + verticalLayoutGroup.spacing * 0.5f);
		}
	}

	public virtual void RenderChoices () {
		StartCoroutine(RenderChoicesDelayed());
	}

	private IEnumerator RenderChoicesDelayed () {
		foreach(ChoiceView choiceView in choiceViews) {
			choiceView.Render();
			yield return new WaitForSeconds(0.4f);
		}
	}

	public ChoiceView LayoutChoice (Choice choice) {
		ChoiceView choiceView = Instantiate (choiceViewPrefab);
		choiceView.transform.SetParent (transform, false);
		choiceView.choiceGroupView = this;
		choiceView.LayoutText(choice);
		choiceViews.Add(choiceView);
		return choiceView;
	}

	public void MakeChoice (Choice choice) {
		StopAllCoroutines();
		StartCoroutine (FadeOutAndContinue (choice));
	}

	IEnumerator FadeOutAndContinue (Choice choice) {
		foreach (ChoiceView choiceView in choiceViews) {
			choiceView.StopAllCoroutines();
			if(choiceView.choice == choice)
				choiceView.button.interactable = false;
			else
				choiceView.button.enabled = false;
		}
		foreach(ChoiceView choiceView in choiceViews) {
			if(choiceView.choice != choice)
				StartCoroutine(choiceView.FadeOut(0.4f));
		}
		if(choiceViews.Count > 1)
			yield return new WaitForSeconds (0.25f);
		foreach(ChoiceView choiceView in choiceViews) {
			if(choiceView.choice == choice)
				yield return StartCoroutine(choiceView.FadeOut(0.6f));
		}
		foreach (ChoiceView choiceView in choiceViews) {
			Destroy (choiceView.gameObject);
		}
		yield return new WaitForSeconds (0.25f);
		Destroy (gameObject);
		Main.Instance.gameState.ChooseChoiceIndex(choice.index);
	}
}
