using UnityEngine;
using System.Collections;

public class PictureController : MonoBehaviour 
{
	public float waitTime = 5f;


	public GameObject currentObj;
	public GameObject backObj;
	public Texture[] textures;

	private float currentAlpha;
	private float backAlpha;
	private bool isFade;
	private int previousID = -1;

	void Start () 
	{
		isFade = false;

		currentObj.renderer.material.mainTexture = GetTexture();
		backObj.renderer.material.mainTexture = GetTexture();

		StartCoroutine("Prosess");
	}
	
	void Update () 
	{
		if(isFade)
		{
			currentAlpha -= 0.01f; backAlpha += 0.01f;
		}
		if(currentAlpha <= 0f) isFade = false;
		currentObj.renderer.material.color = new Color(1f,1f,1f,currentAlpha);
		backObj.renderer.material.color = new Color(1f,1f,1f,backAlpha);
	}

	private Texture GetTexture()
	{
		int id = Mathf.CeilToInt (Random.Range (0f,textures.Length)) - 1;

		if(id == previousID)
		{
			while(id == previousID)
			{
				id = Mathf.CeilToInt (Random.Range (0f,textures.Length)) - 1;
			}
		}

		previousID = id;

		return textures[id];
	}
	
	IEnumerator Prosess()
	{
		bool i = true;
		while(i)
		{
			Init ();
			yield return new WaitForSeconds(waitTime);
			Fade ();
			yield return new WaitForSeconds(2f);
			Replace ();
		}
	}

	private void Init()
	{
		currentAlpha = 1.0f;
		backAlpha = 0f;
		backObj.renderer.material.mainTexture = GetTexture();
	}

	private void Fade()
	{
		isFade = true;
	}

	private void Replace()
	{
		currentObj.renderer.material.mainTexture = backObj.renderer.material.mainTexture;
	}
}
