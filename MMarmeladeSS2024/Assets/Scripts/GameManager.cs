using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string gameSceneName;
    public Vector3[] playerSpawns = new Vector3[4];
    public int playerCount = 2;
    public GameObject[] playerReferences;
    public List<InputDevice> inputDevices = new List<InputDevice>();
    public GameObject playerPrefab;

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

        gameObject.GetComponent<PlayerInputManager>().playerPrefab = playerPrefab;
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
                    Debug.Log(device);
                    if(!inputDevices.Contains(device))
                        inputDevices.Add(device);
                }
            }

            playerCount = inputDevices.Count;
            playerReferences = new GameObject[playerCount];
            Debug.Log("PlayerCount: " + playerCount);
			for(int i = 0; i < playerCount; i++)
            {
                Debug.Log("Joining Player with id: " + i);
                gameObject.GetComponent<PlayerInputManager>().JoinPlayer(i, -1, null, inputDevices[i]);
            }

            playerReferences = GameObject.FindGameObjectsWithTag("Player");
Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAA");
            for(int i = 0; i < playerCount; i++)
            {
                playerReferences[i].transform.position = playerSpawns[i];
                playerReferences[i].GetComponent<PlayerInformation>().playerID = i;
                PointsManager.addPlayer(i);
            }
		}
    }
}