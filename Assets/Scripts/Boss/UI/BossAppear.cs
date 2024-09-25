using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAppear : MonoBehaviour
{
    [SerializeField] private Canvas canvas_1;
    [SerializeField] private Canvas canvas_2;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera BossShotCamera;
    [SerializeField] private ParticleSystem ParticleSystem;



    public void BeforeBossFight()
    {
        canvas_1.enabled = false;
        canvas_2.enabled = true;

        mainCamera.depth = -1;
        BossShotCamera.depth = 1;
        ParticleSystem.Stop();

    }


    public void AfterBossFight()
    {
        canvas_1.enabled = true;
        canvas_2.enabled = false;

        mainCamera.depth = 1;
        BossShotCamera.depth = -1;
        ParticleSystem.Play();
    }
}
