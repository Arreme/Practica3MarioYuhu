using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] Score score;
    private void OnTriggerEnter(Collider other)
    {
        if (score != null)
        {
            AudioManager._Instance.PlaySound((int)AudioManager.Audios.GETCOIN);
            score.score();
        }
        Destroy(gameObject);
    }
}
