using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    [SerializeField] Canvas menuCanvas, inGameCanvas, gameOverCanvas;

    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void ShowMenuCanvas()
    {
        menuCanvas.enabled = true;
    }

    public void HideMenuCanvas()
    {
        menuCanvas.enabled = false;
    }

    public void ShowInGameCanvas()
    {
        inGameCanvas.enabled = true;
    }

    public void HideInGameCanvas()
    {
        inGameCanvas.enabled = false;
    }

    public void ShowGameOverCanvas()
    {
        gameOverCanvas.enabled = true;
    }

    public void HideGameOverCanvas()
    {
        gameOverCanvas.enabled = false;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
