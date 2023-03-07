using mis.Core;
using System.Collections.Generic;
using UnityEngine;

namespace mis.Battle
{
    internal sealed class TetherAreaVisibilityChecker : MonoBehaviour, ITetherAreaInfoProvider
    {
        [SerializeField]
        private float _checkDotProduct;

        [SerializeField]
        private TetherArea[] _areas;

        private bool _isActive;

        private IViewPoint _viewPoint;

        private readonly List<Vector3> _visiblePoints = new();
        private readonly List<Vector3> _visiblePointsInFrontOfPlayer = new();

        private static IPlayerService PlayerService =>
            GameServices.Get<IPlayerService>();

        public IList<Vector3> GetVisiblePoints =>
            _visiblePoints;

        public IList<Vector3> GetVisiblePointsInFrontOnPlayer =>
            _visiblePointsInFrontOfPlayer;

        public void OnBattleStart()
        {
            _viewPoint = (PlayerService.GetFirstPlayer().Controller.Pawn as ICharacter).ViewPoint;
            _isActive = true;
        }

        public void OnBattleStop()
        {
            _isActive = false;
        }

        private void FixedUpdate()
        {
            if (!_isActive)
            {
                return;
            }

            _visiblePoints.Clear();
            _visiblePointsInFrontOfPlayer.Clear();

            var viewPoint = _viewPoint.Position;
            var viewDirection = _viewPoint.ForwardDirection;

            foreach (var area in _areas)
            {
                foreach (var checkPoint in area.Points)
                {
                    var checkVector = checkPoint - viewPoint;
                    if (!Physics.Raycast(viewPoint, checkVector, out var hit))
                    {
                        continue;
                    }

                    var isPointVisible = hit.point == checkPoint;
                    if (!isPointVisible)
                    {
                        continue;
                    }

                    _visiblePoints.Add(checkPoint);

                    var dot = Vector3.Dot(viewDirection.normalized, checkVector.normalized);
                    if (dot >= _checkDotProduct)
                    {
                        _visiblePointsInFrontOfPlayer.Add(checkPoint);
                    }

                    Debug.DrawLine(viewPoint, checkPoint, isPointVisible ? Color.green : Color.red, Time.fixedDeltaTime);
                }
            }
        }
    }
}