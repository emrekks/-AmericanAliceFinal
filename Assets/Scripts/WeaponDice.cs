using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDice : MonoBehaviour
{
    public PlayerController character;
    public Rigidbody rb;
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

        rb.AddForce(Vector3.forward * Time.deltaTime, ForceMode.Impulse);
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
