using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    public Vector2 velocity { get; private set; }    
    public float speed;
    private void Start()
    {
        Instance = this;
    }
    void Update()
    {
        velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        var hit = Physics2D.Raycast(transform.position + (0.5f * (Vector3)velocity), velocity, 0.1f, LayerMask.GetMask("ImInYourWalls"));
        if (hit.collider == null)
        {
            transform.position += (Vector3)velocity * speed * Time.deltaTime;
        }
    }
}
