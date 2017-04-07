using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KokoEngine;

namespace Droids
{
    class Boid : Behaviour
    {
        public Flock Flock { get; set; }
        public float Speed { get; set; } = 5;
        public ITransform Target;

        private IRigidbody _rb;

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update(float dt)
        {
            Vector3 dir = (Target.Position - Transform.Position).Normalized;

            _rb.AddForce(dir * Speed);

            Transform.Rotation = (float)Math.Atan2(_rb.velocity.X, -_rb.velocity.Y);

            foreach (var neighbour in Flock.GetNeighbours(this))
            {
                Vector3 diff = neighbour.Transform.Position - Transform.Position;

                if (Vector3.Magnitude(diff) < 50)
                    _rb.AddForce(-diff.Normalized * Speed * 0.2f);
            }

            Debug.Track(GameObject);
        }
    }
}
