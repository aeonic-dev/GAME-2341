using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update

    private void Start() {
        GetComponent<Text>().enabled = false;
    }

    public void Show() {
        GetComponent<Text>().enabled = true;
    }
}
