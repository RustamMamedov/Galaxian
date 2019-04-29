using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private PlayerBullet bullet;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private BoolValue isGamePlayStarted;

    public void TryShoot()
    {
        if (!isGamePlayStarted.Value)
            return;

        if (!bullet.Throw(bulletPoint.position))
            return;
        
        SetBulletPointActive(false);
    }

    public void SetBulletPointActive(bool active)
    {
        bulletPoint.gameObject.SetActive(active);
    }
}
