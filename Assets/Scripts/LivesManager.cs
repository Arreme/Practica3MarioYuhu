using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
    [SerializeField] private int _startingMarioLives;
    [SerializeField] private Text _text;
    private void Awake()
    {
        sData._marioLives = _startingMarioLives;
        _text.text = _startingMarioLives.ToString();
    }

    public void Death()
    {
        sData._marioLives--;
        _startingMarioLives.ToString();
        if (sData._marioLives < 0)
        {
            //Game Over and Restart
        } else
        {
            //Teleport to checkpoint
        }
    }
}
