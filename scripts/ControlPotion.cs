using UnityEngine;
using System.Collections;

public class ControlPotion : MonoBehaviour {
	Animator anim;

	// Use this for initialization
	void Start () {
		//anim.GetComponent<Animator> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name.Equals ("Girl")) {
			ControlPersonaje ctr = other.gameObject.GetComponent<ControlPersonaje>();
			if(ctr != null) ctr.Curar();
			Destroy(gameObject);
			
		}
		
	}





}
