using UnityEngine;
using UnityEngine.UI;

namespace Platformer397
{
    public class GameOverMenuController : MonoBehaviour
    {
        [SerializeField] private Button mainMenuBtn;
        [SerializeField] private Button restartBtn;
        [SerializeField] private Button quitBtn;

        private void Start()
        {
            mainMenuBtn.onClick.AddListener(() => SceneController.Instance.ChangeScene("Menu"));
            restartBtn.onClick.AddListener(() => SceneController.Instance.ChangeScene("Gameplay"));
        }
    }
}
