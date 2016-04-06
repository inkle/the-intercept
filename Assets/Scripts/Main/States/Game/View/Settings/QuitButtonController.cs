using UnityEngine;
using System.Collections;

public class QuitButtonController : MonoBehaviour {
	
	void OnEnable () {
		gameObject.SetActive(SystemInfo.deviceType == DeviceType.Desktop);
	}
}
