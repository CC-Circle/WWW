using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatake : MonoBehaviour
{
    public static bool isCollide = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CloneEnemy"))
        {
            isCollide = true;
            Destroy(other.gameObject);
        }
    }
}
