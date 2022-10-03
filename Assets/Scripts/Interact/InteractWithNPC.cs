using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameJam.Player;

public class InteractWithNPC : MonoBehaviour
{
    [SerializeField] private GameObject _npc;
    [SerializeField] private GameObject _readyTalk;
    [SerializeField] private GameObject _infoPanel;
    [SerializeField] private PlayerController _player;

    private float _distance;
    private bool _isTalking = true;

    void Update()        
    {
        _distance = Vector3.Distance(transform.position, _npc.transform.position);

        if (_distance < 5f)
        {           
            _readyTalk.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E) && _isTalking)
            {
                _player.enabled = false;
                Cursor.lockState = CursorLockMode.Confined;
                _infoPanel.SetActive(true);
                _isTalking = false;
            }

            else if (Input.GetKeyDown(KeyCode.Tab) && _isTalking == false)
            {
                _player.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                _infoPanel.SetActive(false);
                _isTalking = true;
            }           
        }

        else
        {
            HidePanel();
        }
    }

    private void HidePanel()
    {
        _readyTalk.SetActive(false);
        _infoPanel.SetActive(false);
    }
}
