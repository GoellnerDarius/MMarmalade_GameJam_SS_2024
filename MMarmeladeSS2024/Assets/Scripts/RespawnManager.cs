using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class Respawner : MonoBehaviour
{
    private GameObject[] _player;

    private GameObject[] respawnPositions;

    private Random rn;

    [SerializeField] private GameManager _manager;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectsWithTag("Player");
        respawnPositions = (from Transform child in transform where child.CompareTag("RespawnPoints") select child.gameObject).ToArray();
        rn = new Random();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.position =
                respawnPositions[rn.Next(0, respawnPositions.Length)].gameObject.transform.position;
            
        }
    }
}
