using _ProjectBoy.Scripts.Audio;
using _ProjectBoy.Scripts.Infostructure.Services;
using _ProjectBoy.Scripts.Service;
using UnityEngine;

namespace _ProjectBoy.Scripts.Core.RunnerGameplay
{
    public class PlayerMove : Mover
    {
        private IInputService inputService => AllServices.Container.Single<IInputService>();

        protected void Update()
        {
            Direction = new Vector3(inputService.Axis.x, 0, inputService.Axis.y);

            if (Direction != Vector3.zero)
            {
                Move();
                MasterSoundSettings.Instance.soundEffects.PlayStep();
            }
        }
    }
}