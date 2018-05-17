using UnityEngine;
using System.Collections;

public class ControlDmgBody : MonoBehaviour {
	public float proxDmg = 0.0f;
	public float dmgRate = 0.05f;
	Collider2D colliderGirl = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name.Equals ("Girl") && colliderGirl == null) {
			colliderGirl = other;
			Invoke("BajarSalud", dmgRate);
			
		}
		
	}

	void OnTriggerExit2D(Collider2D other){
		if (other == colliderGirl) {
			colliderGirl = null;
			CancelInvoke("BajarSalud");
		}

	}


	void BajarSalud(){
		colliderGirl.gameObject.GetComponent<ControlPersonaje> ().RecibirOtroDmg ();

		Debug.Log ("Daño por cuerpo");
	}


}
