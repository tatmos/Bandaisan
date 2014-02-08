using UnityEngine;
using System.Collections;

public class SceneChange : MonoBehaviour {

	public string nextSceneName = "TITLE";
	public float waitTime = 4f;
	
	// Use this for initialization
	void Start () {
		StartCoroutine(HitEnd(1.0f));
		StartCoroutine(ChangeForceNext(waitTime));
	}
	
	bool keyEnable = false;//何か操作したら
	bool forceNext = false;//強制遷移

	// Update is called once per frame
	void Update () {
		//	強制遷移、または１秒後に何か操作したら遷移
		if(forceNext || (keyEnable && Input.anyKeyDown)){
			Application.LoadLevel(nextSceneName);
		}
	}

	//１秒後に何か操作したら遷移
	private IEnumerator HitEnd(float time)
	{
		yield return new WaitForSeconds(time);
		keyEnable = true;
	}

	//一定時間後強制遷移
	private IEnumerator ChangeForceNext(float time)
	{
		yield return new WaitForSeconds(time);
		forceNext = true;
	}
}
