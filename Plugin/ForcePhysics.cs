using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

using UnityEngine;
using ThunderRoad;


namespace ForcePhysics
{
    public class ForcePhysicsModule : MonoBehaviour
    {

        public bool force_enabled = true;

        Creature creature;
        
        private void Awake()
        {
            Ragdoll ragdoll = gameObject.GetComponentInChildren<Ragdoll>();
            this.creature = ragdoll.creature;
        }

        private void LateUpdate()
        {
            if (force_enabled && !this.creature.ragdoll.creature.isKilled && !this.creature.hidden)
            {
                Logger.Detailed("Forcing physics for {0} ({1}, {2})", creature.name, creature.creatureId, creature.GetInstanceID());
                this.creature.ragdoll.AddPhysicToggleModifier(this);
                
            }
            else
            {
                Logger.Detailed("Not forcing physics for {0} ({1}, {2})", creature.name, creature.creatureId, creature.GetInstanceID());
                this.creature.ragdoll.RemovePhysicToggleModifier(this);
            }
        }
    }
}
