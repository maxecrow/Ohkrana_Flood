
using UnityEngine;

namespace Cinemachine.Examples
{

    public class MixinBlend : MonoBehaviour
    {
        public Rigidbody rb;
        public float initialBottomWeight = 2f;


        private CinemachineMixingCamera vcam;

        // Start is called before the first frame update
        void Start()
        {
            vcam = GetComponent<CinemachineMixingCamera>();
            vcam.m_Weight0 = initialBottomWeight;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            vcam.m_Weight1 = rb.velocity.magnitude;
            
        }
    }
}
