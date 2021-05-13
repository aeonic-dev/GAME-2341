using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatBar : MonoBehaviour
{
    public RectTransform t;
    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        t.transform.localScale = new Vector2(PlayerController.heat,1);
    }
}
