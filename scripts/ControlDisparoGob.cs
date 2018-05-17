using UnityEngine;
using System.Collections;

public class ControlDisparoGob : MonoBehaviour {

	Collider2D disparandoA = null;

	ControlEnemigo ctr;

	// Use this for initialization
	void Start () {
		ctr = GameObject.Find ("goblin archer").GetComponent<ControlEnemigo> ();;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other){
		if(other.gameObject.name.Equals("Girl") && disparandoA == null){
			Disparar();
			disparandoA = other;
		}

	}

	void OnTriggerExit2D (Collider2D other){
		if (other == disparandoA) {
			disparandoA = null;
		}

	}

	void Disparar(){
		ctr.Disparar ();

	}


}
