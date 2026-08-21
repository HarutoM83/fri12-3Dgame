using UnityEngine;
using UnityEngine.UI;
using R3;               // R3 core
using R3.Triggers;

public class HPGauge : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Image hpgauge;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player.hp.Select(hp => hp / player.maxhp)
            .Subscribe(x => hpgauge.fillAmount = x);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
