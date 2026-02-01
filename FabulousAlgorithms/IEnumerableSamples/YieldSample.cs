using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

// I made some conversations with ChatGPT - https://chatgpt.com/share/697d278b-596c-800d-a7be-89059b728dae


namespace FabulousAlgorithms.IEnumerableSamples
{
    internal class YieldSample
    {
    }

    class Test
    {
        public IEnumerator Foo()
        {
            yield return 10;
            yield return 2;
            yield return 4;
            yield return 3;
        }

        public IEnumerator Foo1()
        {
            for (int i = 0; i < 10; i++)
            {
                yield return i;
            }
        }

        public IEnumerator<int> Foo4()
        {
            yield return 10;
            yield return 2;
            yield return 4;
            yield return 3;
        }

        public IEnumerable Foo3()
        {
            yield return 10;
            yield return 2;
            yield return 4;
            yield return 3;
        }
    }

    // The class above transforms into this 
    /*
     using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue | DebuggableAttribute.DebuggingModes.DisableOptimizations)]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
[module: RefSafetyRules(11)]

[CompilerGenerated]
internal class Program
{
    private static void <Main>$(string[] args)
    {
        Test test = new Test();
        test.Foo();
    }
}

[NullableContext(1)]
[Nullable(0)]
internal class Test
{
    [CompilerGenerated]
    private sealed class <Foo1>d__1 : IEnumerator<object>, IEnumerator, IDisposable
    {
        private int <>1__state;

        [Nullable(0)]
        private object <>2__current;

        [Nullable(0)]
        public Test <>4__this;

        private int <i>5__1;

        object IEnumerator<object>.Current
        {
            [DebuggerHidden]
            [return: Nullable(0)]
            get
            {
                return <>2__current;
            }
        }

        object IEnumerator.Current
        {
            [DebuggerHidden]
            [return: Nullable(0)]
            get
            {
                return <>2__current;
            }
        }

        [DebuggerHidden]
        public <Foo1>d__1(int <>1__state)
        {
            this.<>1__state = <>1__state;
        }

        [DebuggerHidden]
        void IDisposable.Dispose()
        {
        }

        private bool MoveNext()
        {
            int num = <>1__state;
            if (num != 0)
            {
                if (num != 1)
                {
                    return false;
                }
                <>1__state = -1;
                <i>5__1++;
            }
            else
            {
                <>1__state = -1;
                <i>5__1 = 0;
            }
            if (<i>5__1 < 10)
            {
                <>2__current = <i>5__1;
                <>1__state = 1;
                return true;
            }
            return false;
        }

        bool IEnumerator.MoveNext()
        {
            //ILSpy generated this explicit interface implementation from .override directive in MoveNext
            return this.MoveNext();
        }

        [DebuggerHidden]
        void IEnumerator.Reset()
        {
            throw new NotSupportedException();
        }
    }


    [CompilerGenerated]
    private sealed class <Foo3>d__3 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IEnumerator, IDisposable
    {
        private int <>1__state;

        [Nullable(0)]
        private object <>2__current;

        private int <>l__initialThreadId;

        [Nullable(0)]
        public Test <>4__this;

        object IEnumerator<object>.Current
        {
            [DebuggerHidden]
            [return: Nullable(0)]
            get
            {
                return <>2__current;
            }
        }

        object IEnumerator.Current
        {
            [DebuggerHidden]
            [return: Nullable(0)]
            get
            {
                return <>2__current;
            }
        }

        [DebuggerHidden]
        public <Foo3>d__3(int <>1__state)
        {
            this.<>1__state = <>1__state;
            <>l__initialThreadId = Environment.CurrentManagedThreadId;
        }

        [DebuggerHidden]
        void IDisposable.Dispose()
        {
        }

        private bool MoveNext()
        {
            switch (<>1__state)
            {
                default:
                    return false;
                case 0:
                    <>1__state = -1;
                    <>2__current = 10;
                    <>1__state = 1;
                    return true;
                case 1:
                    <>1__state = -1;
                    <>2__current = 2;
                    <>1__state = 2;
                    return true;
                case 2:
                    <>1__state = -1;
                    <>2__current = 4;
                    <>1__state = 3;
                    return true;
                case 3:
                    <>1__state = -1;
                    <>2__current = 3;
                    <>1__state = 4;
                    return true;
                case 4:
                    <>1__state = -1;
                    return false;
            }
        }

        bool IEnumerator.MoveNext()
        {
            //ILSpy generated this explicit interface implementation from .override directive in MoveNext
            return this.MoveNext();
        }

        [DebuggerHidden]
        void IEnumerator.Reset()
        {
            throw new NotSupportedException();
        }

        [DebuggerHidden]
        [return: Nullable(new byte[] { 1, 0 })]
        IEnumerator<object> IEnumerable<object>.GetEnumerator()
        {
            <Foo3>d__3 <Foo3>d__;
            if (<>1__state == -2 && <>l__initialThreadId == Environment.CurrentManagedThreadId)
            {
                <>1__state = 0;
                <Foo3>d__ = this;
            }
            else
            {
                <Foo3>d__ = new <Foo3>d__3(0);
                <Foo3>d__.<>4__this = <>4__this;
            }
            return <Foo3>d__;
        }

        [DebuggerHidden]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<object>)this).GetEnumerator();
        }
    }


    [CompilerGenerated]
    private sealed class <Foo4>d__2 : IEnumerator<int>, IEnumerator, IDisposable
    {
        private int <>1__state;

        private int <>2__current;

        [Nullable(0)]
        public Test <>4__this;

        int IEnumerator<int>.Current
        {
            [DebuggerHidden]
            get
            {
                return <>2__current;
            }
        }

        object IEnumerator.Current
        {
            [DebuggerHidden]
            [return: Nullable(0)]
            get
            {
                return <>2__current;
            }
        }

        [DebuggerHidden]
        public <Foo4>d__2(int <>1__state)
        {
            this.<>1__state = <>1__state;
        }

        [DebuggerHidden]
        void IDisposable.Dispose()
        {
        }

        private bool MoveNext()
        {
            switch (<>1__state)
            {
                default:
                    return false;
                case 0:
                    <>1__state = -1;
                    <>2__current = 10;
                    <>1__state = 1;
                    return true;
                case 1:
                    <>1__state = -1;
                    <>2__current = 2;
                    <>1__state = 2;
                    return true;
                case 2:
                    <>1__state = -1;
                    <>2__current = 4;
                    <>1__state = 3;
                    return true;
                case 3:
                    <>1__state = -1;
                    <>2__current = 3;
                    <>1__state = 4;
                    return true;
                case 4:
                    <>1__state = -1;
                    return false;
            }
        }

        bool IEnumerator.MoveNext()
        {
            //ILSpy generated this explicit interface implementation from .override directive in MoveNext
            return this.MoveNext();
        }

        [DebuggerHidden]
        void IEnumerator.Reset()
        {
            throw new NotSupportedException();
        }
    }


    [CompilerGenerated]
    private sealed class <Foo>d__0 : IEnumerator<object>, IEnumerator, IDisposable
    {
        private int <>1__state;

        [Nullable(0)]
        private object <>2__current;

        [Nullable(0)]
        public Test <>4__this;

        object IEnumerator<object>.Current
        {
            [DebuggerHidden]
            [return: Nullable(0)]
            get
            {
                return <>2__current;
            }
        }

        object IEnumerator.Current
        {
            [DebuggerHidden]
            [return: Nullable(0)]
            get
            {
                return <>2__current;
            }
        }

        [DebuggerHidden]
        public <Foo>d__0(int <>1__state)
        {
            this.<>1__state = <>1__state;
        }

        [DebuggerHidden]
        void IDisposable.Dispose()
        {
        }

        private bool MoveNext()
        {
            switch (<>1__state)
            {
                default:
                    return false;
                case 0:
                    <>1__state = -1;
                    <>2__current = 10;
                    <>1__state = 1;
                    return true;
                case 1:
                    <>1__state = -1;
                    <>2__current = 2;
                    <>1__state = 2;
                    return true;
                case 2:
                    <>1__state = -1;
                    <>2__current = 4;
                    <>1__state = 3;
                    return true;
                case 3:
                    <>1__state = -1;
                    <>2__current = 3;
                    <>1__state = 4;
                    return true;
                case 4:
                    <>1__state = -1;
                    return false;
            }
        }

        bool IEnumerator.MoveNext()
        {
            //ILSpy generated this explicit interface implementation from .override directive in MoveNext
            return this.MoveNext();
        }

        [DebuggerHidden]
        void IEnumerator.Reset()
        {
            throw new NotSupportedException();
        }
    }

    [IteratorStateMachine(typeof(<Foo>d__0))]
    public IEnumerator Foo()
    {
        <Foo>d__0 <Foo>d__ = new <Foo>d__0(0);
        <Foo>d__.<>4__this = this;
        return <Foo>d__;
    }

    [IteratorStateMachine(typeof(<Foo1>d__1))]
    public IEnumerator Foo1()
    {
        <Foo1>d__1 <Foo1>d__ = new <Foo1>d__1(0);
        <Foo1>d__.<>4__this = this;
        return <Foo1>d__;
    }

    [IteratorStateMachine(typeof(<Foo4>d__2))]
    public IEnumerator<int> Foo4()
    {
        <Foo4>d__2 <Foo4>d__ = new <Foo4>d__2(0);
        <Foo4>d__.<>4__this = this;
        return <Foo4>d__;
    }

    [IteratorStateMachine(typeof(<Foo3>d__3))]
    public IEnumerable Foo3()
    {
        <Foo3>d__3 <Foo3>d__ = new <Foo3>d__3(-2);
        <Foo3>d__.<>4__this = this;
        return <Foo3>d__;
    }
}

     */
}
