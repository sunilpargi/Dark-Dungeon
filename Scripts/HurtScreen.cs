using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtScreen : MonoBehaviour
{
    public GameObject hurtScreen;

   public void ActivateObj()
    {
        StartCoroutine(DeactivateObj());
    }

     IEnumerator DeactivateObj()
    {
        hurtScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        hurtScreen.SetActive(false);
    }
}
