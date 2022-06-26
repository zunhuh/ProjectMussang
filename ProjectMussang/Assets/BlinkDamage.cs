using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkDamage : MonoBehaviour
{
    SpriteRenderer sr;
    public bool bActive = false;

    public void Blink_start()
    {
        bActive = true;
        Invoke("Blink_end",1.0f);
    }
    void Blink_end()
    {
        sr.color = Color.white;
        bActive = false;
    }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();       
    }
    void Update()
    {
        if (bActive)
        {
            if (Time.fixedTime % 0.2f < 0.10f)  { sr.color = Color.red; }
            else                                { sr.color = Color.white; }
        }
    }
}

/*
public class Blink : MonoBehaviour {

    public GameObject go;
    
	void Update () {

        if (go != null){
            if (Time.fixedTime % 0.5f < 0.2f) { go.SetActive(true); }
            else { go.SetActive(false); }
        }
    }
}
*/

/*
    Renderer renderer = GetComponent<Renderer>();
	void Update () 
    {
        if (renderer != null) {
            if (Time.fixedTime % 0.5f < 0.2f) { renderer.enabled = false; }
            else { renderer.enabled = true; }
        }
    }
*/
