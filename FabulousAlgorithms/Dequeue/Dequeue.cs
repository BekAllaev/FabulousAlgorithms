using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FabulousAlgorithms.Dequeue
{
    public sealed class Dequeue<T> : IDequeue<T>
    {
        #region Mini Dequeue
        private interface IMini : IEnumerable<T>
        {
            public int Size { get; }

            public IMini PushLeft(T item);

            public IMini PushRight(T item);

            public T Left();

            public T Right();

            public IMini PopLeft();

            public IMini PopRight();
        }

        private record One(T item1) : IMini
        {
            public int Size => 1;

            public IEnumerator<T> GetEnumerator()
            {
                yield return item1;
            }

            public T Right() => item1;

            public T Left() => item1;

            public IMini PopLeft() => throw new InvalidOperationException();

            public IMini PopRight() => throw new InvalidOperationException();

            public IMini PushLeft(T item) => new Two(item, item1);

            public IMini PushRight(T item) => new Two(item1, item);

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private record Two(T item1, T item2) : IMini
        {
            public int Size => 2;

            public IEnumerator<T> GetEnumerator()
            {
                yield return item1;
                yield return item2;
            }

            public T Left() => item1;

            public T Right() => item1;

            public IMini PopLeft() => new One(item2);

            public IMini PopRight() => new One(item1);

            public IMini PushLeft(T item) => new Three(item, item1, item2);

            public IMini PushRight(T item) => new Three(item1, item2, item);

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private record Three(T item1, T item2, T item3) : IMini
        {
            public int Size => 3;

            public IEnumerator<T> GetEnumerator()
            {
                yield return item1;
                yield return item2;
                yield return item3;
            }

            public T Left() => item1;

            public T Right() => item3;

            public IMini PopLeft() => new Two(item2, item3);

            public IMini PopRight() => new Two(item1, item2);

            public IMini PushLeft(T item) => new Four(item, item1, item2, item3);

            public IMini PushRight(T item) => new Four(item1, item2, item3, item);

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private record Four(T item1, T item2, T item3, T item4) : IMini
        {
            public int Size => 4;
            public IEnumerator<T> GetEnumerator()
            {
                yield return item1;
                yield return item2;
                yield return item3;
                yield return item4;
            }
            public T Left() => item1;
            public T Right() => item4;
            public IMini PopLeft() => new Three(item2, item3, item4);
            public IMini PopRight() => new Three(item1, item2, item3);
            public IMini PushLeft(T item) => throw new InvalidOperationException();
            public IMini PushRight(T item) => throw new InvalidOperationException();
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
        #endregion

        private record SingleDequeue(T item) : IDequeue<T>
        {
            public bool IsEmpty => false;

            public IEnumerator<T> GetEnumerator()
            {
                yield return item;
            }

            public T Right() => item;

            public T Left() => item;

            public IDequeue<T> PopLeft() => Empty;

            public IDequeue<T> PopRight() => Empty;

            public IDequeue<T> PushLeft(T newItem) => 
                new Dequeue<T>(new One(newItem), Dequeue<IMini>.Empty, new One(item));

            public IDequeue<T> PushRight(T newItem) => 
                new Dequeue<T>(new One(item), Dequeue<IMini>.Empty, new One(newItem));

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private sealed class EmptyDequeue : IDequeue<T>
        {
            public bool IsEmpty => true;

            public IDequeue<T> PopLeft() => throw new InvalidOperationException();

            public IDequeue<T> PopRight() => throw new InvalidOperationException();

            public IDequeue<T> PushLeft(T item) => new SingleDequeue(item);

            public IDequeue<T> PushRight(T item) => new SingleDequeue(item);

            public T Left() => throw new InvalidOperationException();

            public T Right() => throw new NotImplementedException();

            public IEnumerator<T> GetEnumerator()
            {
                yield break;
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private readonly IMini left;
        private readonly IMini right;
        private readonly IDequeue<IMini> middle;

        private Dequeue(IMini left, IDequeue<IMini> middle, IMini right)
        {
            this.left = left;
            this.middle = middle;
            this.right = right;
        }

        public static IDequeue<T> Empty { get; } = new EmptyDequeue();

        public bool IsEmpty => false;

        public T Left() => left.Left();

        public IDequeue<T> PopLeft() 
        {
            if (left.Size > 1)
                return new Dequeue<T>(left.PopLeft(), middle, right);
            if (!middle.IsEmpty)
                return new Dequeue<T>(middle.Left(), middle.PopLeft(), right);
            if (right.Size > 1)
                return new Dequeue<T>(new One(right.Left()), middle, right.PopLeft());
            return new SingleDequeue(right.Left()); // This line will works since there minimal size of MINI is 1
        }

        public IDequeue<T> PopRight()
        {
            if (right.Size > 1)
                return new Dequeue<T>(left, middle, right.PopRight());
            if (!middle.IsEmpty)
                return new Dequeue<T>(left, middle.PopRight(), middle.Right());
            if (left.Size > 1)
                return new Dequeue<T>(left.PopRight(), middle, new One(left.Right()));
            return new SingleDequeue(left.Right());
        }

        public IDequeue<T> PushLeft(T item) =>
            left.Size < 4 ?
                new Dequeue<T>(left.PushLeft(item), middle, right) :
                new Dequeue<T>(
                    new Two(item, left.Left()),
                    middle.PushLeft(left),
                    right);

        public IDequeue<T> PushRight(T item) =>
            right.Size < 4 ?
                new Dequeue<T>(left, middle, right.PushRight(item)) :
                new Dequeue<T>(
                    left,
                    middle.PushRight(right.PopRight()),
                    new Two(right.Right(), item));

        public T Right() => right.Right();

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in left)
                yield return item;
            foreach (var mini in middle)
                foreach(var item in mini)
                    yield return item;
            foreach (var item in right)
                yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
