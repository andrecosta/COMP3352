using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KokoEngine;

namespace Droids
{
    class Flock : Behaviour
    {
        private List<Boid> _boids = new List<Boid>();

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update(float dt)
        {
            base.Update(dt);
        }

        public void AddBoid(Boid boid)
        {
            boid.Flock = this;
            _boids.Add(boid);
        }

        public Boid[] GetNeighbours(Boid boid)
        {
            return _boids.Where(b => Vector3.SqrMagnitude(b.Transform.Position - boid.Transform.Position) < 25 * 25).ToArray();
        }
    }
}
