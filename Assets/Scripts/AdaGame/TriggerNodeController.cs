using UnityEngine;

namespace AdaGame
{
    public class TriggerNodeController : MonoBehaviour
    {
        [SerializeField]
        [Header("Particle System")]
        private GameObject particle;
        
        private void OnTriggerEnter(Collider other)
        {
            AudioManager.current.PlayBonusSound();
            
            GameObject particleInstantiate = Instantiate(particle, transform.parent.position, particle.transform.rotation);
            particleInstantiate.SetActive(true);
            
            if (other.gameObject.CompareTag("Sphere"))
            {
                string gameObjectTag = this.gameObject.tag;
                
                switch (gameObjectTag)
                {
                    case "Node2":
                        BonusTimeOrBits(2);
                        break;
                    case "Node3":
                        BonusTimeOrBits(3);
                        break;
                    case "Node4":
                        BonusTimeOrBits(4);
                        break;
                    case "Node5":
                        BonusTimeOrBits(5);
                        break;
                    case "Node6":
                        BonusTimeOrBits(6);
                        break;
                    case "Node7":
                        BonusTimeOrBits(7);
                        break;
                    case "Node8":
                        BonusTimeOrBits(8);
                        break;
                    case "Node9":
                        BonusTimeOrBits(9);
                        break;
                    case "Node10":
                        BonusTimeOrBits(10);
                        break;
                    case "Node11":
                        BonusTimeOrBits(11);
                        break;
                    case "Node12":
                        BonusTimeOrBits(12);
                        break;
                    case "Node13":
                        BonusTimeOrBits(13);
                        break;
                    case "Node14":
                        BonusTimeOrBits(14);
                        break;
                    case "Node15":
                        BonusTimeOrBits(15);
                        break;
                }
            }
            
            Destroy(particleInstantiate, 1.5F);
           
        }

        private void BonusTimeOrBits(int node)
        {
            // restituisce un valore compreso tra 0 e 10
            int value = Random.Range(0, 11);

            if (value > 1)
            {
                UIManagerController.current.bitsBonus += node;
                UIManagerController.current.BitsBonusAppear(node);
            }
            else
            {
                UIManagerController.current.currentTime += node;
                UIManagerController.current.TimeBonusAppear(node);
            }
        }
    }
}
