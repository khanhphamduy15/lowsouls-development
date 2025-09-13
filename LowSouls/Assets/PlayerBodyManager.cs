using UnityEngine;

namespace LS
{
    public class PlayerBodyManager : MonoBehaviour
    {
        [Header("Hair Objects")]
        [SerializeField] public GameObject hair;
        [SerializeField] public GameObject facialHair;


        [Header("Head Objects")]
        [SerializeField] public GameObject head;
        [SerializeField] public GameObject[] body;
        [SerializeField] public GameObject[] arms;
        [SerializeField] public GameObject[] legs;
        [SerializeField] public GameObject eyebrows;

        //Enable Body Features
        public void EnableHead()
        {
            //enable head object
            head.SetActive(true);

            //enable facial feature
            eyebrows.SetActive(true);
        }
        public void DisableHead()
        {
            //disable head object
            head.SetActive(false);

            //disable facial feature
            eyebrows.SetActive(false);
        }

        public void EnableHair()
        {
            hair.SetActive(true);
        }

        public void DisableHair()
        {
            hair.SetActive(false);
        }

        public void EnableFacialHair()
        {
            facialHair.SetActive(true);
        }

        public void DisableFacialHair()
        {
            facialHair.SetActive(false);
        }

        public void EnableBody()
        {
            foreach (var model in body)
            {
                model.SetActive(true);
            }
        }

        public void DisableBody()
        {
            foreach (var model in body)
            {
                model.SetActive(false);
            }
        }

        public void EnableLowerBody()
        {
            foreach(var model in legs)
            {
                model.SetActive(true);
            }
        }

        public void DisableLowerBody()
        {
            foreach(var model in legs)
            {
                model.SetActive(false);
            }
        }

        public void EnableArms()
        {
            foreach (var model in arms)
            {
                model.SetActive(true);
            }
        }

        public void DisableArms()
        {
            foreach (var model in arms)
            {
                model.SetActive(false);
            }
        }
    }
}
