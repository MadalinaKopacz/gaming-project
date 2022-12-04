using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KillingSlider : MonoBehaviour
{
    [SerializeField] private float movingSpeed = 0.03f;
    [SerializeField] private float maxIdleTime = 7; // 7 seconds
    [SerializeField] private float maxLevelTime = 2 * 60; // 2 mins
    private Vector3 end;
    private float startTime;
    private float startIdle;
    private bool isIdle;
    private Animator animator;

    private void Start() 
    {
        end = new Vector3(10000, gameObject.transform.position.y, gameObject.transform.position.z);
        startTime = Time.time;
        animator = GameObject.Find("Player").GetComponent<Animator>();
        isIdle = false;
    }


    private bool checkIdle()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle Animation") || 
            animator.GetCurrentAnimatorStateInfo(0).IsName("IdleAiming"))
        {
            return true;
        }
        
        return false;
    }


    void Update() 
    {
        float currentTime = Time.time;

        if (!checkIdle())
        {
            // Player moved
            isIdle = false; 
        }
        else if (checkIdle() && !isIdle)
        {
            // Player entered idle state
            isIdle = true;
            startIdle = currentTime;
        }
        
        if (isIdle)
        {
            if (currentTime - startIdle >= maxIdleTime)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, end, movingSpeed);
            }
        }


        if (currentTime - startTime >= maxLevelTime)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, end, movingSpeed);
        }

    }
}
