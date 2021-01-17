using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDice : MonoBehaviour
{
    public PlayerController character;
    public GameObject dicePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ThrowDice()
    {
        Instantiate(dicePrefab, this.transform.position, this.transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            character.DiceCount += 1;
            this.gameObject.SetActive(false);

        }
    }
}
