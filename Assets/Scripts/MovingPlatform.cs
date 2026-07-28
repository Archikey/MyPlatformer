using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField]
    private Vector3 finishPos = Vector3.zero;
    [SerializeField]
    private float speed;

    private Vector3 startPos;
    private float trackPercent = 0;
    private int direction = 1;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, finishPos);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       startPos = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        trackPercent += direction * speed * Time.deltaTime;
        float x = (finishPos.x - startPos.x) * trackPercent + startPos.x;
        float y = (finishPos.y - startPos.y) * trackPercent + startPos.y;

        transform.position = new Vector3(x, y, startPos.z);

        if ((direction == 1 && trackPercent > .9f) ||
            (direction == -1 && trackPercent <.1f))
        {
            direction *= -1;

        }
    }
}
