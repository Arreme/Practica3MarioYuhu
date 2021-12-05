using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
    [SerializeField] private int _startingMarioLives;
    [SerializeField] private Text _text;
    [SerializeField] private GameObject _deathScreen;
    [SerializeField] private MarioPlayerController _controller;
    private void Awake()
    {
        sData._marioLives = _startingMarioLives;
        _text.text = _startingMarioLives.ToString();
    }

    public void Death()
    {
        sData._marioLives--;
        _text.text = sData._marioLives.ToString();
        if (sData._marioLives < 0)
        {
            _controller.gameObject.SetActive(false);
            _deathScreen.SetActive(true);
        } else
        {
            _controller.RestartGame();
        }
    }
}
