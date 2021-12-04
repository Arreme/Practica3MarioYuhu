using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_1 : MonoBehaviour
{
    private List<IRestartGame> listeners = new List<IRestartGame>(); 

    public void RestartGame()
    {
        foreach (IRestartGame l in listeners) l.RestartGame(); 
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            RestartGame(); 
    }

    public void addRestartListener(IRestartGame listener)
    {
        listeners.Add(listener); 
    }
    public void removeRestartListener(IRestartGame listener)
    {
        listeners.Remove(listener);
    }
}
