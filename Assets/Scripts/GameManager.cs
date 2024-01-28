using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{

    public static GameManager GameManagerInstance;
    [SerializeField]
    private GameObject canvasContainer;

    [SerializeField]
    private List<Color> availableColors;

    public List<GameObject> players;

    [SerializeField]
    GameObject finalScreen;

    [SerializeField]
    Image winPlayerImage;

    [SerializeField]
    private GameObject panelClose;

    public bool IsGameOver;
    public bool IsPause;
    private void Awake()
    {
        if(GameManagerInstance==null)
        {
            GameManagerInstance = this;
        }
        IsGameOver = false;
    }
    public void SetNewPlayer(GameObject pointsContainer,GameObject player)
    {
        pointsContainer.transform.SetParent(canvasContainer.transform);
        players.Add(player);
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
        PlayerController winPlayer= players[0].GetComponent<PlayerController>();

        foreach (var item in players)
        {
            if(item.GetComponent<PlayerController>().MyPoints>winPlayer.MyPoints)
            {
                winPlayer = item.GetComponent<PlayerController>();
            }
        }
        winPlayerImage.color = winPlayer.Mycolor;
        IsGameOver = true;
        //Time.timeScale = 0;

    }
    // Update is called once per frame

    public void OpenCloseGame()
    {
        panelClose.SetActive(true);
        IsPause = true;
    }

    public void CancelClose()
    {
        panelClose.SetActive(false);
        IsPause = false;
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void ResetScreen()
    {
        //Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        IsGameOver = false;
    }
    void Update()
    {
        
    }
}
