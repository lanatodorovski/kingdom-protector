using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ExpeditionTimer))]
public class ExpeditionTimerEditor : Editor 
{
    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        if(GUILayout.Button("Skip Timer"))
        {
            ExpeditionTimer expeditionTimer = (ExpeditionTimer)target;
            expeditionTimer.EndExpedition();
        }

    }
}
