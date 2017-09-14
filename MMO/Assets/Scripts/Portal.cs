using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    [SerializeField]
        private string sectorName;

    [SerializeField]
        private bool isJumping = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Debug.Log("We collided with the portal");
        }
    }

    private void OnTriggerStay(Collider other)
    {
      if (other.gameObject.name == "Player")
        {
            if (Input.GetKeyDown(KeyCode.J) && !isJumping)
            {
                isJumping = true;
                Application.LoadLevel(sectorName);
                Debug.Log("We jumped the portal");

            }
        }
    }
}
