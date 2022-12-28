using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Invoke(nameof(FinishLevel), 1f);
        }
    }

    private void FinishLevel()
    {
        SceneManager.LoadScene(3);
    }
}
