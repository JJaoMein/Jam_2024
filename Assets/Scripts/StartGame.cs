using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
   public void StartExperience(InputAction.CallbackContext inputContext)
    {
        if (inputContext.started)
        {
            SceneManager.LoadScene("Level");
        }
    }
}
