using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

public static class PointsManager
{
    public static Dictionary<int, int> playerPoints{ get; private set; }

    // Start is called before the first frame update
  public static void addPlayer(int playerId)
  {
      playerPoints[playerId] = 0;

  }

    // Update is called once per frame
    public static void UpdatePoints(int playerid,int points)
    {
        playerPoints[playerid]+=points;
       
    }
    
}
