using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Shoot", fileName = "ActionShoot")]
public class ActionShoot_lsy : AIAction_lsy
{
    public float shootTime = 1f;
    private float shootCheckTime;
    //private Vector2 aimDirection;

    public override void Act(StateController_lsy controller)
    {
        shootCheckTime -= Time.deltaTime;
       // DeterminateAim(controller);
        ShootPlayer(controller);
    }

    private void ShootPlayer(StateController_lsy controller)
    {
        // Stop enemy
        controller.CharacterMovement.SetHorizontal(0);
        controller.CharacterMovement.SetVertical(0);

        // Shoot
        if (shootCheckTime <= 0f)
        {
            if (controller.CharacterWeapon.CurrentWeapon != null)
            {
                //controller.CharacterWeapon.CurrentWeapon.WeaponAim.SetAim(aimDirection);

                controller.CharacterWeapon.CurrentWeapon.UseWeapon();
                shootCheckTime = shootTime;
            }
        }
    }

    private void OnEnable()
    {
        shootCheckTime = shootTime;
        //aimDirection = controller.Target.position - controller.transform.position;
    }
}
