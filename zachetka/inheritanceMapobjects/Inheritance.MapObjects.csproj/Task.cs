using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance.MapObjects
{
    public interface IInteractiveObject
    {
        void Interact(Player player);
    }

    public interface IFreeInteractive : IInteractiveObject
    {
        Treasure Treasure { get; set; }
    }

    public interface IInteractiveMine : IFreeInteractive
    {
        int Owner { get; set; }
    }

    public class Dwelling : IInteractiveObject
    {
        public int Owner { get; set; }
        public void Interact(Player player)
        {
            Owner = player.Id;
        }
    }

    public class Mine : IInteractiveMine
    {
        public int Owner { get; set; }
        public Army Army { get; set; }
        public Treasure Treasure { get; set; }

        public void Interact(Player player)
        {
            if (player.CanBeat(Army))
            {
                Owner = player.Id;
                player.Consume(Treasure);
            }
            else
            {
                player.Die();
            }
        }
    }

    public class Creeps : IFreeInteractive
    {
        public Army Army { get; set; }
        public Treasure Treasure { get; set; }
        public void Interact(Player player)
        {
            if (player.CanBeat(Army))
            {
                player.Consume(Treasure);
            }
            else
            {
                player.Die();
            }
        }
    }

    public class Wolfs : IInteractiveObject
    {
        public Army Army { get; set; }
        public void Interact(Player player)
        {
            if (!player.CanBeat(Army))
                player.Die();
        }
    }

    public class ResourcePile : IInteractiveObject
    {
        public Treasure Treasure { get; set; }
        public void Interact(Player player)
        {
            player.Consume(Treasure);
        }
    }

    public static class Interaction
    {
        public static void Make(Player player, IInteractiveObject mapObject)
        {
            mapObject.Interact(player);
        }
    }
}
