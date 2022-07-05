using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public int Exp=100;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Exp = Player.FindObjectOfType<Player>().CurrentHealth;
    }
}
