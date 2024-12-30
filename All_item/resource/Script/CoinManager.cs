using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private int _currentCoin = 0;
    [SerializeField] private Text Coin;
    private void Update()
    {
        Coin.text = _currentCoin.ToString();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "coin")
        {
            _currentCoin+=1;
            
            collision.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Trigger");
            Destroy(collision.gameObject,1f);
        }
    }
}
