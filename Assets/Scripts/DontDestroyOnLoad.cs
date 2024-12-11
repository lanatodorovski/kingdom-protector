using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        //remove this later
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("ExpeditionScene");
        }
    }
}
