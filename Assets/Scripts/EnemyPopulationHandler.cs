using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPopulationHandler : MonoBehaviour
{
    [SerializeField] private int eliminationCount;
    public int GetEliminationCount()
    {
        return eliminationCount;
    }
}
