using System;
using UniRx;

namespace Warlords.Utils
{
    [Serializable]
    public class IntStat
    {
        public IntReactiveProperty Value = new IntReactiveProperty();

        public IntStat() { }

        public IntStat(int value) => Value.Value = value;
    }
}