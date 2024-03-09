using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pointsrenderer : MonoBehaviour
{
    //display scores in the 4 corners
    [SerializeField] private TextMeshProUGUI[] _scoreDisplays;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            
            for (int i = 0; i < _scoreDisplays.Length; i++)
            {
                _scoreDisplays[i].text = "Player " + (i + 1) + " : " + PointsManager.playerPoints[i];
            }
            
    }
}
