﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Arrowと原理は同じ。
//違いは、ターゲットがプレイヤのところ
public class EnemyBullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    void Update()
    {

        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
