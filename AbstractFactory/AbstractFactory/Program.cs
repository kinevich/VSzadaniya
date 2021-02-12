using System;

namespace AbstractFactory
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }

    class Room
    {
        public IChandelier Chandelier { get; }

        public IWallpaper Wallpaper { get; }

        public Room (IRoomFactory factory)
        {
            Chandelier = factory.CreateChandelier();
            Wallpaper = factory.CreateWallpaper();
        }
    }

    class LightRoomFactory : IRoomFactory
    {
        public IChandelier CreateChandelier()
        {
            return new LightRoomChandelier();
        }

        public IWallpaper CreateWallpaper()
        {
            return new LightRoomWallpaper();
        }
    }

    class LightRoomChandelier : IChandelier { }

    class LightRoomWallpaper : IWallpaper { }

    class DarkRoomFactory : IRoomFactory
    {
        public IChandelier CreateChandelier()
        {
            return new DarkRoomChandelier();
        }

        public IWallpaper CreateWallpaper()
        {
            return new DarkRoomWallpaper();
        }
    }

    class DarkRoomChandelier : IChandelier { }

    class DarkRoomWallpaper : IWallpaper { }

    interface IChandelier { }        
    

    interface IWallpaper { }


    interface IRoomFactory
    {
        IChandelier CreateChandelier();
        IWallpaper CreateWallpaper();
    }

}
