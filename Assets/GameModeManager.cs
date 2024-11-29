using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    [SerializeField] private bool isScouting;
    [SerializeField] private Camera cinemachineBrain;

    private void Update()
    {
        //ToggleScoutingCamera(isScouting);
    }

    private void ToggleScoutingCamera(bool isActive)
    {
        cinemachineBrain.gameObject.SetActive(isScouting);
    }
}
