using UnityEngine;

public class BlackBar : MonoBehaviour
{
    [SerializeField] private Animator top;
    [SerializeField] private Animator bottom;

    [SerializeField] private Portal portal;

    private readonly int close = Animator.StringToHash("Close");
    public void CloseAnim()
    {
        top.SetTrigger(close);
        bottom.SetTrigger(close);
    }

    private readonly int open = Animator.StringToHash("Open");
    public void OpenAnim()
    {
        top.SetTrigger(open);
        bottom.SetTrigger(open);
    }

    public void ChangeScene()
    {
        portal.ChangeScene();
    }
}
