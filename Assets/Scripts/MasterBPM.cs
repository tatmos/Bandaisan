using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.SerializableAttribute]
public class BgmClipSetting
{
	public AudioClip clip;
	public float bpm;
	public List<int> baseNoteList;
	public string title;
}

public class MasterBPM : MonoBehaviour {

	public List<AudioClip> audioClipList = new List<AudioClip>();
	public List<BgmClipSetting> bgmClipList = new List<BgmClipSetting>();


	public AudioSource bgmPlayer;

	ParticleSystem ps;

	public float bpm = 120f;
	public int bgmNo = 0;
	public int baseNote  = 0;
	public string bgmTitle = "";

	static public MasterBPM master;

	void Awake()
	{
		if(master == null){
			DontDestroyOnLoad(this);
		} else {
			GameObject.Destroy(this.gameObject);
		}
	}
	
	void Start () {

		if(master == null){
			master = this;
		
			ps = GetComponent<ParticleSystem>();

			bgmPlayer = gameObject.AddComponent<AudioSource>();

			Init();

			BgmPlay(Random.Range(0,bgmClipList.Count));
		} else {



		}
	}

	public void BgmPlay(int inBgmNo)
	{

		bgmNo = inBgmNo;
		bgmPlayer.clip = bgmClipList[bgmNo].clip;
		bpm = bgmClipList[bgmNo].bpm;

		bgmTitle = bgmClipList[bgmNo].clip.name;
		bgmPlayer.Play();

		gameTime = 0;

	}

	void Init()
	{
		gameTime = 0;
	}

	//float lastPlayTime = 0;
	//bool noteOn = true;

	public float gameTime = 0;	
	public float gameTime2 = 0;
	public int clipNo = 0;

	//bool lastPlayFlag = false;

	public float speed = 1f;

	void Update () {
		if(MasterBPM.master == null)return;

		int baseNoteListCount = bgmClipList[bgmNo].baseNoteList.Count;
		baseNote = bgmClipList[bgmNo].baseNoteList[(int)(baseNoteListCount * Mathf.PerlinNoise(gameTime * 0.5f, 0.0F))%baseNoteListCount];

		if(bgmPlayer.isPlaying){

			gameTime = (float)bgmPlayer.timeSamples/(44100f/2f) *((float)bpm/(float)60f);
			gameTime2 = gameTime*speed;
			//Debug.Log("BGM" + (bpm/60f));

		} else {
			gameTime+= Time.deltaTime*(bpm/60f)*speed;


			int newBgmNo = Random.Range(0,bgmClipList.Count);
			if(master.bgmNo != newBgmNo || master.bgmPlayer.isPlaying == false)
			{
				if(master.bgmNo == newBgmNo)
				{
					newBgmNo = (master.bgmNo+1)%bgmClipList.Count;	//	shift (not same music)
				}

				master.BgmPlay(newBgmNo);
			}

		}

		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
			if(speed < 32f)
			{
				speed+=0.8f;
			}
			
			clipNo++;
		}
		if(Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.DownArrow))
		{
			if(speed > -2)
			{
				speed-=0.6f;
			}
			clipNo--;
		}

		speed = Mathf.Lerp(speed,1,gameTime*0.0005f);

		//ps.enableEmission = audio.isPlaying;
	}

	void OnGUI()
	{
		//GUILayout.BeginHorizontal();
		//GUILayout.Label("BPM " + (int)bpm + " " + bgmTitle + " Speed " + speed);
		//GUILayout.EndHorizontal();
	}

	void OnBPMUp()
	{
		bpm += 1;
	}

	/*
	void ChangeBPM(float inBPM){
		bpm = inBPM;

		lastPlayTime = 0;

		GameObject[] gml = GameObject.FindGameObjectsWithTag("Player");
		foreach(GameObject gm in gml)
		{
			ClickSound cs = gm.GetComponentInChildren<ClickSound>();
			cs.SetBpm(inBPM);
		}
	}*/
}
