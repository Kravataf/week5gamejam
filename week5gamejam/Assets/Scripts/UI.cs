using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private TMP_Text state;

    private PlayerMovement plr;

    private void Awake()
    {
        plr = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        state.text = $"player state: {plr.state}";

        if (!plr.canRoll) state.text += $" (roll cooldown)";
    }
}
