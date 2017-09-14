using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocation : MonoBehaviour {

    private string SavePositionURL = "http://varygames.com/spacemmo/saveposition.php";
    public float posX;
    public float posY;
    public float posZ;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.P))
        {
            SavePosition();
        }
	}

    public void SavePosition()
    {
        posX = transform.position.x;
        posY = transform.position.y;
        posZ = transform.position.z;

        WWWForm form = new WWWForm();
        form.AddField("charactername", PhotonNetwork.player.NickName);
        form.AddField("posX", posX.ToString());
        form.AddField("posY", posY.ToString());
        form.AddField("posZ", posZ.ToString());
        WWW w = new WWW(SavePositionURL, form);

        Debug.Log("URL: " + SavePositionURL);
        Debug.Log("Position Saved for: " + PhotonNetwork.player.NickName);
    }
}