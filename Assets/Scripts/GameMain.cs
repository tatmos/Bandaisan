using UnityEngine;
using System.Collections;

public class GameMain : MonoBehaviour {

	public GameObject player;

	public int score = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			player.transform.Translate(new Vector3(-10,0,0));
		}
		
		if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			player.transform.Translate(new Vector3(10,0,0));
		}

		if(Input.GetKeyDown(KeyCode.Space))
		{
			player.transform.localScale = new Vector3(20,20,20);
		}
		if(Input.GetKeyUp(KeyCode.Space))
		{
			player.transform.localScale = new Vector3(10,10,10);
		}

	}




}
