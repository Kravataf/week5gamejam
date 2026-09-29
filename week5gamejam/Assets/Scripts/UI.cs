using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private TMP_Text state;

    private void Update()
    {
        state.text = $"player state: {GameObject.Find("Player").GetComponent<PlayerMovement>().state}";
    }
}
