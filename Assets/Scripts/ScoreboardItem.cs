using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class ScoreboardItem : MonoBehaviour
{
    public TMP_Text usernameText;
    public TMP_Text killsText;
    public TMP_Text deathText;

    public int deathCount;
    public int killCount;

    public void Initialize(Player player)
    {
        usernameText.text = player.NickName;
    }
    public void PlayerDied()
    { 
    }
}
