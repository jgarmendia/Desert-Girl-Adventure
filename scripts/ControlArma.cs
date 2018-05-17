using UnityEngine;
using System.Collections;

public class ControlArma : MonoBehaviour {
	
	ControlPersonaje ctr;
	bool atacando = false;
	bool viviendo;
	bool estaCol = false;

	// Use this for initialization
	void Start () {
		ctr = GameObject.Find ("Girl").GetComponent<ControlPersonaje> ();

	
	}
	
	// Update is called once per frame
	void Update () {
		atacando = ctr.enGolpe;
		viviendo = ctr.viva;
	
	}


	void OnTriggerEnter2D (Collider2D other){
		if(other.gameObject.name.Equals("AreaGoblin") && atacando && !estaCol){
			ControlEnemigo ctrE = other.gameObject.GetComponentInParent<ControlEnemigo>();


			if(ctrE != null) ctrE.RecibirAtaque();
			Debug.Log ("Daño de arma Enter");

		}
		
	}


	void OnTriggerStay2D (Collider2D other){
		if(other.gameObject.name.Equals("AreaGoblin") && atacando && viviendo){
			ControlEnemigo ctrE = other.gameObject.GetComponentInParent<ControlEnemigo>();
			
			if(ctrE != null) ctrE.RecibirAtaque();
			estaCol = true;
			Debug.Log ("Daño de arma Stay");
			
		}
		
	}

	void OnTriggerExit2D (Collider2D other){
		estaCol = false;
		Debug.Log ("dejando colision");
		
	}


}
