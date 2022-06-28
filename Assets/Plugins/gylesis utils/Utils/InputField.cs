using TMPro;
using UnityEngine;

namespace Warlords.Utils
{
    public class InputField : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputFieldText;
        public string Value => _inputFieldText.text;
    }
}