using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCharacterData : MonoBehaviour {

    private string SaveDataURL = "http://varygames.com/spacemmo/savecharacterdata.php";
    private float posX;
    private float posY;
    private float posZ;

    public PlayerLevel playerLevel;

    // Use this for initialization
    void Start () {
        playerLevel = GetComponent<PlayerLevel>();

        StartCoroutine(SaveData());
	}
	
	// Update is called once per frame
	void Update () {

	}

    public IEnumerator SaveData()
    {
        yield return new WaitForSeconds(30f);
        posX = transform.position.x;
        posY = transform.position.y;
        posZ = transform.position.z;

        WWWForm form = new WWWForm();
        form.AddField("charactername", PhotonNetwork.player.NickName);
        form.AddField("posX", posX.ToString());
        form.AddField("posY", posY.ToString());
        form.AddField("posZ", posZ.ToString());
        form.AddField("level",  playerLevel.level.ToString());
        form.AddField("xp", playerLevel.currentExperience.ToString());
        WWW w = new WWW(SaveDataURL, form);

        Debug.Log("URL: " + SaveDataURL);
        Debug.Log("Data Saved for: " + PhotonNetwork.player.NickName);
        StartCoroutine(SaveData());
    }
}