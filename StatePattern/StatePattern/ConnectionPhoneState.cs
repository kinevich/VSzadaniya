using System;
using System.Collections.Generic;

namespace StatePattern
{
    class ConnectionPhoneState : ShowHelper, IPhoneState
    {
        public void Call(Phone phone)
        {
            
        }

        public void HangUp(Phone phone)
        {
            phone.State = new NormalPhoneState();

            ShowValidActions(new List<Action> { phone.Call });           
        }

        public void PickUp(Phone phone)
        {
            phone.State = new ConnectedPhoneState();

            ShowValidActions(new List<Action> { phone.HangUp, phone.Call });
        }

        public void StandByOff(Phone phone)
        {
            
        }

        public void StandByOn(Phone phone)
        {
            phone.State = new InStandByPhoneState();

            ShowValidActions(new List<Action> { phone.HangUp, phone.StandByOff });
        }
    }
}

