using Nez;
using Microsoft.Xna.Framework;

using CloneRoids.Components;
using Nez.Sprites;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace CloneRoids.Scenes
{
    public class MainScene : Scene
    {
        public const int sensores = 50;

        public float MediaGeracao
        {
           get
            {
                 float Cont = 0;
                 for(int i = 0; i < currentSpecies; i++)
                 {
                     Cont = Cont + speciesTempo[i];
                 }
                 
                 return (Cont + Tempo) / (currentSpecies + 1);
             }
       }
        
        public int currentSpecies { get; private set; } = 0;
        private int[] speciesAsteroids = new int[sensores];
        private float[] speciesTempo = new float[sensores];

        public int geracao { get; private set; } = 0;

        public List<Entity> Projeteis = new List<Entity>();
        public List<Entity> Asteroides = new List<Entity>();

        private Sensor[,] Sensores = new Sensor[sensores, sensores];

        public float Tempo = 0;
        public int AsteroidsDestroyed = 0;

        bool isTransitioning = false;

        public Entity Player;

        public override void initialize()
        {
            base.initialize();

            Core.debugRenderEnabled = true;

            for (int i = 0; i < sensores; i++)
            {
                for (int j = 0; j < sensores; j++)
                {
                    Sensores[i, j] = new Sensor(this);
                }
            }

            // Adiciona um renderizador normal
            addRenderer(new DefaultRenderer());
            clearColor = Color.Black;

            CreatePlayer();

            addSensors();

            var spawner = createEntity("spawner");
            spawner.addComponent(new AsteroidSpawner(this));

            createEntity("controller").addComponent(new VirtualInputUpdater());
        }

        private void addSensors()
        {
            for (int i = 0; i < sensores; i++)
            {
                Player.addComponent(Sensores[currentSpecies, i].clone());
            }
        }

        private void reset()
        {
            while (Projeteis.Count > 0)
                DestroyProjectile(Projeteis[0]);
            
            while (Asteroides.Count > 0)
                DestroyAsteroid(Asteroides[0]);
        }

        public Entity CreateProjectile(string name)
        {
            var projetil = createEntity(name);

            Projeteis.Add(projetil);

            return projetil;
        }

        public void DestroyProjectile(Entity proj)
        {
            if (Projeteis.Remove(proj))
            {
                proj.destroy();
            }
        }

        public Entity CreateAsteroid(string name)
        {
            var projetil = createEntity(name);

            Asteroides.Add(projetil);

            return projetil;
        }

        public void DestroyAsteroid(Entity asteroid)
        {
            if (Asteroides.Remove(asteroid))
            {
                asteroid.destroy();
            }
        }

        public void Lose()
        {
            if (isTransitioning)
                return;

            isTransitioning = true;

            speciesAsteroids[currentSpecies] = AsteroidsDestroyed;
            speciesTempo[currentSpecies] = Tempo;

            VirtualInput.Reset();

            Player.destroy();

            currentSpecies++;

            if (currentSpecies >= sensores)
            {
                int champion = 0, second = 0;
                float maiorPonto = speciesAsteroids[0] * 5 + speciesTempo[0];

                for (int i = 0; i < sensores; i++)
                {
                    if (speciesAsteroids[i] * 5 + speciesTempo[i] > maiorPonto)
                    {
                        second = champion;
                        champion = i;
                    }
                }

                var sens = Sensores.Clone() as Sensor[,];


                for (int i = 0; i < sensores; i++)
                {
                    for (int j = 0; j < sensores; j++)
                    {
                        if (j < i)
                            Sensores[i, j] = sens[second, j];
                        else
                            Sensores[i, j] = sens[champion, j];
                    }

                    while (Nez.Random.chance(.4f))
                        Sensores[i, Nez.Random.nextInt(sensores)] = new Sensor(this);
                }

                currentSpecies = 0;
                geracao++;
            }


            CreatePlayer();
            addSensors();

            AsteroidsDestroyed = 0;
            Tempo = 0;

            var trans = new CrossFadeTransition();
            trans.onScreenObscured = () =>
            {
                Player.enabled = true;

                var sla = Player.getComponent<ArcadeRigidbody>();
                if (sla != null)
                    sla.setVelocity(Vector2.Zero);

                Player.transform.position = new Vector2(Constants.ScreenWidth / 2, Constants.ScreenHeight / 2);
                Player.transform.rotation = 0;

                reset();
            };

            Core.schedule(trans.fadeDuration + .1f, (t) => isTransitioning = false);
            Core.startSceneTransition(trans);
        }

        private void CreatePlayer()
        {
            // Criamos o jogador
            Player = createEntity("player", new Vector2(Constants.ScreenWidth / 2, Constants.ScreenHeight / 2));

            // Definir a camada a qual o jogador pertence
            int playerLayer = 0, playerCollisionLayer = 0;
            Flags.setFlag(ref playerLayer, Constants.PlayerLayer);
            Flags.setFlag(ref playerCollisionLayer, Constants.PlayerLayer);

            // Adiconar o colisor
            var coll = Player.addCollider(
                new BoxCollider(
                    Constants.PlayerWidth / 2,
                    Constants.PlayerWidth / 2,
                    Constants.PlayerWidth,
                    Constants.PlayerHeight));

            // Configurar o colisor
            coll.physicsLayer = playerLayer;
            coll.collidesWithLayers = playerCollisionLayer;
            coll.setLocalOffset(new Vector2(0, 0));

            // Adiciona o sistema de física
            Player.addComponent(new ArcadeRigidbody())
                .shouldUseGravity = false;

            // Adiciona o nosso movimentador
            Player.addComponent(new PlayerPhysicsMovementer(
                Constants.PlayerSpeed,
                Constants.PlayerTurnSpeed));

            // Adiciona o gerador de tiros
            Player.addComponent(new Shooter());
            // Adiciona o "consertador" de rotação
            Player.addComponent(new RotationFixer());
            // Trava ele na tela. Se ele sair de um lado, vem do outro
            Player.addComponent(new BorderTeleporter(20, 30, 1280, 720));

            // Carrega e adiciona a foto
            var playerTex = content.Load<Texture2D>("Sprites/Player/main");
            Player.addComponent(new Sprite(playerTex));
        }
    }
}
