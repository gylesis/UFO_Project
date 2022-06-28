using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Warlords.Utils
{
    [RequireComponent(typeof(Button))]
    public abstract class ReactiveButton<TSender, TValue> : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private IDisposable _disposable;

        protected abstract TValue Value { get; }
        protected abstract TSender Sender { get; }

        protected virtual void Reset() =>
            _button = GetComponent<Button>();

        public Subject<EventContext<TSender, TValue>> Clicked { get; } = new Subject<EventContext<TSender, TValue>>();

        private void Awake()
        {
            _disposable = _button.onClick
                .AsObservable()
                .Subscribe((_ =>
                {
                    var buttonContext = new EventContext<TSender, TValue>();

                    buttonContext.Sender = Sender;
                    buttonContext.Value = Value;

                    Clicked.OnNext(buttonContext);
                }));
        }

        private void OnDestroy() => 
            _disposable.Dispose();
    }

    [RequireComponent(typeof(Button))]
    public abstract class ReactiveButton<TValue> : MonoBehaviour
    {
        [SerializeField] private Button _button;

        protected abstract TValue Value { get; }

        private void Reset() =>
            _button = GetComponent<Button>();

        public Subject<EventContext<TValue>> Clicked =
            new Subject<EventContext<TValue>>();

        private IDisposable _disposable;

        private void Awake()
        {
            _disposable = _button.onClick
                .AsObservable()
                .TakeUntilDestroy(this)
                .Subscribe((_ =>
                {
                    var buttonContext = new EventContext<TValue>();

                    buttonContext.Value = Value;

                    Clicked.OnNext(buttonContext);
                }));
        }
    }

    public struct EventContext<T1, T2>
    {
        public T1 Sender;
        public T2 Value;

        public EventContext(T1 sender, T2 value)
        {
            Sender = sender;
            Value = value;
        }
    }

    public struct EventContext<T1>
    {
        public T1 Value;

        public EventContext(T1 value)
        {
            Value = value;
        }
    }
}