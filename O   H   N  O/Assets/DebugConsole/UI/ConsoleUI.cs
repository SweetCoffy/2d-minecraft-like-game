using GameThing.DebugConsole.Commands;
using UnityEngine;
using UnityEngine.UI;
namespace GameThing.DebugConsole.UI
{
    public class ConsoleUI : MonoBehaviour
    {

        [SerializeField] private ConsoleCommand[] commands = new ConsoleCommand[0];
        public string prefix = "/";
        private Console c;
        public InputField field;
        public ConsoleMessageContainer messageContainer;

        private Console console
        {
            get
            {
                if (c != null) return c;
                return c = new Console(prefix, commands);
            }
        }
        public void ExecuteCommand(string name)
        {
            CommandResponse response = console.ExecuteCommand(name);
            if (response.IsCode("error")) messageContainer.Add($"<color=red>{response.message}</color>");
            if (response.IsCode("ok")) messageContainer.Add($"<color=gray>{response.message}</color>");
            field.text = "";
        }

    }
}
