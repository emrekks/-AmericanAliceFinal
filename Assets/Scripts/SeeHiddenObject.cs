using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeHiddenObject : MonoBehaviour
{
    private GameObject[] HiddenObject;
    private PlayerController _playerController;
    // Start is called before the first frame update
    void Start()
    {
        HiddenObject = GameObject.FindGameObjectsWithTag("HiddenObject");
        _playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerController.isSmall)
        {
            foreach(GameObject child in HiddenObject)
            {
                child.GetComponentInChildren<MeshRenderer>().enabled = true;
            }
        }

        else
        {
            foreach (GameObject child in HiddenObject)
            {
                child.GetComponentInChildren<MeshRenderer>().enabled = false;
            }
        }
    }
}
