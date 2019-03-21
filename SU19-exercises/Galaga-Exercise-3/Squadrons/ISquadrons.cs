using DIKUArcade.Entities;
using System.Collections.Generic;
using DIKUArcade.Graphics;

namespace Galaga_Exercise_3.Squadrons {
    public interface ISquadrons {
        EntityContainer<Enemy> enemies { get; }
        int MaxEnemies { get; }
        
        void CreateEnemies(List<Image> enemyStrides);
    }
}