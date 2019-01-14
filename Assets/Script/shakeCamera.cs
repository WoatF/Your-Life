using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakeCamera : MonoBehaviour {
 public bool Shaking; 
 public float intensity,amount;
 private float ShakeDecay;
 private float ShakeIntensity;    
 private Vector3 OriginalPos;
 private Quaternion OriginalRot;
 
 void Start()
 {
     Shaking = false;   
 }
 
 
 // Update is called once per frame
 void Update () 
 {
     if(ShakeIntensity > 0)
     {
         transform.position = OriginalPos + Random.insideUnitSphere * ShakeIntensity;
         transform.rotation = new Quaternion(OriginalRot.x + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
                                   OriginalRot.y + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
                                   OriginalRot.z + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
                                   OriginalRot.w + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f);
 
        ShakeIntensity -= ShakeDecay;
     }
     else if (Shaking)
     {
        Shaking = false;  
     }
 }                                              
 public void DoShake()
 {
     OriginalPos = transform.position;
     OriginalRot = transform.rotation;
 
     ShakeIntensity = intensity;
     ShakeDecay = amount;
     Shaking = true;
 } 
}
