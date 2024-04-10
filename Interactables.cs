using UnityEngine;

public class Interactables : MonoBehaviour
{
    [SerializeField] protected GameObject promptText;

    protected bool disablePrompt = false;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(disablePrompt) { return; }
        if (collision.TryGetComponent(out Player player))
        {
            promptText.SetActive(true);
            player.OnInteractPressed += Player_OnInteractPressed;
            PlayerEntersRange();
        }
    }
    protected virtual void PlayerEntersRange() { }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (disablePrompt) { return; }
        if (collision.TryGetComponent(out Player player))
        {
            promptText.SetActive(false);
            player.OnInteractPressed -= Player_OnInteractPressed;
            PlayerExitsRange();
        }
    }
    protected virtual void PlayerExitsRange() { }
    public virtual void Player_OnInteractPressed(object sender, System.EventArgs e) { }
}