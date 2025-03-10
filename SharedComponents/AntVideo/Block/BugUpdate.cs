﻿using System;

namespace AntMe.SharedComponents.AntVideo.Block
{
    [Flags]
    internal enum BugFields
    {
        PositionX = 1,
        PositionY = 2,
        Direction = 4,
        Vitality = 8
    }

    internal sealed class BugUpdate : UpdateBase
    {
        public int aEnergy;
        public int dPositionX;
        public int dPositionY;
        public int dDirection;

        public BugUpdate() { }

        // Blocklayout:
        // - ...
        // - sbyte PositionX
        // - sbyte PositionY
        // - short Direction
        // - ushort Vitality

        public BugUpdate(Serializer serializer)
            : base(serializer)
        {
            if (HasChanged(BugFields.PositionX))
            {
                dPositionX = serializer.ReadSByte();
            }

            if (HasChanged(BugFields.PositionY))
            {
                dPositionY = serializer.ReadSByte();
            }

            if (HasChanged(BugFields.Direction))
            {
                dDirection = serializer.ReadShort();
            }

            if (HasChanged(BugFields.Vitality))
            {
                aEnergy = serializer.ReadUShort();
            }
        }

        public override void Serialize(Serializer serializer)
        {
            base.Serialize(serializer);
            if (HasChanged(BugFields.PositionX))
            {
                serializer.SendSByte((sbyte)dPositionX);
            }

            if (HasChanged(BugFields.PositionY))
            {
                serializer.SendSByte((sbyte)dPositionY);
            }

            if (HasChanged(BugFields.Direction))
            {
                serializer.SendShort((short)dDirection);
            }

            if (HasChanged(BugFields.Vitality))
            {
                serializer.SendUshort((ushort)aEnergy);
            }
        }

        public void Change(BugFields field)
        {
            Change((int)field);
        }

        public bool HasChanged(BugFields field)
        {
            return HasChanged((int)field);
        }
    }
}