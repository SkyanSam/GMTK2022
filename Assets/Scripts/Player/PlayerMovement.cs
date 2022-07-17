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
        transform.position += (Vector3)velocity * speed * Time.deltaTime;
    }
}
