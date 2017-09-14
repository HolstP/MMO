using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject playerPrefab;

    public List<PhotonPlayer> connectedPlayerList = new List<PhotonPlayer>();

    // Use this for initialization
    void Start () {
        SpawnPlayer();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnPlayer()
    {
        
    }
}
