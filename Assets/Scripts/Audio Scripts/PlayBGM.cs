using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM : MonoBehaviour
{   

    [SerializeField] private int BGM_Index;
    // Start is called before the first frame update
    void Start()
    {
        BGMPlayer play = FindObjectOfType<BGMPlayer>();
        play.index = BGM_Index;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
