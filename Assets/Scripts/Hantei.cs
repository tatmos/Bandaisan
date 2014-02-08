using UnityEngine;
using System.Collections;

public class Hantei : MonoBehaviour {

	MeshRenderer mr;
	ParticleSystem ps;

	// Use this for initialization
	void Start () {
		mr = GetComponent<MeshRenderer>();
		ps = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
	
		//if(hanteiCheckEnable != ps.enableEmission){
		//	ps.enableEmission = hanteiCheckEnable;
		//}
	}

	public bool hanteiCheckEnable = false;

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Onpu")
		{
			//if(hanteiCheckEnable)
			{
				Debug.Log("Hit Onpu");
				GameObject.Destroy(other.gameObject);
			}
		}
	}
}
