using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBeatController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Miss")
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, gameObject.transform.position.z);
        }

        if (collision.gameObject.tag == "LaneSwapBeat")
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, gameObject.transform.position.z);
        }
    }
}
