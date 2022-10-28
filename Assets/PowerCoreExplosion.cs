using System.Collections;
using System.Collections.Generic;
using DigitalRuby.LightningBolt;
using UnityEngine;

public class PowerCoreExplosion : MonoBehaviour
{
    public LightningBoltScript lightningOne;
    public LightningBoltScript lightningTwo;
    public LightningBoltScript lightningThree;
    private LightningBoltScript defaultOne = new LightningBoltScript();
    private LightningBoltScript defaultTwo = new LightningBoltScript();
    private LightningBoltScript defaultThree = new LightningBoltScript();

    public bool makeExplode = false;
    public bool makeReset = false;

    void Update()
    {
        if (makeExplode)
        {
            Explode();
            makeExplode = false;
        }

        if (makeReset)
        {
            ResetLightning();
            makeReset = false;
        }
    }
    void Start()
    {
        defaultOne.ChaosFactor = lightningOne.ChaosFactor;
        defaultOne.EndPosition = lightningOne.EndPosition;

        defaultTwo.ChaosFactor = lightningTwo.ChaosFactor;
        defaultTwo.EndPosition = lightningTwo.EndPosition;
        
        defaultThree.ChaosFactor = lightningThree.ChaosFactor;
        defaultThree.EndPosition = lightningThree.EndPosition;
    }
    public void Explode()
    {
        lightningOne.ChaosFactor *= 2;
        lightningTwo.ChaosFactor *= 2;
        lightningThree.ChaosFactor *= 2;
        
        lightningOne.EndPosition += new Vector3(Random.Range(-1,1),Random.Range(-1,1),Random.Range(-1,1));
        lightningTwo.EndPosition += new Vector3(Random.Range(-1,1),Random.Range(-1,1),Random.Range(-1,1));
        lightningThree.EndPosition += new Vector3(Random.Range(-1,1),Random.Range(-1,1),Random.Range(-1,1));
    }

    public void ResetLightning()
    {
        lightningOne.ChaosFactor = defaultOne.ChaosFactor;
        lightningOne.EndPosition = defaultOne.EndPosition;

        lightningTwo.ChaosFactor = defaultTwo.ChaosFactor;
        lightningTwo.EndPosition = defaultTwo.EndPosition;
        
        lightningThree.ChaosFactor = defaultThree.ChaosFactor;
        lightningThree.EndPosition = defaultThree.EndPosition;
    }
}
