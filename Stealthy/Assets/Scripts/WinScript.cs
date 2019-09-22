using nvp.events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            NvpEventController.Events(MyEvent.LevelFinish).TriggerEvent(player, null);

            CompleteLevel completeLevel = GetComponent<CompleteLevel>();
            completeLevel.enabled = true;
            completeLevel.Continue();
        }
    }
}
