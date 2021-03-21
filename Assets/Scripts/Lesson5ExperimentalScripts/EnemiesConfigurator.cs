using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NikolayT2DGame
{
    public class EnemiesConfigurator : MonoBehaviour
    {
        [Header("Simple AI")]
        [SerializeField] private AIConfig _simplePatrolAIConfig;
        [SerializeField] private LevelObjectView _simplePatrolAIView;

        [Header("Stalker AI")]
        [SerializeField] private AIConfig _stalkerAIConfig;
        [SerializeField] private LevelObjectView _stalkerAIView;
        [SerializeField] private Seeker _stalkerAISeeker;
        [SerializeField] private Transform _stalkerAITarget;
        [SerializeField] private float _maxSqrDistanceToStalking = 100.0f;

        [Header("Protector AI")]
        [SerializeField] private LevelObjectView _protectorAIView;
        [SerializeField] private AIDestinationSetter _protectorAIDestinationSetter;
        [SerializeField] private AIPatrolPath _protectorAIPatrolPath;
        [SerializeField] private LevelObjectTrigger _protectedZoneTrigger;
        [SerializeField] private Transform[] _protectorWaypoints;

        #region Fields

        private SimplePatrolAI _simplePatrolAI;
        private StalkerAI _stalkerAI;
        private ProtectorAI _protectorAI;
        private ProtectedZone _protectedZone;

        #endregion


        #region Unity methods

        private void Start()
        {
            _simplePatrolAI = new SimplePatrolAI(_simplePatrolAIView, 
                new SimplePatrolAIModel(_simplePatrolAIConfig));

            _stalkerAI = new StalkerAI(_stalkerAIView, new StalkerAIModel(_stalkerAIConfig), 
                _stalkerAISeeker, _stalkerAITarget, _maxSqrDistanceToStalking);
            InvokeRepeating(nameof(RecalculateAIPath), 0.0f, 1.0f);

            _protectorAI = new ProtectorAI(_protectorAIView, 
                new PatrolAIModel(_protectorWaypoints), _protectorAIDestinationSetter, 
                _protectorAIPatrolPath);
            _protectorAI.Init();

            _protectedZone = new ProtectedZone(_protectedZoneTrigger, 
                new List<IProtector> { _protectorAI });
            _protectedZone.Init();
        }

        private void FixedUpdate()
        {
            if (_simplePatrolAI != null) _simplePatrolAI.FixedUpdate();
            if (_stalkerAI != null) _stalkerAI.FixedUpdate();
        }

        private void OnDestroy()
        {
            _protectorAI.Deinit();
            _protectedZone.Deinit();
        }

        #endregion

        #region Methods

        private void RecalculateAIPath()
        {
            _stalkerAI.RecalculatePath();
        }

        #endregion
    }
}