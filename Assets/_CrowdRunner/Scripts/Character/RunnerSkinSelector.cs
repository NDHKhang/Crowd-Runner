using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerSkinSelector : MonoBehaviour
{
    [SerializeField] private Runner runner;

    //Test
    [SerializeField] private GameObject[] skinsPrefab;
    private GameObject currentSkin;

    public void SelectRunnerSkin(int skinIndex)
    {
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    if (i == skinIndex)
        //    {
        //        transform.GetChild(i).gameObject.SetActive(true);
        //        runner.SetAnimator(transform.GetChild(i).GetComponent<Animator>());
        //    }
        //    else
        //        transform.GetChild(i).gameObject.SetActive(false);
        //}

        if (currentSkin != null)
            Destroy(currentSkin);

        currentSkin = Instantiate(skinsPrefab[skinIndex], transform);
        runner.SetAnimator(currentSkin.GetComponent<Animator>());
    }
}
