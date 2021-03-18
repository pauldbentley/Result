namespace Pdb.Results
{
    using System;

    public struct VoidValue : IEquatable<VoidValue>, IComparable<VoidValue>, IComparable
    {
        public static readonly VoidValue Value = new();

        public static bool operator ==(VoidValue first, VoidValue second) => true;

        public static bool operator !=(VoidValue first, VoidValue second) => false;

        public override bool Equals(object? obj) => obj is VoidValue;

        public int CompareTo(VoidValue other) => 0;

        int IComparable.CompareTo(object? obj) => 0;

        public override int GetHashCode() => 0;

        public bool Equals(VoidValue other) => true;

        public override string ToString() => string.Empty;
    }
}