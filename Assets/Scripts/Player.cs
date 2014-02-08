using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public List<AudioClip> seList = new List<AudioClip>();

	// Use this for initialization
	void Start () {
		hantei = HanteiObject.GetComponent<Hantei>();
	}

	float gameTime;

	bool attack = false;
	float attackLastTime = 0;

	public GameObject HanteiObject;
	Hantei hantei;

	// Update is called once per frame
	void Update () {

		if(MasterBPM.master == null)return;
		if(MasterBPM.master != null){
			gameTime = MasterBPM.master.gameTime;
		}

		//	マウスが押されたら
		if(Input.GetMouseButtonDown(0)){
			// タップ判定（移動）
			if(Input.mousePosition.x < Screen.width)
			{
				Vector3 nowPos;
				unproject_mouse_position(out nowPos,Input.mousePosition);
				nowPos.x = 0;

				if(Vector3.Distance(nowPos,transform.position) > 1.5f)
				{
					Debug.Log("  " + Vector3.Distance(nowPos,transform.position).ToString());
					transform.position = nowPos;
				} else {
					attack = true;					
					hantei.hanteiCheckEnable = true;
					attackLastTime = (int)gameTime;

					audio.clip = seList[Random.Range(0,seList.Count)];
					audio.Play();
				}
			}
		}

		//タップすると指揮をふる
		if(attack && (attackLastTime+0.5f) < gameTime)
		{
			transform.localScale = new Vector3(2,2,2);
			attack = false;
		}

		transform.localScale = Vector3.Lerp(transform.localScale,new Vector3(1,1,1),Time.deltaTime*4f);

		
		hantei.hanteiCheckEnable = attack;
	}

	public bool unproject_mouse_position(out Vector3 worldPos,Vector3 mousPos)
	{
		bool ret;
		float depth;
		
		Plane plane = new Plane(Vector3.forward,new Vector3(0,0,0));
		
		Ray ray = Camera.main.ScreenPointToRay(mousPos);
		
		if(plane.Raycast(ray,out depth)){
			worldPos = ray.origin+ray.direction*depth;	
			ret = true;
		} else {
			worldPos = Vector3.zero;
			ret =false;
		}
		
		return ret;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "End")
		{
			//if(hanteiCheckEnable)
			{
				Debug.Log("Hit End");
				Application.LoadLevel("Result");
			}
		}
	}
}
