using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float attackRange = 2f;

	public float targetRange = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDrawGizmosSelected () {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (transform.position, targetRange);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, attackRange);
	}
}
