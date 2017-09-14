using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {

    public GameObject pauseMenu = null;
	public GameObject inventory = null;
	public GameObject character = null;
	public Text xPosition = null;
	public Text yPosition = null;
	public Text zPosition = null;
	public Text playerName;

	// Use this for initialization
	void Start () {
		playerName.text = PhotonNetwork.player.NickName;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.gameObject.activeSelf == false)
        {
            pauseMenu.gameObject.SetActive(true);
        } else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.gameObject.activeSelf == true)
        {
            pauseMenu.gameObject.SetActive(false);
        }

		if (Input.GetKeyDown(KeyCode.I) && inventory.gameObject.activeSelf == false && pauseMenu.gameObject.activeSelf == false)
		{
			inventory.gameObject.SetActive(true);
		} else if (Input.GetKeyDown(KeyCode.I) && inventory.gameObject.activeSelf == true && pauseMenu.gameObject.activeSelf == false)
		{
			inventory.gameObject.SetActive(false);
		}

		if (Input.GetKeyDown(KeyCode.C) && character.gameObject.activeSelf == false && pauseMenu.gameObject.activeSelf == false)
		{
			character.gameObject.SetActive(true);
		} else if (Input.GetKeyDown(KeyCode.C) && character.gameObject.activeSelf == true && pauseMenu.gameObject.activeSelf == false)
		{
			character.gameObject.SetActive(false);
		}

		xPosition.text = GameObject.FindGameObjectWithTag ("Player").transform.position.x.ToString ();
		yPosition.text = GameObject.FindGameObjectWithTag ("Player").transform.position.y.ToString ();
		zPosition.text = GameObject.FindGameObjectWithTag ("Player").transform.position.z.ToString ();

	}

    public void ResumeGame()
    {
        pauseMenu.gameObject.SetActive(false);
    }

    public void Logout()
    {
        PhotonNetwork.playerName = "";
        SceneManager.LoadScene("Login");
    }
}
