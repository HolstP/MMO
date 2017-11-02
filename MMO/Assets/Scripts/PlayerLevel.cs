using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour {

    public int level;
    public int currentExperience;
    public int requiredExperience;

	// Use this for initialization
	void Start () {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentExperience += 100;
        }

        if (currentExperience >= requiredExperience)
        {
            LevelUp();
        }
    }

    public void Levels()
    {
        switch (level)
        {
            case 1:
                requiredExperience = 400;
                break;

            case 2:
                requiredExperience = 400;
                break;

            case 3:
                requiredExperience = 800;
                break;

            case 4:
                requiredExperience = 800;
                break;

            case 5:
                requiredExperience = 400;
                break;

            case 6:
                requiredExperience = 400;
                break;

            case 7:
                requiredExperience = 800;
                break;

            case 8:
                requiredExperience = 800;
                break;

            case 9:
                requiredExperience = 400;
                break;

            case 10:
                requiredExperience = 400;
                break;

            case 11:
                requiredExperience = 800;
                break;

            case 12:
                requiredExperience = 800;
                break;

            case 13:
                requiredExperience = 400;
                break;

            case 14:
                requiredExperience = 400;
                break;

            case 15:
                requiredExperience = 800;
                break;

            case 16:
                requiredExperience = 800;
                break;

            case 17:
                requiredExperience = 400;
                break;

            case 18:
                requiredExperience = 400;
                break;

            case 19:
                requiredExperience = 800;
                break;

            case 20:
                requiredExperience = 20000;
                break;

            case 21:
                requiredExperience = 400;
                break;

            case 22:
                requiredExperience = 400;
                break;

            case 23:
                requiredExperience = 800;
                break;

            case 24:
                requiredExperience = 800;
                break;

            case 25:
                requiredExperience = 400;
                break;

            case 26:
                requiredExperience = 400;
                break;

            case 27:
                requiredExperience = 800;
                break;

            case 28:
                requiredExperience = 800;
                break;

            case 29:
                requiredExperience = 400;
                break;

            case 30:
                requiredExperience = 400;
                break;
        }
    }

    public void LevelUp()
    {
        level++;
        currentExperience = 0;
    }

    public void GrantExperience(int amount)
    {
        currentExperience += amount;
        while(currentExperience >= requiredExperience)
        {
            currentExperience -= requiredExperience;
            level++;
        }
    }
}
