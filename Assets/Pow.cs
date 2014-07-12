using UnityEngine;
using System.Collections;
using System.Linq;

public class Pow : MonoBehaviour {
	
	void Start () 
	{
		var color = from r in this.MaybeGetComponent<Renderer> () select r.material.color;
		print ("Color: " + color);

		var position = from t in this.MaybeGetComponent<Transform> () select t.position;
		print ("Position: " + position);

		Vector3 p;
		if (position.MatchJust (out p)) {
			print ("We have a position! It is " + p);
		}
	}
	
	void Update ()
	{
		
	}
}
