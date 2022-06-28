using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Warlords.Utils
{
    [RequireComponent(typeof(Button))]
    public class DefaultReactiveButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void Reset() =>
            _button = GetComponent<Button>();

        public Subject<Unit> Clicked { get; } =
            new Subject<Unit>();

        private void Awake()
        {
            _button.onClick
                .AsObservable()
                .TakeUntilDestroy(this)
                .Subscribe((_ => { Clicked.OnNext(Unit.Default); }));
        }
    }
    
}