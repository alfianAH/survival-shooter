using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern
{
    public class InputHandler : MonoBehaviour
    {
        [Tooltip("The box we control with keys.")]
        public Transform boxTransform;
        [Tooltip("Stores all commands for replay and button.")]
        public static List<Command> OldCommands = new List<Command>();
        [Tooltip("If we should start the replay")]
        public static bool ShouldStartReplay;
    
        [Tooltip("The different keys we need")]
        private Command buttonW,
            buttonS,
            buttonA,
            buttonD,
            buttonB,
            buttonZ,
            buttonR;
        [Tooltip("Box start position to know where replay begins")]
        private Vector3 boxStartPos;
        [Tooltip("To reset the coroutine.")]
        private Coroutine replayCoroutine;
        [Tooltip("To prevent pressing keys while replaying.")]
        private bool isReplaying;

        private void Start()
        {
            // Bind keys with command
            buttonB = new DoNothing();
            buttonW = new MoveForward();
            buttonS = new MoveReserve();
            buttonA = new MoveLeft();
            buttonD = new MoveRight();
            buttonZ = new UndoCommand();
            buttonR = new ReplayCommand();

            boxStartPos = boxTransform.position;
        }

        private void Update()
        {
            if (!isReplaying)
            {
                HandleInput();
            }

            StartReplay();
        }

        /// <summary>
        /// Check if we press a key, if so, do what the key is binded to
        /// </summary>
        public void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                buttonA.Execute(boxTransform, buttonA);
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                buttonB.Execute(boxTransform, buttonB);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                buttonD.Execute(boxTransform, buttonD);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                buttonR.Execute(boxTransform, buttonR);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                buttonS.Execute(boxTransform, buttonS);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                buttonW.Execute(boxTransform, buttonW);
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                buttonZ.Execute(boxTransform, buttonZ);
            }
        }
    
        /// <summary>
        /// Check if we should start the replay
        /// </summary>
        private void StartReplay()
        {
            if (ShouldStartReplay && OldCommands.Count > 0)
            {
                ShouldStartReplay = false;
                if (replayCoroutine != null)
                {
                    StopCoroutine(replayCoroutine);
                }

                replayCoroutine = StartCoroutine(ReplayCommands(boxTransform));
            }
        }
    
        /// <summary>
        /// Replay coroutine
        /// </summary>
        /// <param name="boxTransform"></param>
        /// <returns></returns>
        IEnumerator ReplayCommands(Transform boxTransform)
        {
            isReplaying = true;

            boxTransform.position = boxStartPos;

            foreach (var oldCommand in OldCommands)
            {
                oldCommand.Move(boxTransform);
            
                yield return new WaitForSeconds(0.3f);
            }
        
            // We can move the box again
            isReplaying = false;
        }
    }
}
