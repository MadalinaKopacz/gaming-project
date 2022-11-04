using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDissapear : MonoBehaviour
{
    private int lifeTime = 10;
    public void Start()
    {
        StartCoroutine(WaitThenDie());
    }
    IEnumerator WaitThenDie()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
