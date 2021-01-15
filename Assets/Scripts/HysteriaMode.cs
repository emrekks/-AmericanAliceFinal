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

    public ParticleSystem particleBlood;
    public ParticleSystem particleRing;
    public float particleRingRadius;
    public bool particleRingOpen;
    
    void Start()
    {
        particleRingRadius = particleRing.shape.radius;
    }
    
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.H))
        {
            RenderSettings.fogDensity = 0.06f;
            hysteriaOpen = true;
            particleBlood.Play();
            particleRing.Play();
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
            }
        }

        if (particleRingOpen == true)
        {
            particleRingRadius += Mathf.Lerp(0, 5,Time.deltaTime);
            particleRingRadius += particleRing.shape.radius;
        }

    }
}
