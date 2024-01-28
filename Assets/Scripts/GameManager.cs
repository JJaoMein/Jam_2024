using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager GameManagerInstance;
    [SerializeField]
    private GameObject canvasContainer;

    private void Awake()
    {
        if(GameManagerInstance==null)
        {
            GameManagerInstance = this;
        }
    }
    public void SetNewPlayer(GameObject pointsContainer)
    {
        pointsContainer.transform.SetParent(canvasContainer.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
