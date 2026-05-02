using FabulousAlgorithms.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FabulousAlgorithms.Dequeue;

public sealed class Deque<T> : IDeque<T>
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

        public IEnumerable<IMini> TwosAndThrees(IMini mini);
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

        public IEnumerable<IMini> TwosAndThrees(IMini mini) => mini switch
        {
            One or Two => [mini.PushLeft(Left())],
            _ => [PushRight(mini.Left()), mini.PopLeft()]
        };

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

        public IEnumerable<IMini> TwosAndThrees(IMini mini) => mini switch
        {
            One => [PushRight(mini.Left())],
            Two or Three => [this, mini],
            _ => [PushRight(mini.Left()), mini.PopLeft()]
        };

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

        public IEnumerable<IMini> TwosAndThrees(IMini mini) => mini switch
        {
            One => [PopRight(), mini.PushLeft(Right())],
            Two or Three => [this, mini],
            _ => [this,
                  mini.PopRight(),
                  mini.PopLeft()]
        };
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

        public IEnumerable<IMini> TwosAndThrees(IMini mini) => mini switch
        {
            One or Two => [PopRight(), mini.PushLeft(Right())],
            Three => [PopRight().PopRight(), PopLeft().PopLeft(), mini],
            _ => [PopRight().PopRight(),
                  PopLeft().PopLeft().PushRight(mini.Left()),
                  mini.PopLeft()]
        };
    }
    #endregion

    private record SingleDequeue(T item) : IDeque<T>
    {
        public bool IsEmpty => false;

        public IEnumerator<T> GetEnumerator()
        {
            yield return item;
        }

        public T Right() => item;

        public T Left() => item;

        public IDeque<T> PopLeft() => Empty;

        public IDeque<T> PopRight() => Empty;

        public IDeque<T> PushLeft(T newItem) =>
            new Deque<T>(new One(newItem), Deque<IMini>.Empty, new One(item));

        public IDeque<T> PushRight(T newItem) =>
            new Deque<T>(new One(item), Deque<IMini>.Empty, new One(newItem));

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IDeque<T> Concatenate(IDeque<T> dequeue) => dequeue.PushLeft(item);
    }

    private sealed class EmptyDequeue : IDeque<T>
    {
        public bool IsEmpty => true;

        public IDeque<T> PopLeft() => throw new InvalidOperationException();

        public IDeque<T> PopRight() => throw new InvalidOperationException();

        public IDeque<T> PushLeft(T item) => new SingleDequeue(item);

        public IDeque<T> PushRight(T item) => new SingleDequeue(item);

        public T Left() => throw new InvalidOperationException();

        public T Right() => throw new NotImplementedException();

        public IEnumerator<T> GetEnumerator()
        {
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IDeque<T> Concatenate(IDeque<T> dequeue) => dequeue;
    }

    private readonly IMini left;
    private readonly IMini right;
    private readonly IDeque<IMini> middle;

    private Deque(IMini left, IDeque<IMini> middle, IMini right)
    {
        this.left = left;
        this.middle = middle;
        this.right = right;
    }

    public static IDeque<T> Empty { get; } = new EmptyDequeue();

    public bool IsEmpty => false;

    public T Left() => left.Left();

    public IDeque<T> PopLeft()
    {
        if (left.Size > 1)
            return new Deque<T>(left.PopLeft(), middle, right);
        if (!middle.IsEmpty)
            return new Deque<T>(middle.Left(), middle.PopLeft(), right);
        if (right.Size > 1)
            return new Deque<T>(new One(right.Left()), middle, right.PopLeft());
        return new SingleDequeue(right.Left()); // This line will works since there minimal size of MINI is 1
    }

    public IDeque<T> PopRight()
    {
        if (right.Size > 1)
            return new Deque<T>(left, middle, right.PopRight());
        if (!middle.IsEmpty)
            return new Deque<T>(left, middle.PopRight(), middle.Right());
        if (left.Size > 1)
            return new Deque<T>(left.PopRight(), middle, new One(left.Right()));
        return new SingleDequeue(left.Right());
    }

    public IDeque<T> PushLeft(T item) =>
        left.Size < 4 ?
            new Deque<T>(left.PushLeft(item), middle, right) :
            new Deque<T>(
                new Two(item, left.Left()),
                middle.PushLeft(left.PopLeft()),
                right);

    public IDeque<T> PushRight(T item) =>
        right.Size < 4 ?
            new Deque<T>(left, middle, right.PushRight(item)) :
            new Deque<T>(
                left,
                middle.PushRight(right.PopRight()),
                new Two(right.Right(), item));

    public T Right() => right.Right();

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in left)
            yield return item;
        foreach (var mini in middle)
            foreach (var item in mini)
                yield return item;
        foreach (var item in right)
            yield return item;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IDeque<T> Concatenate(IDeque<T> items) =>
        right is Deque<T> r ?
            new Deque<T>(
                left,
                middle.PushRightMany(right.TwosAndThrees(r.left))
                      .Concatenate(r.middle),
                r.right) :
        this.PushRightMany(items);
}
