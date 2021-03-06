﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics
{
    public class CinematicController : MonoBehaviour
    {

        ActionScheduler actionScheduler;
        PlayerController pc;
        

        private void Awake()
        {
            actionScheduler = GameObject.Find("Player").GetComponent<ActionScheduler>();
            pc = GameObject.Find("Player").GetComponent<PlayerController>();
        }

        private void OnEnable()
        {
            // if the cutscene is playing, disable controls, and once
            // the cutscene stops, re-enable them
            GetComponent<PlayableDirector>().played += DisablePlayerControl;
            GetComponent<PlayableDirector>().stopped += EnablePlayerControl;
        }

        private void OnDisable()
        {
            GetComponent<PlayableDirector>().played -= DisablePlayerControl;
            GetComponent<PlayableDirector>().stopped -= EnablePlayerControl;
        }

        // placeholder parameter
        void DisablePlayerControl(PlayableDirector stuff)
        {
            if (pc != null)
            {
                print("disabling control");
                actionScheduler.CancelCurrentAction();
                pc.enabled = false;
            }
        }

        // placeholder parameter
        void EnablePlayerControl(PlayableDirector stuff)
        {
            if (pc != null)
            {
                print("enabling control");
                pc.enabled = true;
            }
        }
    }
}
