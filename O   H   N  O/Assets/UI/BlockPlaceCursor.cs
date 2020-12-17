using UnityEngine;
public class BlockPlaceCursor : MonoBehaviour
{
    public Entity e;
    void OnTriggerEnter2D(Collider2D col)
    {
        Block b = col.GetComponent<Block>();
        if (b) e.selectedBlock = b;
    }
    void OnTriggerStay2D(Collider2D col)
    {
        Block b = col.GetComponent<Block>();
        if (b) b.SendMessage("OnMouseOver");
        if (b && Input.GetAxis("Fire1") > 0) b.SendMessage("OnMouseDown");
        if (b) e.canPlace = false;
    }
    void OnTriggerExit2D(Collider2D col)
    {
        Block b = col.GetComponent<Block>();
        if (b) e.selectedBlock = null;
        if (b) b.SendMessage("OnMouseExit");
        if (b) e.canPlace = true;
    }
}