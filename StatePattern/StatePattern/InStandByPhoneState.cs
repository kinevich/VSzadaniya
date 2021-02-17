using System;
using System.Collections.Generic;

namespace StatePattern
{
    class InStandByPhoneState : ShowHelper, IPhoneState
    {
        public void Call(Phone phone)
        {

        }

        public void HangUp(Phone phone)
        {
            phone.State = new ConnectedPhoneState();

            ShowValidActions(new List<Action> { phone.HangUp });
        }

        public void PickUp(Phone phone)
        {

        }

        public void StandByOff(Phone phone)
        {
            phone.State = new InStandByPhoneState();

            ShowValidActions(new List<Action> { phone.StandByOff, phone.HangUp });
        }

        public void StandByOn(Phone phone)
        {
            
        }
    }
}

