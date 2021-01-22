using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{

    private ParticleSystem lightning;
    [SerializeField]private float lightningTimer = 0f;
    [SerializeField]private float lightningTimer2 = 0f;
    [SerializeField]private float rnd;
    private AudioSource lightningSound1;
    private AudioSource lightningSound2;
    private float soundRnd; 


    // Start is called before the first frame update
    void Start()
    {
        lightning = gameObject.GetComponent<ParticleSystem>();
        lightningSound1 = GameObject.FindGameObjectWithTag("Lightning1").GetComponent<AudioSource>();
        lightningSound2 = GameObject.FindGameObjectWithTag("Lightning2").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        lightningTimer2 += Time.deltaTime;
        
        if (lightningTimer2 >= 5f)
        {
            lightningTimer += Time.deltaTime;
        
            if (lightningTimer >= 10f)
            {
                rnd = Random.Range(0, 10);
                lightningTimer = 0f;
            }

            if (rnd == 3)
            {
                ThunderEffect();
                lightningTimer2 = 0f;
            }
            
        }
        
        
    }

    void ThunderEffect()
    {
        lightning.Play(true);
        soundRnd = Random.Range(0, 2);

        if (soundRnd == 0f)
        {
            lightningSound1.Play();
        }
        else
        {
            lightningSound2.Play();
        }
    }
}
