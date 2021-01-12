using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAim : MonoBehaviour
{
    public List<GameObject> colliderDetectedList = new List<GameObject>();
    public GameObject closestEnemy;
    public float maxRange = 25;
    public GameObject Alice;





    // Update is called once per frame
    void Update()
    {
        if(colliderDetectedList.Count >= 1)
        {
            foreach (GameObject enemyGameObject in colliderDetectedList)
            {
                float range = maxRange;
                float dist = Vector3.Distance(enemyGameObject.transform.position, Alice.transform.position);
                if (dist < range)
                {
                    range = dist;
                    closestEnemy = enemyGameObject;
                }

            }
        }

        if (colliderDetectedList.Count == 0)
        {
            closestEnemy = null;
        }


    }

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Enemy" && !colliderDetectedList.Contains(other.gameObject))
        {
            colliderDetectedList.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Enemy")
        {
            colliderDetectedList.Remove(other.gameObject);          
        }
    }

}

