using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerNameRecord : MonoBehaviour
{
    public string nameOfPlayer;
    public string saveName;

    public GameObject pnlMainMenu;
    public GameObject pnlNameSelection;

    public TextMeshProUGUI inputText;
    public TextMeshProUGUI loadName;
    // Start is called before the first frame update
    void Awake()
    {
        PlayerPrefs.SetString("name", "Nameless");
    }

    void Update()
    {
        nameOfPlayer = PlayerPrefs.GetString("name", "none");
        loadName.text = nameOfPlayer;
    }

    public void SetName()
    {
        saveName = inputText.text;
        PlayerPrefs.SetString("name", saveName);
        pnlMainMenu.SetActive(true);
        pnlNameSelection.SetActive(false);
    }
}
