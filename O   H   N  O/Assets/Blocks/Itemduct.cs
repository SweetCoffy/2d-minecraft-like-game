using UnityEngine;

public class Itemduct : MonoBehaviour
{
    public float transportSpeed = 15;
    public Color effect;
    Color original;
    SpriteRenderer s;
    public float liquidTransportRate = 2;
    float liquidTransportCooldown = 0;
    public bool moveLiquids = false;
    public bool moveGravityBlocks = false;
    // Start is called before the first frame update
    void Start()
    {
        s = GetComponent<SpriteRenderer>();
        original = s.color;
    }

    // Update is called once per frame
    void Update()
    {
        s.color = Color.Lerp(s.color, original, 0.2f);
        if (moveLiquids && liquidTransportCooldown > 0)
        {
            liquidTransportCooldown -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Block b = col.GetComponent<Block>();
        Debug.Log(b);
        if ((b != null && b.fluid && moveLiquids) || (b != null && b.blockGravity && moveGravityBlocks))
        {
            b.flowing = false;
            b.falling = false;
            b.transform.position = transform.position;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        Block b = col.GetComponent<Block>();
        Debug.Log(b);
        if ((b != null && b.fluid && moveLiquids) || (b != null && b.blockGravity && moveGravityBlocks))
        {
            b.flowing = true;
            b.falling = true;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        DroppedItem di = col.GetComponent<DroppedItem>();
        Entity e = col.GetComponent<Entity>();
        Block b = col.GetComponent<Block>();

        if (di != null)
        {
            di.GetComponent<Rigidbody2D>().velocity = (Vector2)transform.right * transportSpeed;
            s.color = effect;
        }
        else if (e != null)
        {
            e.GetComponent<Rigidbody2D>().position += (Vector2)transform.right * transportSpeed * Time.deltaTime;
            e.GetComponent<Rigidbody2D>().velocity *= 0.9f;
            //e.GetComponent<Rigidbody2D>().position = Vector3.Lerp(e.GetComponent<Rigidbody2D>().position, transform.position, pullToCenterSpeed * Time.deltaTime);
        }
        else if ((b != null && b.fluid && moveLiquids) || (b != null && b.blockGravity && moveGravityBlocks))
        {
            if (liquidTransportCooldown <= 0)
            {
                liquidTransportCooldown = 1 / liquidTransportRate;
                b.transform.position += transform.right;
            }
        }


    }
}
