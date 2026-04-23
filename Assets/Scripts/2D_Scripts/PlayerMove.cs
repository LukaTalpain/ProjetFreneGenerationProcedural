using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Vector2 directionVec = new Vector2(0, 0);
    public Rigidbody2D rb;
    public float speed;
    public float maxSpeed;
    public float actualFloat;
    private void Start()
    {
        directionVec = new Vector2(0, 0);
        
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Move(DirectionType.Forward);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Move(DirectionType.Backward);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Move(DirectionType.Left);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Move(DirectionType.Right);
        }
    }
    public void Move(DirectionType direction)
    {
        if (direction == DirectionType.Forward)
        {
            directionVec.y += 1;
        }
        if (direction == DirectionType.Backward)
        {
            directionVec.y -= 1;
        }
        if (direction == DirectionType.Right)
        {
            directionVec.x += 1;
        }
        if (direction == DirectionType.Left)
        {
            directionVec.x -= 1;
        }
    }
    private void FixedUpdate()
    {
        NormalizeVec();
        //faire le movement 
        if (rb.linearVelocity.magnitude + speed > maxSpeed)
        {
            //rien
        }
        else
        {
            rb.linearVelocity += (directionVec * speed) / 4;

        }
        if (rb.linearVelocity.magnitude < 0.01)
        {
            rb.linearVelocity = new Vector2(0, 0);
        }
        actualFloat = rb.linearVelocity.magnitude;

        directionVec = Vector2.zero;
    }

    private void NormalizeVec()
    {
        if (directionVec.x < -1)
        {
            directionVec.x = -1;
        }
        if (directionVec.x > 1)
        {
            directionVec.x = 1;
        }

        if (directionVec.y < -1)
        {
            directionVec.y = -1;
        }
        if (directionVec.y > 1)
        {
            directionVec.y = 1;
        }

        directionVec.Normalize();
    }

}
public enum DirectionType
{
    Forward, Backward, Right, Left
}
