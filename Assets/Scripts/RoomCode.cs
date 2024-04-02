using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomCode : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI RoomCodeInput;
    // Start is called before the first frame update
    void Start()
    {
        RoomCodeInput.text = "Code: " + PlayerPrefs.GetString("RoomCodeIn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
