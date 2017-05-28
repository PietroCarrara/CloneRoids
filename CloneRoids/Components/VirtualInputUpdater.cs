using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nez;
using CloneRoids.Scenes;
using Microsoft.Xna.Framework;

namespace CloneRoids.Components
{
    public class VirtualInputUpdater : Component, IUpdatable
    {
        MainScene cena;

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();

            cena = entity.scene as MainScene;
        }

        public void update()
        {
            Debug.drawText("Geracao: " + (cena.geracao + 1) + " - Especie: " + (cena.currentSpecies + 1) + "/" + MainScene.sensores + "\nTempo: " + cena.Tempo + "\nAsteroides: " + cena.AsteroidsDestroyed, Color.White, 1f/1000, 4);

            VirtualInput.Update();

            cena.Tempo += Time.deltaTime;

            if (VirtualInput.Tempo > 5)
                cena.Lose();
        }
    }
}
