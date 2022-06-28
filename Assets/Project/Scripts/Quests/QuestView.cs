using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Project.Scripts.Player;
using TMPro;
using UniRx;
using UnityEngine;
using Warlords.Utils;
using Zenject;

namespace Project.Scripts.Quests
{
    public class QuestView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _questTitle;

        [SerializeField] private TMP_Text _currentGoalTitle;
        [SerializeField] private TMP_Text _currentGoalDescription;

        [SerializeField] private ParticleSystem _completeParticles;
        [SerializeField] private DefaultReactiveButton _reactiveButton;

        [SerializeField] private RectTransform _questContainer;

        [SerializeField] private Ease _ease;
        [SerializeField] private float _pullDuration = 1;
        private Quest _currentQuest;

        private bool _isContainerPulled;
        private TweenerCore<Vector3, Vector3, VectorOptions> _moveTween;

        private Vector2 _startPos;
        private Vector2 _offsetedPos;
        private PlayerContainer _playerContainer;

        [Inject]
        private void Init(PlayerContainer playerContainer)
        {
            _playerContainer = playerContainer;
            _reactiveButton.Clicked.Subscribe((OnShowButtonClicked));
            _startPos = new Vector2(_questContainer.localPosition.x, _questContainer.localPosition.y);
            _offsetedPos = new Vector2(_startPos.x - 166, _startPos.y) ;
        }

        private void OnShowButtonClicked(Unit obj)
        {
            PullContainer(TimeSpan.FromSeconds(5));
        }

        public void PullContainer()
        {
            _moveTween?.Kill();
            
            _reactiveButton.gameObject.SetActive(false);
     
            float moveX = _isContainerPulled ? _startPos.x : _offsetedPos.x;

            _moveTween = _questContainer.DOLocalMoveX(moveX, _pullDuration).SetEase(_ease).OnComplete((() =>
            {
                _reactiveButton.gameObject.SetActive(true);

                _isContainerPulled = !_isContainerPulled;
            }));
        }
        
        public void PullContainer(TimeSpan pullBackTime)
        {
            _moveTween?.Kill();

            _reactiveButton.gameObject.SetActive(false);

            float moveX = _isContainerPulled ? _startPos.x : _offsetedPos.x;

            _moveTween = _questContainer.DOLocalMoveX(moveX, _pullDuration).SetEase(_ease).OnComplete((() =>
            {
                _reactiveButton.gameObject.SetActive(true);

                _isContainerPulled = !_isContainerPulled;
            }));
            
            Observable.Timer(pullBackTime).Take(1).Subscribe((l => PullContainer()));
        }

        public void SetQuest(Quest quest)
        {
            _questTitle.text = quest.Title;
        }

        public void OnQuestGoalChanged(QuestGoal questGoal)
        {
            if (questGoal == null)
            {
                _currentGoalTitle.enabled = false;
                _currentGoalTitle.text = "пока квестов нет";
                _currentGoalDescription.text = "пока квестов нет";
            }
            
            _currentGoalTitle.enabled = true;
            _currentGoalTitle.text = questGoal.GoalData.Title;
            _currentGoalDescription.text = questGoal.GoalData.Description;
        }

        public void OnQuestComplete(Quest quest)
        {
            var width = Screen.width / 2;
            var height = Screen.height / 2;

            var screenCenter = new Vector2(width, height);

            //Vector3 screenToWorldPoint = UnityEngine.Camera.main.ScreenToWorldPoint(screenCenter);

            _completeParticles.transform.position = _playerContainer.Transform.position;

            _completeParticles.Play();
        }

        private void OnDestroy()
        {
            if (_currentQuest != null)
            {
                _currentQuest.Completed -= OnQuestComplete;
                _currentQuest.GoalChanged -= OnQuestGoalChanged;
            }
        }
    }
}