using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public static GameManager GameManagerInstance;
    [SerializeField]
    private GameObject canvasContainer;

    [SerializeField]
    private List<Color> availableColors;

    [SerializeField]
    GameObject finalScreen;
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

    public Color GetColor()
    {
        Color color=Color.white;
        
            color = availableColors[0];
            availableColors.Remove(availableColors[0]);

        return color;
    }

    public void FinishGame()
    {
        finalScreen.SetActive(true);
    }
    // Update is called once per frame

    public void ResetScreen()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void Update()
    {
        
    }
}
