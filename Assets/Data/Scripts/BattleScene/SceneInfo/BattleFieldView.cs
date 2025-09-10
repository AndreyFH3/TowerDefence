using System.Collections.Generic;
using Levels.Enemies;
using Levels.Game.Sub;
using UnityEngine;
using Zenject;

namespace Levels.Game
{

    public class BattleFieldView : MonoBehaviour
    {
        [SerializeField] private List<TowerSpotView> _spots;
        public TowerSpotView[] Spots => _spots.ToArray();
        

        public void SetTower()
        {

        }

        public void SetUnit()
        {

        }

        public void OpenTowerSpot()
        {

        }
        public class BattleFieldViewFabric : PlaceholderFactory<BattleFieldView>
        {

        }
    }
}