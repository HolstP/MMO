using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {

    public GameObject pauseMenu = null;
	public GameObject inventory = null;
	public GameObject character = null;
	public GameObject map = null;
	public Text playerName;
	public Text currentHealth = null;
	public Text maxHealth = null;
	public Text currentEnergy = null;
	public Text maxEnergy = null;
    public GameObject targetPanel = null;
    public Text target = null;
    public GameObject deathScreen = null;
    public Text level = null;

	// Use this for initialization
	void Start () {
		playerName.text = PhotonNetwork.player.NickName;

		if (playerName.text == "") {
			SceneManager.LoadScene ("Lobby");
		}

        targetPanel.SetActive(false);

        deathScreen.SetActive(false);
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

		if (Input.GetKeyDown(KeyCode.I) && inventory.gameObject.activeSelf == false && pauseMenu.gameObject.activeSelf == false && gameObject.GetComponent<Chat>().InputFieldChat.isFocused == false)
		{
			inventory.gameObject.SetActive(true);
		} else if (Input.GetKeyDown(KeyCode.I) && inventory.gameObject.activeSelf == true && pauseMenu.gameObject.activeSelf == false)
		{
			inventory.gameObject.SetActive(false);
		}

		if (Input.GetKeyDown(KeyCode.C) && character.gameObject.activeSelf == false && pauseMenu.gameObject.activeSelf == false && gameObject.GetComponent<Chat>().InputFieldChat.isFocused == false)
		{
			character.gameObject.SetActive(true);
		} else if (Input.GetKeyDown(KeyCode.C) && character.gameObject.activeSelf == true && pauseMenu.gameObject.activeSelf == false)
		{
			character.gameObject.SetActive(false);
		}

		if (Input.GetKeyDown(KeyCode.M) && map.gameObject.activeSelf == false && pauseMenu.gameObject.activeSelf == false && gameObject.GetComponent<Chat>().InputFieldChat.isFocused == false)
		{
			map.gameObject.SetActive(true);
		} else if (Input.GetKeyDown(KeyCode.M) && map.gameObject.activeSelf == true && pauseMenu.gameObject.activeSelf == false)
		{
			map.gameObject.SetActive(false);
		}

		currentHealth.text = GameObject.FindWithTag("Player").GetComponent<Player>().currentHealth.ToString ();
		maxHealth.text = GameObject.FindWithTag("Player").GetComponent<Player> ().maxHealth.ToString();
		currentEnergy.text = GameObject.FindWithTag("Player").GetComponent<Player> ().currentEnergy.ToString();
		maxEnergy.text = GameObject.FindWithTag("Player").GetComponent<Player> ().maxEnergy.ToString();
        level.text = GameObject.FindWithTag("Player").GetComponent<PlayerLevel>().level.ToString();
        if (GameObject.FindWithTag("Player").GetComponent<Player>().target != null)
        {
            targetPanel.SetActive(true);
            target.text = GameObject.FindWithTag("Player").GetComponent<Player>().target.transform.name;
        } else if (GameObject.FindWithTag("Player").GetComponent<Player>().target == null)
        {
            targetPanel.SetActive(false);
        }

        if (GameObject.FindWithTag("Player").GetComponent<Player>().dead)
        {
            deathScreen.SetActive(true);
        } else if (!GameObject.FindWithTag("Player").GetComponent<Player>().dead)
        {
            deathScreen.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        pauseMenu.gameObject.SetActive(false);
    }

    public void Respawn()
    {
        GameObject.FindWithTag("Player").GetComponent<Player>().currentHealth = GameObject.FindWithTag("Player").GetComponent<Player>().maxHealth;
        GameObject.FindWithTag("Player").GetComponent<Player>().currentEnergy = GameObject.FindWithTag("Player").GetComponent<Player>().maxEnergy;
        GameObject.FindWithTag("Player").GetComponent<Player>().dead = false;
    }

    public void Logout()
    {
        PhotonNetwork.playerName = "";
        SceneManager.LoadScene("Lobby");
    }
}