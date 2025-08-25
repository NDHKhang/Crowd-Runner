using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrowdCounter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshPro counterText;
    [SerializeField] private Transform charactersParent;

    private Vector3 baseLocalPosition;

    void Start()
    {
        baseLocalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCounterHeight();
        UpdateCounterText();

        if (charactersParent.childCount <= 0)
            Destroy(gameObject);
    }

    private void UpdateCounterHeight()
    {
        float z = (float)charactersParent.childCount * 0.05f;
        transform.localPosition = baseLocalPosition + new Vector3(0, 0, z);
    }

    private void UpdateCounterText()
    {
        counterText.text = charactersParent.childCount.ToString();
    }
}
