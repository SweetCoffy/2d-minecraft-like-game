using UnityEngine;
using System.Collections.Generic;
public class BottleFiller : PowerBlock {
    public float consumeAmount = 5;
    public Vector2 offset;
    protected override void Start() {
        base.Start();
        powerConsume = consumeAmount;
    }
    void OnTriggerEnter2D(Collider2D col) {
        if (!requirementsMet) return;
        Block b = col.GetComponent<Block>();
        if (b) {
            if (b.fluid) {
                ItemSpawning.Spawn(new Item(b.dropItem, b.dropAmount), transform.position + (Vector3)offset);
                Destroy(b.gameObject);
            }
        }
    }
}