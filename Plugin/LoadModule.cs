using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ThunderRoad;
using UnityEngine;

namespace ForcePhysics
{
    public class LoadModule : LevelModule
    {

        public string mod_version = "0.0";
        public string mod_name = "UnnamedMod";
        public string logger_level = "Basic";

        private bool _enabled = true;
        public bool enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
                foreach (Creature creature in creatureList)
                {
                    if (creature.gameObject.TryGetComponent<ForcePhysicsModule>(out ForcePhysicsModule module))
                    {
                        module.force_enabled = _enabled;
                    }
                }
            }
        }

        LinkedList<Creature> creatureList = new LinkedList<Creature>();

        public override IEnumerator OnLoadCoroutine()
        {
            Logger.init(mod_name, mod_version, logger_level);

            Logger.Basic("Loading " + mod_name);
            EventManager.onCreatureSpawn += EventManager_onCreatureSpawn;
            return base.OnLoadCoroutine();
        }

        private void EventManager_onCreatureSpawn(Creature creature)
        {
            if (!creature.isPlayer)
            {
                if (!creature.gameObject.TryGetComponent<ForcePhysicsModule>(out ForcePhysicsModule _))
                {
                    Logger.Detailed(String.Format("Adding ForcePhysicsModule to {0} ({1}, {2})", creature.name, creature.creatureId, creature.GetInstanceID()));
                    var component = creature.gameObject.AddComponent<ForcePhysicsModule>();
                    creatureList.AddLast(creature);
                    creature.OnDespawnEvent += delegate {
                        GameObject.Destroy(component);
                        creatureList.Remove(creature);
                    };
                }
            }
        }
    }
}
