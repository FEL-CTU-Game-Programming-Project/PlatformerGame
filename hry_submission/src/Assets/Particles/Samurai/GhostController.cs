using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    /*
     * Ghost controller is used for making trails in dash abilities of Jeff and Samurai
     */
    [SerializeField]
    public GameObject ghostPF;
    [SerializeField]
    private Transform character;
    public float ghostDelay;
    private float ghostDelaySeconds;
    public bool generateGhost = false;

    
    void Start() {
        ghostDelaySeconds = ghostDelay;
    }
    /*
     * Instantiates an object with a fadeout animation in set intervals
     * Sets sprite of instantiated object to sprite of "character"
     * Sets local scale to match "characters"
     */
    void Update() {
        if (!generateGhost || character == null) {
            return;
		}
        if (ghostDelaySeconds > 0) {
            ghostDelaySeconds -= Time.deltaTime;
		} else {
            GameObject currentGhost = Instantiate(ghostPF, character.transform.position, character.transform.rotation);
            Sprite sprite = character.GetComponent<SpriteRenderer>().sprite;
            currentGhost.transform.localScale = character.localScale;
            currentGhost.GetComponent<SpriteRenderer>().sprite = sprite;
            Destroy(currentGhost, 1f);
            ghostDelaySeconds = ghostDelay;
		}
    }
}
