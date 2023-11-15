using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformaAktiviraj : MonoBehaviour
{
    public Aktivator aktivator;

    private Transform pozicija;
    // Start is called before the first frame update
    void Start()
    {
        pozicija = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (aktivator.aktiviran)
        {

                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+179f, gameObject.transform.position.z);
                aktivator.aktiviran = false;

        }
    }
}
