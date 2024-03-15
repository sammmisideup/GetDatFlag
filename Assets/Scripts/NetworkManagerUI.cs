using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;

    [SerializeField] private GameObject panel;

    [SerializeField] private GameObject menuCam;     


    private void Awake()
    {

        hostBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            hostBtn.gameObject.SetActive(false);
            clientBtn.gameObject.SetActive(false);
            menuCam.SetActive(false);    
            panel.SetActive(false);   
        });

        clientBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            hostBtn.gameObject.SetActive(false);
            clientBtn.gameObject.SetActive(false);
            menuCam.SetActive(false); 
            panel.SetActive(false);
        });



    }



}
