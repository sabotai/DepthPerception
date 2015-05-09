using UnityEngine;
using System.Collections;

public class ScreenFadeInOut : MonoBehaviour
{
	public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.
	
	
	private bool sceneStarting = true;      // Whether or not the scene is still fading in.

	public bool fadeOut = false;
	public AudioSource[] fadeAudio;
	private float[] originalVolume;
	
	void Awake ()
	{
		
		// Set the texture so that it is the the size of the screen and covers it.
		GetComponent<GUITexture>().pixelInset = new Rect(0,0, Screen.width * 4f, Screen.height * 8f);
		GetComponent<GUITexture>().color = Color.white;

	}
	void Start(){
		originalVolume = new float[fadeAudio.Length];
		for (int i = 0; i < fadeAudio.Length; i++){
			//fade out from the volume at the start of the scene
			originalVolume[i] = fadeAudio[i].GetComponent<AudioSource>().volume;
		}
	}

	
	
	void Update ()
	{
		// If the scene is starting...
		if (sceneStarting) {
						// ... call the StartScene function.
			StartScene ();
				}
		if (fadeOut) {
			EndScene ();
			//fadeAudio.GetComponent<AudioSource>().volume = 1f - GetComponent<GUITexture>().color.a;
			//StartCoroutine("FadeAudio");
				}
		//Debug.Log (GetComponent<GUITexture>().color.a);
		if (sceneStarting || fadeOut){
			
			for (int i = 0; i < fadeAudio.Length; i++){
				//lower the audio with the opacity, also adjust for the loadlevel threshold
				fadeAudio[i].GetComponent<AudioSource>().volume = originalVolume[i] - GetComponent<GUITexture>().color.a - .05f;
				//Debug.Log (originalVolume[i] - GetComponent<GUITexture>().color.a);
			}
		}
	}
	
	
	void FadeToClear ()
	{
		// Lerp the colour of the texture between itself and transparent.
		GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.clear, fadeSpeed * Time.deltaTime);
	}
	
	
	void FadeToBlack ()
	{
		// Lerp the colour of the texture between itself and black.
		GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.black, fadeSpeed * Time.deltaTime);
	}	

	void FadeToWhite ()
	{
		// Lerp the colour of the texture between itself and white.
		GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.white, fadeSpeed * Time.deltaTime);
	}
	
	
	void StartScene ()
	{
		// Fade the texture to clear.
		FadeToClear();
		
		// If the texture is almost clear...
		if(GetComponent<GUITexture>().color.a <= 0.05f)
		{
			// ... set the colour to clear and disable the GUITexture.
			GetComponent<GUITexture>().color = Color.clear;
			GetComponent<GUITexture>().enabled = false;
			
			// The scene is no longer starting.
			sceneStarting = false;
		}
	}
	
	
	public void EndScene ()
	{
		// Make sure the texture is enabled.
		GetComponent<GUITexture>().enabled = true;
		
		// Start fading towards black.
		//FadeToBlack();
		FadeToWhite ();
		
		// If the screen is almost black...
		if(GetComponent<GUITexture>().color.a >= 0.95f){
			// ... load the next level.
			int currentLayer = PlayerPrefs.GetInt("layerProgress");
			Debug.Log ("Leaving layer " + currentLayer);
			PlayerPrefs.SetInt("layerProgress", currentLayer+1);
			int lvlIndex = Application.loadedLevel;
			int levelCount = Application.levelCount - 1;
			int loadMe = lvlIndex;
			
			Debug.Log ("this level index = " + lvlIndex + ", level count = " + levelCount);
			
			if (lvlIndex < levelCount){
				loadMe = lvlIndex + 1;
			} else {
				loadMe = 0;
			}
			
			
			Application.LoadLevel(loadMe);
		}
	}

	public void EndScene (int sceneNumber)
	{
		// Make sure the texture is enabled.
		GetComponent<GUITexture>().enabled = true;
		
		// Start fading towards black.
		//FadeToBlack();
		FadeToWhite ();
		
		// If the screen is almost black...
		if(GetComponent<GUITexture>().color.a >= 0.5f){
			// ... load the next level.

			
			Application.LoadLevel(sceneNumber);
		}
	}



}