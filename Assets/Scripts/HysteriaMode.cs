using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HysteriaMode : MonoBehaviour
{

    
    #region HysteriaTimer

    public bool hysteriaOpen;
    public float hysteriaTimer;

    #endregion

    #region HysteriaParticle

    public ParticleSystem particleBlood;
    public ParticleSystem particleRing;
    private bool particleRingOpen;
    public GameObject ringObject;
    private float Timer = 0f;

    #endregion


    public GameObject pprocess;
    public AudioSource hysteriaScream;
    public Animator anim;
    
    void Update()
    {
        if (particleRingOpen == true)
        {
            RingMove();
        }
        else if (particleRingOpen == false)
        {
            particleRing.Clear();
            particleRing.Stop();
        }
        
        
        if (Input.GetKeyDown(KeyCode.H) && hysteriaOpen == false)
        {
            anim.SetTrigger("Hysteria");
            hysteriaScream.Play();
            RenderSettings.fogDensity = 0.06f;
            hysteriaOpen = true;
            particleBlood.Play();
            particleRingOpen = true;
            pprocess.gameObject.SetActive(true);

        }

        if (hysteriaOpen == true)
        {
            hysteriaTimer += Time.deltaTime;
            
            
            if (hysteriaTimer >= 5f)
            {
                RenderSettings.fogDensity = 0f;
                particleRingOpen = false;
                hysteriaTimer = 0f;
                hysteriaOpen = false;
                pprocess.gameObject.SetActive(false);
            }
        }
    }

    void RingMove()
    {
        particleRing.Play();
        Timer += Time.deltaTime;
        if (Timer >= 0.05f)
        {
            ringObject.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
            Timer = 0f;
        }
        
    }
}
