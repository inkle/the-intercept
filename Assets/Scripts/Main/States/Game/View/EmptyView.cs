using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EmptyView : UIMonoBehaviour {

	public LayoutElement layoutElement {
		get {
			return GetComponent<LayoutElement>();
		}
	}
}
