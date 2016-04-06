using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect)), ExecuteInEditMode]
public class EnforceScrollRectClampingBehaviour : MonoBehaviour {
	
	private ScrollRect scrollRect;
	public bool resetVelocityOnClamp = true;

	void OnEnable () {
		scrollRect = GetComponent<ScrollRect>();
	}
	
	void LateUpdate () {
		if (scrollRect.horizontalNormalizedPosition > 1) {
			scrollRect.horizontalNormalizedPosition = 1;
		} else if (scrollRect.horizontalNormalizedPosition < 0) {
			scrollRect.horizontalNormalizedPosition = 0;
		}
		if (scrollRect.verticalNormalizedPosition > 1) {
			scrollRect.verticalNormalizedPosition = 1;
		} else if (scrollRect.verticalNormalizedPosition < 0) {
			scrollRect.verticalNormalizedPosition = 0;
		}

		if (resetVelocityOnClamp) {
			if (scrollRect.horizontalNormalizedPosition >= 1 && scrollRect.velocity.x < 0)
				scrollRect.velocity = new Vector2(0, scrollRect.velocity.y);
			else if (scrollRect.horizontalNormalizedPosition <= 0 && scrollRect.velocity.x > 0)
				scrollRect.velocity = new Vector2(0, scrollRect.velocity.y);
			
			if (scrollRect.verticalNormalizedPosition >= 1 && scrollRect.velocity.y < 0)
				scrollRect.velocity = new Vector2(scrollRect.velocity.x, 0);
			else if (scrollRect.verticalNormalizedPosition <= 0 && scrollRect.velocity.y > 0)
				scrollRect.velocity = new Vector2(scrollRect.velocity.x, 0);
		}
	}
}
