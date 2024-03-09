using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class Respawner : MonoBehaviour
{
    private GameObject[] _player;

    private GameObject[] respawnPositions;

    private Random rn;
    private List<int> respawnPointIndexes;
    
    
    [SerializeField] private GameManager _manager;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectsWithTag("Player");
        respawnPositions = (from Transform child in transform where child.CompareTag("RespawnPoints") select child.gameObject).ToArray();
        rn = new Random();
        respawnPointIndexes = new List<int>();
       refillBag();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if( respawnPointIndexes.Count==0)
                refillBag();
            int respawnPoint = rn.Next(0, respawnPointIndexes.Count);
            other.gameObject.transform.position =
                respawnPositions[
                   respawnPointIndexes[respawnPoint]
                ].gameObject.transform.position;
            PlayerInformation pl = other.gameObject.GetComponent<PlayerInformation>();
            respawnPointIndexes.Remove(respawnPoint);
            PointsManager.UpdatePoints(pl.playerID,pl.PlayersEaten);
            pl.PlayersEaten = 0;
        }
    }
    private void refillBag()
    {
        for (int i = 0; i < respawnPositions.Length; i++)
        {
            respawnPointIndexes.Add(i);
        }
    }
}
