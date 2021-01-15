using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HysteriaMode : MonoBehaviour
{

    #region HysteriaColor
    
    public Material environmentMat;
    public Material playerMat;
    public Material enemyMat;
    
    public Material environmentHisMat;
    public Material playerHisMat;
    public Material enemyHisMat;

    //public MeshRenderer player;
    public MeshRenderer enemy;
    public MeshRenderer environment;

    public SkinnedMeshRenderer player;
    
    #endregion

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
    
    
    void Update()
    {
        if (particleRingOpen == true)
        {
            RingMove();
        }
        
        
        if (Input.GetKeyDown(KeyCode.H))
        {
            RenderSettings.fogDensity = 0.06f;
            hysteriaOpen = true;
            particleBlood.Play();
            particleRingOpen = true;
            player.material = playerHisMat;
            enemy.material = enemyHisMat;
            environment.material = environmentHisMat;
            
        }

        if (hysteriaOpen == true)
        {
            hysteriaTimer += Time.deltaTime;
            
            
            if (hysteriaTimer >= 5f)
            {
                hysteriaTimer = 0f;
                hysteriaOpen = false;
                player.material = playerMat;
                enemy.material = enemyMat;
                environment.material = environmentMat;
                RenderSettings.fogDensity = 0f;
                particleRingOpen = false;
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
