using System;
using System.Collections.Generic;

namespace StatePattern
{
    class NormalPhoneState : ShowHelper, IPhoneState
    {
        public void Call(Phone phone)
        {
            phone.State = new ConnectionPhoneState();

            ShowValidActions(new List<Action> { phone.PickUp, phone.HangUp });             
        }

        public void HangUp(Phone phone)
        {

        }

        public void PickUp(Phone phone)
        {

        }

        public void StandByOff(Phone phone)
        {

        }

        public void StandByOn(Phone phone)
        {

        }
    }
}

