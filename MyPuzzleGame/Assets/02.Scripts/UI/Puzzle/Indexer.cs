using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public struct Indexer
    {
        //============================================================================================
        //Static~
        public static Indexer Add(Indexer a, Indexer b)
        {
            return new Indexer(a.x + b.x, a.y + b.y);
        }

        public static Indexer Mul(Indexer a, int scalar)
        {
            return new Indexer(a.x * scalar, a.y * scalar);
        }

        public static Indexer operator+(Indexer a, Indexer b)
        {
            return Add(a, b);
        }

        public static Indexer right { get { return new Indexer(1, 0); } }
        public static Indexer left { get { return new Indexer(-1, 0); } }
        public static Indexer top { get { return new Indexer(0, -1); } }
        public static Indexer bottom { get { return new Indexer(0, 1); } }

        //============================================================================================
        //Fields ~
        public int x;
        public int y;

        //============================================================================================
        //Func~
        public Indexer(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Indexer(Vector2 vector2)
        {
            this.x = (int)(vector2.x + float.Epsilon);
            this.y = (int)(vector2.y + float.Epsilon);
        }

        public void Normalize()
        {
            if(x != 0)
                this.x = this.x > 0 ? 1 : -1;
            if (y != 0)
                this.y = this.y > 0 ? 1 : -1;
        }

    }
}