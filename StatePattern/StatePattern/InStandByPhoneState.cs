using System;
using System.Collections.Generic;

namespace StatePattern
{
    class InStandByPhoneState : IPhoneState
    {
        public void Call(Phone phone)
        {

        }

        public void HangUp(Phone phone)
        {
            phone.State = new ConnectedPhoneState();

            ShowHelper.ShowValidPhoneActions(new List<Action> { phone.HangUp });
        }

        public void PickUp(Phone phone)
        {

        }

        public void StandByOff(Phone phone)
        {
            phone.State = new InStandByPhoneState();

            ShowHelper.ShowValidPhoneActions(new List<Action> { phone.StandByOff, phone.HangUp });
        }

        public void StandByOn(Phone phone)
        {
            
        }
    }
}

