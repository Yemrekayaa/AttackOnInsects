using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CameraSettings : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

 
    void Start(){
        Time.timeScale = 1;
    }
    void Update()
    {
        
        transform.position = player.position + offset;


    }




}
