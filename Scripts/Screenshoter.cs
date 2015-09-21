using UnityEngine;
using System.Collections;

public class Screenshoter : MonoBehaviour { 
	
	void Update () {

		if (Input.GetKey (KeyCode.F)) 
			StartCoroutine(TakeScreen());
	}

	public int multiplier = 1;
	public Camera screenshotCamera;
	IEnumerator TakeScreen (string frameNumber = "") {

		screenshotCamera.depth = 1;
		Application.CaptureScreenshot("Screenshots/Screenshot_" +  System.DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss" + " " + frameNumber) + ".png", multiplier);
		yield return null;
		screenshotCamera.depth = -1;
	}


}
