// Copyright (C) 2017 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;

namespace GameVanilla.Core
{
    // This class is responsible for loading the next scene in a transition.
    public class WinButton : MonoBehaviour
    {
        private WebSocket net;
        private int aposta;
        public string scene = "HomeScene";
        public float duration = 1.0f;
        public Color color = Color.black;

        /// <summary>
        /// Performs the transition to the next scene.
        /// </summary>
        void Awake()
        {
            net = GameObject.Find("URLManager").GetComponent<WebSocket>();
            aposta = PlayerPrefs.GetInt("apostaAtual");
        }
        public void PerformTransition()
        {
            net.WalletManager((aposta * 200) , "add", "balance", "win");
            Transition.LoadLevel(scene, duration, color);
        }
    }
}
