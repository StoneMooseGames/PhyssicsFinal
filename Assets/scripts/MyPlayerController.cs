using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyPlayerController : MonoBehaviour
{
    public GameObject marker;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject newMarker = Instantiate(marker, this.gameObject.transform.position, Quaternion.identity);
            newMarker.transform.position = GameObject.Find("PlayerCapsule").transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Exit")
        {
            SceneManager.LoadScene(0);
        }
       
    }
}
