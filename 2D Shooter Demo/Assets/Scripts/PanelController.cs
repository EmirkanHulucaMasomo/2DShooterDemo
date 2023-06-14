using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    
    [SerializeField] private GameObject pauseButton, pauseMenu, deadMenu;
    private void OnEnable()
    {
        EventManager.onPlayerDeath += PlayerDeadPanel;
        
    }
    private void OnDisable()
    {
        EventManager.onPlayerDeath -= PlayerDeadPanel;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDeadPanel()
    {
        MenuScript.GameIsPaused = true;
        pauseButton.SetActive(false);
        deadMenu.SetActive(true);
    }
}
