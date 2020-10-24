using UnityEngine;
using UnityEngine.UI;
namespace GameThing.DebugConsole.UI {
    public class ConsoleMessageContainer : MonoBehaviour{
        public GameObject messagePrefab;
        public int messagesBeforeDeletion = 25;
        public static ConsoleMessageContainer m;
        void Start() {
            m = this;
        }
        public void Add(string text) {
            Text t = Instantiate(messagePrefab, transform.position, Quaternion.identity, transform).GetComponent<Text>();
            t.text = text;
            if (transform.childCount > messagesBeforeDeletion) RemoveOldest();
        }
        public void RemoveOldest() {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}