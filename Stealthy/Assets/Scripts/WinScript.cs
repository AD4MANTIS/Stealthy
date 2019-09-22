using nvp.events;
using System.Collections;
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
            StartCoroutine(Win());
        }
    }
    private IEnumerator Win()
    {
        yield return new WaitForSeconds(7f);
        GameManager.Instance.LoadNextLevel();
    }
}
