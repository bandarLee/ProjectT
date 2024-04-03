using UnityEngine;

public class BlockExplosion : MonoBehaviour
{
    public float force = 500f; // 적용할 힘의 크기
    public float radius = 5f; // 폭발 반경

    // 게임이 시작하면 실행되는 Start 메서드
    void Start()
    {
        Explode();
    }

    private void Explode()
    {
        // 모든 자식 오브젝트를 순회하며 Rigidbody를 찾아 힘을 가함
        foreach (Transform child in transform)
        {
            // 자식 오브젝트에서 Rigidbody 컴포넌트를 가져옴
            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Rigidbody에 폭발력을 가함
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }
    }
}
