using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Login : Photon.MonoBehaviour
{

    private string playerName;
    private string email;
    private string password;
    [SerializeField]
    private string clientVersion = "Dev 0.0.0.3";
    [Header("Photon")]
    public string AppVersion = "1.0";

    [Header("Login Screen")]
    public GameObject loginScreen = null;
    public InputField emailField = null;
    public InputField passwordField = null;
    public Text errorMsg = null;
    public Text versionText = null;
    public string myUserID;
    public string LoginUrl = "http://varygames.com/spacemmo/login.php";
    public string GetUserIDURL = "http://www.varygames.com/spacemmo/getuserid.php";
    public string[] userid;

    [Header("Login Loading Screen")]
    public GameObject loginLoadingScreen = null;
    public Text loginLoadingText = null;

    [Header("Character Creation Screen")]
    public string CharacterCreationUrl = "http://varygames.com/spacemmo/charactercreation.php";
    public GameObject characterCreationScreen = null;
    public InputField characternameField = null;
    public Text charCreationErrorMsg = null;
    private string charactername;

    [Header("Character Selection Screen")]
    public string deleteCharacterURL = "http://varygames.com/spacemmo/deletecharacter.php";
    public GameObject characterSelectionScreen = null;
    public GameObject logoutWindow = null;
    public GameObject quitGameWindow = null;
    public GameObject characterOne = null;
    public GameObject characterTwo = null;
    public GameObject characterThree = null;
    [SerializeField]
    public Toggle character1;
    [SerializeField]
    public Toggle character2;
    [SerializeField]
    public Toggle character3;
    public GameObject createCharacterOne = null;
    public GameObject createCharacterTwo = null;
    public GameObject characterInfoPanel = null;
    public GameObject playButton = null;
    public GameObject deleteCharacter = null;


    [Header("User Stats")]
    public Text characterNameOne = null;
    public Text units = null;
    public Text characterNameTwo = null;
    public Text characterNameThree = null;
    public string[] characterinfo;

    [Serializable]
    public class AllSectors
    {
        public string s_name;
        public string s_SceneName;
    }

    [Header("Levels Manager")]
    public List<AllSectors> m_scenes = new List<AllSectors>();
    private List<GameObject> CacheRoomList = new List<GameObject>();
    private int CurrentScene = 0;



    void Start()
    {
        loginScreen.gameObject.SetActive(true);
        loginLoadingScreen.gameObject.SetActive(false);
        characterCreationScreen.gameObject.SetActive(false);
        characterSelectionScreen.gameObject.SetActive(false);
        logoutWindow.gameObject.SetActive(false);
        quitGameWindow.gameObject.SetActive(false);

        errorMsg.text = "";
        email = ""; 
        password = "";
        myUserID = "";
        characternameField.text = "";
        charCreationErrorMsg.text = "";

        versionText.text = clientVersion;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (loginScreen.gameObject.activeSelf == true)
            {
                LogIn();
            } else if (characterCreationScreen.gameObject.activeSelf == true)
            {
                CreateCharacter();
            }
        }

        if (character1.isOn)
        {
            playButton.gameObject.SetActive(true);
            deleteCharacter.gameObject.SetActive(true);
            characterInfoPanel.gameObject.SetActive(true);
            units.text = (GetCharacterInfo(characterinfo[0], "Units "));
        }
        else if (character2.isOn)
        {
            playButton.gameObject.SetActive(true);
            deleteCharacter.gameObject.SetActive(true);
            characterInfoPanel.gameObject.SetActive(true);
            units.text = (GetCharacterInfo(characterinfo[1], "Units "));
        }
        else if (character3.isOn)
        {
            playButton.gameObject.SetActive(true);
            deleteCharacter.gameObject.SetActive(true);
            characterInfoPanel.gameObject.SetActive(true);
            units.text = (GetCharacterInfo(characterinfo[2], "Units "));
        }
        else
        {
            deleteCharacter.gameObject.SetActive(false);
            playButton.gameObject.SetActive(false);
            characterInfoPanel.gameObject.SetActive(false);
        }
    }

    public void LogIn()
    {
        errorMsg.text = "";
        email = emailField.text;
        password = passwordField.text;

        if (email == "" || password == "")
            errorMsg.text = "Please fill out both Email and Password";

        else
        {
            // Starts login proces
            Debug.Log("Login started.");
            WWWForm form = new WWWForm();
            form.AddField("email", email);
            form.AddField("password", password);
            form.AddField("clientVersion", clientVersion);
            WWW w = new WWW(LoginUrl, form);
            Debug.Log("URL: " + LoginUrl);
            loginScreen.gameObject.SetActive(false);
            loginLoadingScreen.gameObject.SetActive(true);
            loginLoadingText.text = "Logging in...";
            StartCoroutine(LogIn(w));
        }
    }

    public IEnumerator LogIn(WWW _w)
    {
        yield return _w;

        Debug.Log("Checking game version and login information.");

        loginLoadingText.text = "Checking login credentials.";

        if (_w.text == "Please fill out Email.")
        {
            loginLoadingScreen.gameObject.SetActive(false);
            loginScreen.gameObject.SetActive(true);
            errorMsg.text = "Please fill out Email.";
            Debug.Log("Email field is blank.");
        }
        else if (_w.text == "Please fill out Password.")
        {
            loginLoadingScreen.gameObject.SetActive(false);
            loginScreen.gameObject.SetActive(true);
            errorMsg.text = "Please fill out Password.";
            Debug.Log("Password field is blank.");
        }
        else if (_w.text == "Outdated")
        {
            loginLoadingScreen.gameObject.SetActive(false);
            loginScreen.gameObject.SetActive(true);
            errorMsg.text = "Game version is outdated.";
            Debug.Log("Game version is outdated.");
        }
        else if (_w.text == "Incorrect Email or Password.")
        {
            loginLoadingScreen.gameObject.SetActive(false);
            loginScreen.gameObject.SetActive(true);
            errorMsg.text = "Email or password is incorrect.";
            Debug.Log("Incorrect Email or Password.");
        }
        else if (_w.text == "Log in successful!")
        {
            loginLoadingText.text = "Logged in.";
            Debug.Log("User logged in.");
            ConnectPhoton();
            StartCoroutine(GetUserID());
        }
        else
        {
            loginLoadingScreen.gameObject.SetActive(false);
            loginScreen.gameObject.SetActive(true);
            Debug.Log("ERROR:" + _w.error);
            errorMsg.text = "Error connecting.";
        }
    }

    private IEnumerator GetUserID()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        WWW userID = new WWW(GetUserIDURL, form);
        yield return userID;

        string userIDString = userID.text;
        userid = userIDString.Split(';');
        myUserID = (GetUserID(userid[0], "User_ID"));
        Debug.Log("UserID fetched UserID is: " + myUserID);
        StartCoroutine(CheckUser());
    }

    string GetUserID(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|")) value = value.Remove(value.IndexOf("|"));
        return value;
    }

    private IEnumerator CheckUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("user_id", myUserID);
        WWW checkUser = new WWW("http://www.varygames.com/spacemmo/checkuser.php", form);
        yield return checkUser;
        if (checkUser.text == "Player has 0 characters.")
        {
            //Do something
            //Change directly to character creation
            //PhotonNetwork.playerName = email;
            //ConnectPhoton();
            loginLoadingScreen.gameObject.SetActive(false);
            characterSelectionScreen.gameObject.SetActive(false);
            characterCreationScreen.gameObject.SetActive(true);
            Debug.Log("No characters found, redirecting user to character creation.");
        }
        else if (checkUser.text == "Player has 1 characters.")
        {
            //string userInfoString = checkUser.text;
            //user = userInfoString.Split(';');
            StartCoroutine(GetCharacterInfo());
            loginLoadingScreen.gameObject.SetActive(false);
            characterSelectionScreen.gameObject.SetActive(true);
            Debug.Log("One character found.");
            characterOne.gameObject.SetActive(true);
            characterTwo.gameObject.SetActive(false);
            characterThree.gameObject.SetActive(false);
            createCharacterOne.gameObject.SetActive(true);
            createCharacterTwo.gameObject.SetActive(false);
        }
        else if (checkUser.text == "Player has 2 characters.")
        {
            //string userInfoString = checkUser.text;
            //user = userInfoString.Split(';');
            StartCoroutine(GetCharacterInfo());
            loginLoadingScreen.gameObject.SetActive(false);
            characterSelectionScreen.gameObject.SetActive(true);
            Debug.Log("Two characters found.");
            characterOne.gameObject.SetActive(true);
            characterTwo.gameObject.SetActive(true);
            characterThree.gameObject.SetActive(false);
            createCharacterOne.gameObject.SetActive(false);
            createCharacterTwo.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("An error occured.");
        }
    }

    string CheckUserAcc(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|")) value = value.Remove(value.IndexOf("|"));
        return value;
    }

    private IEnumerator GetCharacterInfo()
    {
        WWWForm form = new WWWForm();
        form.AddField("user_id", myUserID);
        WWW characterInfo = new WWW("http://www.varygames.com/spacemmo/characterinfo.php", form);
        yield return characterInfo;
        if (characterInfo.text == "No character found.") // If character name is not found. Redirect to character creation screen.
        {
            Debug.Log("User was not found.");
            loginLoadingScreen.gameObject.SetActive(false);
            characterCreationScreen.gameObject.SetActive(true);
        }
        else
        {
            loginLoadingScreen.gameObject.SetActive(false);
            characterSelectionScreen.gameObject.SetActive(true);
            string characterInfoString = characterInfo.text;
            characterinfo = characterInfoString.Split(';');
            if (characterOne.gameObject.activeSelf == true)
            {
                characterNameOne.text = (GetCharacterInfo(characterinfo[0], "Charactername "));
                //units.text = (GetCharacterInfo(characterinfo[0], "Units "));
            }
            if (characterTwo.gameObject.activeSelf == true)
            {
                characterNameTwo.text = (GetCharacterInfo(characterinfo[1], "Charactername "));
            }
            Debug.Log("Users info succesfully retrived.");
        }
    }

    string GetCharacterInfo(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|")) value = value.Remove(value.IndexOf("|"));
        return value;
    }

    public void CreateNewCharacter()
    {
        characterSelectionScreen.gameObject.SetActive(false);
        characterCreationScreen.gameObject.SetActive(true);
    }

    public void CreateCharacter()
    {
        charactername = characternameField.text;

        if (characternameField.text == "")
        {
            charCreationErrorMsg.text = "Please enter a charactername.";
        } else
        {
            // Starts Character Creation proces
            Debug.Log("Character Creation started.");
            WWWForm form = new WWWForm();
            form.AddField("charactername", charactername);
            form.AddField("user_id", myUserID);
            WWW c = new WWW(CharacterCreationUrl, form);
            Debug.Log("URL: " + CharacterCreationUrl);
            StartCoroutine(CreateCharacter(c));
        }
    }

    public IEnumerator CreateCharacter(WWW _c)
    {
        yield return _c;

        Debug.Log("Checking charactername.");

        if (_c.text == "Charactername can't be empty")
        {
            charCreationErrorMsg.text = "Please enter a charactername.";
            Debug.Log("Charactername field is blank.");
        }
        else if (_c.text == "Charactername already exists.")
        {
            charCreationErrorMsg.text = "Charactername already in use.";
            Debug.Log("Charactername is already in use.");
        }
        else if (_c.text == "Character created.")
        {
            charCreationErrorMsg.text = "Character Created!";
            Debug.Log("Character: " + characternameField.text + " has been created.");
            characterCreationScreen.gameObject.SetActive(false);
            characternameField.text = "";
            StartCoroutine(CheckUser());
        }
        else
        {
            Debug.Log("ERROR:" + _c.error);
            charCreationErrorMsg.text = "Error Creating Character. Please try again.";
        }
    }

    public void DeleteSelectedCharacter()
    {
        StartCoroutine(DeleteCharacter());
    }

    private IEnumerator DeleteCharacter()
    {
        WWWForm form = new WWWForm();
        form.AddField("user_id", myUserID);
        if (character1.isOn)
        {
            form.AddField("charactername", characterNameOne.text);
        } else if (character2.isOn)
        {
            form.AddField("charactername", characterNameTwo.text);
        } else if (character3.isOn)
        {
            form.AddField("charactername", characterNameThree.text);
        }
        WWW deleteCharacter = new WWW(deleteCharacterURL, form);
        yield return deleteCharacter;
        Debug.Log("Character deleted.");
        StartCoroutine(CheckUser());
    }

    void ConnectPhoton()
    {
        // the following line checks if this client was just created (and not yet online). if so, we connect
        if (!PhotonNetwork.connected || PhotonNetwork.connectionStateDetailed == ClientState.PeerCreated)
        {
            PhotonNetwork.AuthValues = null;
            PhotonNetwork.ConnectUsingSettings(AppVersion);
        }
    }

    public void LogoutWindow()
    {
        logoutWindow.gameObject.SetActive(true);
    }

    public void CancelLogoutWindow()
    {
        logoutWindow.gameObject.SetActive(false);
    }

    public void QuitGameWindow()
    {
        quitGameWindow.gameObject.SetActive(true);
    }

    public void CancelQuitGameWindow()
    {
        quitGameWindow.gameObject.SetActive(false);
    }

    public void LogOut()
    {
        if (PhotonNetwork.connected)
        {
            PhotonNetwork.Disconnect();
            loginScreen.gameObject.SetActive(true);
            loginLoadingScreen.gameObject.SetActive(false);
            characterSelectionScreen.gameObject.SetActive(false);
            characterCreationScreen.gameObject.SetActive(false);
            logoutWindow.gameObject.SetActive(false);
            errorMsg.text = "";
            email = "";
            password = "";
            emailField.text = "";
            passwordField.text = "";
            myUserID = "";
            characternameField.text = "";
            charCreationErrorMsg.text = "";
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void JoinGame()
    {
        if (character1.isOn)
        {
            PhotonNetwork.playerName = characterNameOne.text;
        } else if (character2.isOn)
        {
            PhotonNetwork.playerName = characterNameTwo.text;
        } else if (character3.isOn)
        {
            PhotonNetwork.playerName = characterNameThree.text;
        }

        SceneManager.LoadScene("Level");
    }
}