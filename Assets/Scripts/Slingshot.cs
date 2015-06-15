using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {


	//Fields seen in the Inspector panek
	public GameObject prefabProjectile;
	public float shotMult = 4.0f;

	// Internal Variable
	private GameObject    launchPoint;
	private Vector3       launchPos;
	private GameObject    projectile;

	bool aimingMode;

	
	void Awake(){
		Transform launchPointTransform = transform.Find ("LaunchPoint");
		launchPoint = launchPointTransform.gameObject;
		launchPoint.SetActive(false);

		launchPos = launchPointTransform.position;

	}

	
	void OnMouseEnter() {
		launchPoint.SetActive(true);
	
	}
	
	void OnMouseExit() {
		launchPoint.SetActive(false);


	}


	void OnMouseDown() {

		aimingMode = true;

		// Instatiate a new Projectile
		projectile = Instantiate(prefabProjectile) as GameObject;


		//Start at launchPoint
		//set the projectile's Position to the LaunchPos
		projectile.transform.position = launchPos;

		//Set isKinematic is true for now
		projectile.GetComponent<Rigidbody>().isKinematic = true;
	


	}

	void Update() {
	    //If we're not in aming mode, do nothing
		if(!aimingMode) {
			return;

		}


	// Get the current mouse position in 3D
		Vector3 mousePos2D = Input.mousePosition;


	//Convert it to 3D world coordinates
		mousePos2D.z = - Camera.main.transform.position.z;
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
	


    //Find the difference between launchPos and mouse position
		Vector3 mouseDelta = mousePos3D - launchPos;

		float maxMagnitude = this.GetComponent<SphereCollider>().radius;

		mouseDelta = Vector3.ClampMagnitude(mouseDelta,maxMagnitude);
	
	

    //Move the projectile to this new position
		projectile.transform.position = launchPos + mouseDelta;

		if (Input.GetMouseButtonUp(0)) {
			aimingMode = false;
			projectile.GetComponent<Rigidbody>().isKinematic = false;

			projectile.GetComponent<Rigidbody>().velocity = -mouseDelta * shotMult;

			FollowCam.S.poi = projectile;

		}
	
	}








}