namespace StatePattern
{
    interface IPhoneState
    {
        void Call(Phone phone);

        void PickUp(Phone phone);

        void HangUp(Phone phone);

        void StandByOn(Phone phone);

        void StandByOff(Phone phone);
    }
}

