using UnityEngine;
using System.Collections;

public class GameMain : MonoBehaviour {

	public GameObject player;
	public GUIText gt;

	public int score = 0;
	int lastScore = 0;

	static public GameMain master;

	void Awake()
	{
		if(master == null){
			DontDestroyOnLoad(this);
			score = PlayerPrefs.GetInt("Score");
		} else {
			GameObject.Destroy(this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		if(master == null){
			master = this;
		}
	}


	// Update is called once per frame
	void Update () {
	
		gt.text = string.Format("Score : {0:D4}", score);
		if(lastScore != score){
			PlayerPrefs.SetInt("Score", score);
			lastScore = score;
		}
	}




}
