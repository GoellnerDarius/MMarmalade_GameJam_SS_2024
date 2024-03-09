using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public static class PointsManager
{
    private static Dictionary<int, int> playerPoints;
    // Start is called before the first frame update
  public static void Start()
    {
        
    }

    // Update is called once per frame
    public static void UpdatePoints(int playerid)
    {
        playerPoints[playerid]++;
        foreach (var VARIABLE in playerPoints)
        {
            Debug.Log(VARIABLE.Key+", "+VARIABLE.Value);
        }
        Debug.Log("-------------------------------------------------------");
    }
}
