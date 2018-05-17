using UnityEngine;
using System.Collections;

public class ControlBalaGob : MonoBehaviour {

	public float speed = 6;
	public float lifetime = 2;
	public Vector3 direction = new Vector3(-1, 0, 0);

	Vector3 stepVector;
	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifetime);
		rb = GetComponent<Rigidbody2D> ();
		stepVector = speed * direction.normalized;
	
	}
	

	void FixedUpdate () {
		rb.velocity = stepVector;

	}

	private void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name.Equals ("Girl")) {
			ControlPersonaje ctr = other.gameObject.GetComponent<ControlPersonaje>();
			if(ctr != null) ctr.RecibirBala();
			Destroy(gameObject);

		}

	}




}
