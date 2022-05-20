using System;
using System.Collections;
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

        private bool activated = false;
        
        private void Awake()
        {
            Ragdoll ragdoll = gameObject.GetComponentInChildren<Ragdoll>();
            this.creature = ragdoll.creature;
            activated = false;
            StartCoroutine(DelayActivation());
        }

        IEnumerator DelayActivation()
        {
            int currFrameCount = 0;
            while(currFrameCount < Config.WaitFrames)
            {
                currFrameCount++;
                yield return new WaitForEndOfFrame();
            }
            activated = true;
        }

        private void LateUpdate()
        {
            if (Config.Enabled && activated && !this.creature.ragdoll.creature.isKilled && !this.creature.hidden && !this.creature.isCulled)
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
