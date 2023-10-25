using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainSceneCanvas : MonoBehaviour
{
    public TextMeshProUGUI playerNameText; // Reference to Player Name Text UI

    void Start()
    {
        playerNameText.text = PlayerDataHandler.Instance.PlayerName;
    }
}