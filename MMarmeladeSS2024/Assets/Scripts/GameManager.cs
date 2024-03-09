using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string gameSceneName;
    public Vector2[] playerSpawns = new Vector2[4];
    public int playerCount = 2;
    public GameObject[] playerReferences;
    // I used here RuntimeAnimatorController to support AnimatorController and AnimatorOverrideController
    public RuntimeAnimatorController[] playerAnimatorController = new RuntimeAnimatorController[4];

    public List<InputDevice> inputDevices = new List<InputDevice>();


    void Awake()
    {
        if(GameObject.FindGameObjectWithTag("GameManager") == this.gameObject)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if(scene.name == gameSceneName) 
		{
            foreach (var device in InputSystem.devices)
            {
                if (device is Gamepad gamepad)
                {
                    if(!inputDevices.Contains(device))
                        inputDevices.Add(device);
                }
            }

            playerCount = inputDevices.Count;
            playerReferences = new GameObject[playerCount];
			for(int i = 0; i < playerCount; i++)
            {   
                gameObject.GetComponent<PlayerInputManager>().JoinPlayer(i, -1, null, inputDevices[i]);
            }

            playerReferences = GameObject.FindGameObjectsWithTag("Player");

            for(int i = 0; i < playerCount; i++)
            {
                playerReferences[i].transform.position = playerSpawns[i];
                playerReferences[i].GetComponent<PlayerInformation>().playerID = i;
                playerReferences[i].GetComponent<Animator>().runtimeAnimatorController = playerAnimatorController[i];
            }
            PointsManager.Start();
		}
    }

    public void PlayGameMusic()
    {
    }




}