using UnityEngine;
using System.Collections;

public class LightResponse : Singleton<LightResponse>{
	
	Light light;
	delegate void stateHandler();
	stateHandler state;
	Color currentColor;
	Color originalColor;
	float maxIntesnity;
	float originalIntensity;
	float trans;
	public float transOut;
	
	// Use this for initialization
	void Start () {
		
		light = GetComponent<Light> ();
		originalColor = light.color;
		originalIntensity = light.intensity;
		state = idle;
	}
	
	// Update is called once per frame
	void Update () {
		
		state ();
	}
	
	void idle () {
		
	}
	
	void decreasing () {
		
		trans -= transOut;
		Color transColor = Color.Lerp (originalColor, currentColor, trans);
		float transIntensity = Mathf.Lerp (originalIntensity, maxIntesnity, trans);
		light.color = transColor;
		light.intensity = transIntensity;
		
		if (trans <= 0) 
			state = idle;
	}
	
	public void changeColor (Color col, float intensity, float transOut) {

		maxIntesnity = intensity;
		state = decreasing;
		currentColor = col;
		trans = 1;
	}
	
}
