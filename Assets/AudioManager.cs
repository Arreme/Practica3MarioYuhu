using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _Instance;
    [SerializeField] private AudioSource _audioPlayer;
    [SerializeField] private AudioClip[] _audios;

    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }
    public enum Audios
    {
        GOOMBADEAD,
        GETHEALTH,
        BOWSERLAUGH,
        GETCOIN,
        JUMP1,
        JUMP2,
        JUMP3,
        PUNCH1,
        PUNCH2,
        KICK,
        TAKEDMG,
        DIE
    }
    public void PlaySound(int audio)
    {
        _audioPlayer.clip = _audios[audio];
        _audioPlayer.Play();
    }
}
