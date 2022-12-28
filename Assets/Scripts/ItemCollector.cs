using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int gem = 0;

    public Text gemText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gem1")
        {
            Destroy(collision.gameObject);
            gem++;
            gemText.text = "GEMS: " + gem;
        }
        else if (collision.gameObject.tag == "Gem5")
        {
            Destroy(collision.gameObject);
            gem = gem + 5;
            gemText.text = "GEMS: " + gem;
        }
    }
}
