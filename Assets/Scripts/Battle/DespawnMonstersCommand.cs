using mis.Core;
using System.Collections.Generic;

namespace mis.Battle
{
    public class DespawnMonstersCommand : AbstractCommand
    {
        private readonly IEnumerable<IMonster> _monsters;
        private readonly IObjectService _objectService;

        public DespawnMonstersCommand(IEnumerable<IMonster> monsters)
        {
            _monsters = monsters;
            _objectService = GameServices.Get<IObjectService>();
        }

        protected override void ExecuteInternal()
        {
            foreach (var monster in _monsters)
            {
                if (monster == null)
                {
                    continue;
                }

                monster.Deinit();
                _objectService.DespawnEntity(monster);
            }
        }
    }
}