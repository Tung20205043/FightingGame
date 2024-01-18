using UnityEngine;
using UnityEngine.SceneManagement;

public class BgSelect : MonoBehaviour
{
    public GameObject[] BackGround;
    public static int currentBgIndex = 0;

    public AudioSource ChangeSound;
    public AudioClip ChooseSound;

    public AudioSource Volume;

    void Start()
    {
        Volume.volume = ModeManager.volValue;
        ChangeSound.volume = ModeManager.soundValue;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) {
            MoveBackGround(1);
            ChangeSound.Play();
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            MoveBackGround(-1);
            ChangeSound.Play();
        }
        if (Input.GetKeyDown(KeyCode.J)) {
            ChangeSound.clip = ChooseSound;
            ChangeSound.Play();
            SceneManager.LoadScene(3);
        }
    }

    void MoveBackGround(int offset) {
        int newIndex = Mathf.Clamp(currentBgIndex + offset, 0, BackGround.Length - 1);
        BackGround[currentBgIndex].SetActive(false);
        currentBgIndex = newIndex;
        BackGround[newIndex].SetActive(true);
    }

   
}
