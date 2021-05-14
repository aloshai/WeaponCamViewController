using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace WeaponCamViewController
{
    public class WeaponCamViewController : BaseScript
    {
        public WeaponCamViewController()
        {
            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);
        }

        private void OnClientResourceStart(string resourceName)
        {
            if (API.GetCurrentResourceName() != resourceName) return;

            Tick += OnTick;
        }

        private Task OnTick()
        {
            if (LocalPlayer.Character.Weapons.Current != null &&
                LocalPlayer.Character.Weapons.Current.Hash != WeaponHash.Unarmed &&
                LocalPlayer.Character.Weapons.Current.AmmoType != AmmoType.Melee)
            {
                if (LocalPlayer.Character.CurrentVehicle != null && LocalPlayer.Character.IsAiming && API.GetFollowVehicleCamViewMode() != 4)
                {
                    API.SetFollowVehicleCamViewMode(4);
                }
                else if (LocalPlayer.Character.CurrentVehicle == null && API.GetFollowPedCamViewMode() != 4)
                {
                    API.SetFollowPedCamViewMode(4);
                }
            }

            return Delay(0);
        }
    }
}
