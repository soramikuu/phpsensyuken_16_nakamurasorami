using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float duration = 5f;
    public float radius = 3f;
    public float explosionDuration = 0.25f;
    public GameObject explosionModel;

    private float explosionTimer;
    private bool exploded;

    // Start is called before the first frame update
    void Start()
    {
        explosionTimer = duration;
        explosionModel.transform.localScale = new Vector3(1, 1, 1) * radius;
        explosionModel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        explosionTimer -= Time.deltaTime;
        if (explosionTimer <= 0f && exploded == false)
        {
            exploded = true;

            //Collider[] hitObjects = Physics.OverlapSphere(transform.position, radius);

            //foreach (Collider collider in hitObjects)
            //{
            //    Debug.Log(collider.name + "was hit!!");
            //}

            StartCoroutine(Explode());
            
        }
    }

    private IEnumerator Explode ()
    {
        explosionModel.SetActive(true);

        yield return new WaitForSeconds(explosionDuration);

        Destroy(gameObject);
    }
}
