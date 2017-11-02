using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Photon.MonoBehaviour {

	public float maxHealth = 100f;
	public float currentHealth;
	public float maxEnergy = 100f;
	public float currentEnergy;
	public string localPlayerName;
	public bool localPlayer = false;
    public bool dead = false;

    public PlayerLevel PlayerLevel { get; set; }

	public GameObject target = null;

	[HideInInspector]
	public PhotonView photonView;

    // Use this for initialization
    void Start () {
		this.currentHealth = this.maxHealth;
		this.currentEnergy = this.maxEnergy;
        PlayerLevel = GetComponent<PlayerLevel>();
		photonView = GetComponent<PhotonView>();

		if (photonView.isMine) {
			localPlayerName = PhotonNetwork.player.name;
			transform.name = localPlayerName;
			localPlayer = true;
		} else {
			localPlayerName = photonView.owner.NickName;
			transform.name = localPlayerName;
			localPlayer = false;
		}

    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			GetInteraction ();
		}

        if (Input.GetMouseButtonDown(1))
        {
            GetTarget();
        }

        if (target != null)
        {
            Vector3 targetPos = target.GetComponent<Collider>().gameObject.transform.position;
            float distance = Vector3.Distance(transform.position, targetPos);

            if (distance > target.GetComponent<Target>().targetRange)
            {
                target = null;
            }
        }

        if (this.currentHealth <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        //this.currentHealth = this.maxHealth;
        //this.currentEnergy = this.maxEnergy;
        dead = true;
    }

    public void TakeDamage(int amount)
    {
        //Take Damage
    }

    void GetTarget()
    {
        Ray targetRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit targetInfo;
        if (Physics.Raycast(targetRay, out targetInfo, Mathf.Infinity))
        {
            GameObject targetObject = targetInfo.collider.gameObject;
            Vector3 targetPos = targetInfo.collider.gameObject.transform.position;
            if (targetObject.tag == "Enemy")
            {
                float distance = Vector3.Distance(transform.position, targetPos);
                if (distance <= targetObject.GetComponent<Target>().targetRange)
                {
                    targetObject.GetComponent<Target>().TargetThis();
                    target = targetObject;
                }
            }
        }
    }

	void GetInteraction() {
		Ray interactionRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit interactionInfo;
		if (Physics.Raycast (interactionRay, out interactionInfo, Mathf.Infinity)) {
			GameObject interactedObject = interactionInfo.collider.gameObject;
			Vector3 interactedPos = interactionInfo.collider.gameObject.transform.position;
			if (interactedObject.tag == "Interactable Object") {
				float distance = Vector3.Distance (transform.position, interactedPos);
				if (distance <= interactedObject.GetComponent<Interactable> ().interactionRange) {
					interactedObject.GetComponent<Interactable> ().Interact ();
				}
			}
		} else if (Physics.Raycast (interactionRay, out interactionInfo, Mathf.Infinity)) {
			GameObject interactedObject = interactionInfo.collider.gameObject;
			Vector3 interactedPos = interactionInfo.collider.gameObject.transform.position;
			if (interactedObject.tag != "Interactable Object") {
				Debug.Log ("Not an Interactable Object.");
			}
		} else if (Physics.Raycast (interactionRay, out interactionInfo, Mathf.Infinity)) {
			GameObject interactedObject = interactionInfo.collider.gameObject;
			Vector3 interactedPos = interactionInfo.collider.gameObject.transform.position;
			if (interactedObject.tag == "Interactable Object") {
				float distance = Vector3.Distance (transform.position, interactedPos);
				if (distance > interactedObject.GetComponent<Interactable> ().interactionRange) {
					Debug.Log ("Out of range.");
				}
			}
		}
	}
}