using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class ContentManager : UIMonoBehaviour, IBeginDragHandler {

	public RectTransform canvasRectTransform {
		get {
			return (RectTransform)GetComponentInParent<Canvas>().transform;
		}
	}

	public VerticalLayoutGroup layoutGroup;
	public ScrollRect scrollRect;

	public float minY;
	public float maxY;

	public Vector2Tween tween = new Vector2Tween();
	public Vector2 tweenRemaining {
		get {
			if(!tween.tweening) return Vector2.zero;
			return tween.targetValue - tween.currentValue;
		}
	}

	public Image paperImage;

	public float paperTopSpacingCanvasHeightFraction = 0.2f;
	public float paperBottomSpacingCanvasHeightFraction = 0.25f;
	public int paperBottomOffset = 100;

	private float targetY;
	private float lastLayoutGroupSize;

	bool contentLargerThanArea {
		get {
			return scrollRect.content.sizeDelta.y > canvasRectTransform.sizeDelta.y + paperBottomOffset;
		}
	}

	bool showChoices;
	public float scrollDistanceUntilHidingChoices = 100;

	private void Awake () {
		tween.OnChange += OnChange;
		ResetContent();
	}

	void OnChange (Vector2 currentValue) {
		scrollRect.content.anchoredPosition = currentValue;	
	}

	private void OnEnable () {
		ResetContent();
	}

	private void OnDisable () {
		ResetContent();
	}

	private void ResetContent () {
		rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 0);
		rectTransform.anchoredPosition = new Vector2(0, 0);
		lastLayoutGroupSize = ((RectTransform)layoutGroup.transform).sizeDelta.y;
		paperImage.rectTransform.anchoredPosition = new Vector2(paperImage.rectTransform.anchoredPosition.x, 0);
	}

	public void ScrollToBottom (float time = 1.4f) {
		Vector2 targetScroll = new Vector2(scrollRect.content.anchoredPosition.x, scrollRect.content.sizeDelta.y - paperBottomOffset);
		tween.Tween(scrollRect.content.anchoredPosition, targetScroll, time, AnimationCurve.EaseInOut(0,0,1,1));
	}

	void Update () {
		if (tween.tweening) {
			tween.Loop();
		}
	}


	void LateUpdate () {
		UpdateAutoScroll();
		UpdatePaperSize ();
		ClampScrollRect();

		bool newShowChoices = maxY - scrollDistanceUntilHidingChoices < scrollRect.content.anchoredPosition.y || tween.tweening || !contentLargerThanArea;
		if(showChoices != newShowChoices) {
			showChoices = newShowChoices;
			if(showChoices) {
				Main.Instance.gameState.choiceContainerView.FadeIn();
				Main.Instance.gameState.scrollToBottomButton.FadeOut();
			} else {
				Main.Instance.gameState.choiceContainerView.FadeOut();
				Main.Instance.gameState.scrollToBottomButton.FadeIn();
			}
		}
	}

	void UpdateAutoScroll () {
		float newLayoutGroupSize = ((RectTransform)layoutGroup.transform).sizeDelta.y;
		if(newLayoutGroupSize > lastLayoutGroupSize) {
			rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, newLayoutGroupSize);
			lastLayoutGroupSize = newLayoutGroupSize;
			ScrollToBottom();
		}
	}

	void UpdatePaperSize () {
		float canvasHeight = canvasRectTransform.sizeDelta.y;
		layoutGroup.padding = new RectOffset(layoutGroup.padding.left, layoutGroup.padding.right, Mathf.RoundToInt(canvasHeight * paperTopSpacingCanvasHeightFraction), Mathf.RoundToInt(canvasHeight * paperBottomSpacingCanvasHeightFraction) + paperBottomOffset);
		paperImage.rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, Mathf.Max(rectTransform.sizeDelta.y, paperImage.sprite.texture.height));
		paperImage.rectTransform.anchoredPosition = new Vector2(paperImage.rectTransform.anchoredPosition.x, paperImage.rectTransform.sizeDelta.y * 0.5f);
	}

	void ClampScrollRect () {
		scrollRect.vertical = contentLargerThanArea;
		if(contentLargerThanArea) {
			minY = canvasRectTransform.sizeDelta.y * 0.75f;
			maxY = scrollRect.content.sizeDelta.y - paperBottomOffset;
		} else {
			minY = 0;
			maxY = scrollRect.content.sizeDelta.y;
		}

		float newY = Mathf.Clamp(scrollRect.content.anchoredPosition.y, minY, maxY);
		scrollRect.content.anchoredPosition = new Vector2(scrollRect.content.anchoredPosition.x, newY);
		if (scrollRect.content.anchoredPosition.y >= maxY && scrollRect.velocity.y > 0)
			scrollRect.velocity = new Vector2(scrollRect.velocity.x, 0);
		else if (scrollRect.content.anchoredPosition.y <= minY && scrollRect.velocity.y < 0)
			scrollRect.velocity = new Vector2(scrollRect.velocity.x, 0);
	}

	public void OnBeginDrag (PointerEventData eventData) {
		if(contentLargerThanArea)
			tween.Stop();
	}

	public void OnDrag (PointerEventData eventData) {
		if(!contentLargerThanArea) 
			return;
		if (scrollRect.content.anchoredPosition.y >= maxY && eventData.delta.y > 0)
			return;
		else if (scrollRect.content.anchoredPosition.y <= minY && eventData.delta.y < 0)
			return;
	}
}