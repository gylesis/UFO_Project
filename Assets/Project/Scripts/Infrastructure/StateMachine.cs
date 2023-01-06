using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Infrastructure
{
    public class StateMachine : IDisposable
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _current;

        private Dictionary<IState, Transition> _transitions = new Dictionary<IState, Transition>();
        private List<Transition> _anyTransitions = new List<Transition>();

        public StateMachine(params IState[] states)
        {
            _states = states.ToDictionary(x => x.GetType());
        }

        public void ChangeState<TState>() where TState : IState
        {
            IState state = _states[typeof(TState)];
            _current?.Exit();

            _current = state;

            _current?.Enter();
        }

        public void ChangeState(IState state)
        {
            if (state == _current) return;

            // Debug.Log($"Changed state to {state.GetType()}");

            _current?.Exit();

            _current = state;

            _current?.Enter();
        }

        public void AddTransition(IState from, IState to, Func<bool> condition)
        {
            var transition = new Transition(to, condition);

            _transitions.Add(from, transition);
        }

        private void CheckConditions()
        {
            foreach (var transition in _anyTransitions)
            {
                if (transition.IsSucceed())
                {
                    ChangeState(transition.To);
                }
            }

            foreach (var keyValuePair in _transitions)
            {
                Transition transition = keyValuePair.Value;

                if (transition.IsSucceed())
                {
                    ChangeState(transition.To);
                }
            }
        }

        public void AddAnyTransition(IState state, Func<bool> predicate)
        {
            _anyTransitions.Add(new Transition(state, predicate));
        }

        public void Tick()
        {
            CheckConditions();
        }

        public void Dispose()
        {
            _current?.Exit();
        }
    }

    public struct Transition
    {
        public IState To { get; }

        private readonly Func<bool> _condition;

        public Transition(IState to, Func<bool> condition)
        {
            _condition = condition;
            To = to;
        }

        public bool IsSucceed()
        {
            return _condition.Invoke();
        }
    }

    public interface IState
    {
        void Enter();
        void Exit();
    }

    public interface AADState : IState
    {
        public void Enter(GameObject gameObject);
        public void Exit(GameObject gameObject) { }
    }

    public abstract class DState { }
}