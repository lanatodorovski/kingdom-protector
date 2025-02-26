using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    
    [SerializeField] private bool isScouting;

    [SerializeField] private CinemachineVirtualCamera ScoutingCamera;
    [SerializeField] private CinemachineVirtualCamera BattleCamera;







    public void ToggleScoutingCamera(/*bool isActive*/)
    {
        if(BattleCamera.Priority == 20)
        {
            BattleCamera.Priority = 10;
        }
        else{
            BattleCamera.Priority = 20;
        }
    }


}