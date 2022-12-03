using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KillingSlider : MonoBehaviour
{
    [SerializeField] private float move;
    private Vector3 end;
    

    private void Start() 
    {
        end = new Vector3(10000, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    void Update() 
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, end, move);
    }
}
