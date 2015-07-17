using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {


	public static FollowCam S; 

	public GameObject poi;

	private float camZ;

	public Transform farLeft;
	public Transform farRight;






	void Awake() {
		S = this;
		camZ = this.transform.position.z;
	
	
	
	}


	void Update() {
		if(poi == null) {
			return;
		
		}

		Vector3 destination = poi.transform.position;


		//Limit the x and y Position to minumum values


		destination.x = Mathf.Max (0, destination.x);
		destination.y = Mathf.Max (0, destination.y);
		destination.x = Mathf.Clamp (destination.x, farLeft.position.x, farRight.position.x);




		destination.z = camZ;

		transform.position = destination;


		this.GetComponent<Camera> ().orthographicSize = 10 + destination.y;//Mathf.Max(10, destination.y);

	


	
	}




}
