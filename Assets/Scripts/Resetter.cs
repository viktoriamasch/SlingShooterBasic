using UnityEngine;
using System.Collections;

public class Resetter : MonoBehaviour {

	public Rigidbody2D projectile;			
	public float resetSpeed = 0.025f;		//	The angular velocity threshold of the projectile, below which our game will reset
	
	private float resetSpeedSqr;			
	private SpringJoint2D spring;			//	The SpringJoint2D component which is destroyed when the projectile is launched


	void Start ()
	{

		resetSpeedSqr = resetSpeed * resetSpeed;
	}
	
	void Update () 
    {
        if (projectile)
        {
            if(!spring)
                spring = projectile.GetComponent<SpringJoint2D>();

            CheckForReset();
        }
        else
        {
            TryFindProjectile();
        }
	}

    void CheckForReset()
    {
        //	If we hold down the "R" key...
        if (Input.GetKeyDown(KeyCode.R))
        {
            //	... call the Reset() function
            Reset();
        }

        
        if (spring == null && projectile.velocity.sqrMagnitude < resetSpeedSqr)
        {
            Reset();
        }
    }
	void OnTriggerExit2D (Collider2D other)
    {
		if (projectile && other.GetComponent<Rigidbody2D>() == projectile)
        {
			Reset ();
		}
	}
    void TryFindProjectile()
    {
        if (GameObject.FindGameObjectWithTag("ChickenProjectile"))
        {
            projectile = GameObject.FindGameObjectWithTag("ChickenProjectile").GetComponent<Rigidbody2D>();
            spring = projectile.GetComponent<SpringJoint2D>();
        }
    }
	public void Reset ()
    {
        Destroy(projectile.gameObject);
	}
}
