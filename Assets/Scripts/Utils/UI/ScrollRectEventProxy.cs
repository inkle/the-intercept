using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(ScrollRect))]
public class ScrollRectEventProxy : MonoBehaviour, IBeginDragHandler, IDragHandler {

	public ContentManager contentManager;

	public void OnBeginDrag (PointerEventData eventData) {
		contentManager.OnBeginDrag(eventData);	
	}

	public void OnDrag (PointerEventData eventData) {
		contentManager.OnDrag(eventData);
	}
}
