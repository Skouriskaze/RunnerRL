using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunnerRL
{
    class RunnerGame
    {
        public enum Action
        {
            NOOP,
            JUMP,
            UP,
            DOWN
        }
        public enum State
        {
            NORMAL,
            JUMPING,
            DEAD
        }
        public const float INITIAL_SPEED = 1f;
        public const float JUMP_SPEED = 10f;
        public const float GRAVITY = 2f;

        private float speed;
        private float jumpSpeed;
        private float currentX;
        private float currentY;
        private State playerState;
        private int currentLevel;

        public Chunk c;
        private Action plannedAction;


        public RunnerGame()
        {
            speed = INITIAL_SPEED;
            jumpSpeed = 0;
            c = new Chunk();
            plannedAction = Action.NOOP;
        }

        public void act(Action a)
        {
            plannedAction = a;
        }

        public void update()
        {
            if (playerState != State.DEAD)
            {
                switch (plannedAction)
                {
                    case Action.JUMP:
                        jump();
                        break;

                    case Action.UP:
                        switchUp();
                        break;

                    case Action.DOWN:
                        switchDown();
                        break;

                    case Action.NOOP:
                    default:
                        break;
                }
                updateVariables();
                updateStatus();
            }
        }

        private void switchUp()
        {

        }
        private void switchDown()
        {

        }
        private void updateVariables()
        {
            currentX += speed;
            currentY += jumpSpeed;

            if (currentX > c.Patches.ElementAt(0).Length)
            {
                // TODO: Create a Chunk function to do these
                currentX -= c.removePatch();
                c.addPatchToFull();
            }

            if (currentY <= 0 && c.floorAtPosition(currentX))
            {
                currentY = 0;
                jumpSpeed = 0;
            }
            else
            {
                jumpSpeed -= GRAVITY;
            }

            plannedAction = Action.NOOP;
        }
        private void jump()
        {
            if (playerState == State.NORMAL)
            {
                jumpSpeed = JUMP_SPEED;
            }
        }

        private void updateStatus()
        {
            if (jumpSpeed != 0)
            {
                playerState = State.JUMPING;
            }
            else if (currentY == 0)
            {
                playerState = State.NORMAL;
            }
            else if (currentY < 0 && !c.floorAtPosition(currentX))
            {
                playerState = State.DEAD;
            }
        }

        # region properties
        public float Speed
        {
            get { return speed; }
        }

        public float X
        {
            get { return currentX; }
        }

        public float Y
        {
            get { return currentY; }
        }

        public int CurrentLevel
        {
            get { return currentLevel; }
        }
        #endregion

    }

    class Chunk
    {
        public const float LENGTH = 50f;
        public const float MIN_LENGTH = 3f;
        public const float MAX_LENGTH = 10f;

        private LinkedList<Patch> patches;
        private float currLength;

        private Random rand;


        public Chunk()
        {
            patches = new LinkedList<Patch>();
            currLength = 0;
            rand = new Random();

            addPatch(10f); // TODO: Maybe make this random
        }


        public bool floorAtPosition(float pos)
        {
            float currPos = 0;
            LinkedListNode<Patch> currPatchNode = patches.First;
            bool isFloor = true;
            while (currPos < pos)
            {
                Patch currPatch = currPatchNode.Value;
                isFloor = currPatch.Floor;

                currPos += currPatch.Length;
                currPatchNode = currPatchNode.Next;
            }
            return isFloor;
        }

        public void addPatchToFull()
        {
            while (currLength < LENGTH)
            {
                addRandomPatch();
            }
        }

        public void addPatch(float length)
        {
            bool isFloor;
            if (patches.Count > 0)
            {
                isFloor = !patches.Last.Value.Floor;
            }
            else
            {
                isFloor = true;
            }

            Patch nextPatch = new Patch(currLength, length, isFloor);
            patches.AddLast(nextPatch);
            currLength += length;
        }

        public void addRandomPatch()
        {
            float next = (float)((MAX_LENGTH - MIN_LENGTH) * rand.NextDouble() + MIN_LENGTH);
            addPatch(next);
        }

        public float removePatch()
        {
            float length = patches.First.Value.Length;
            patches.RemoveFirst();
            currLength -= length;

            return length;
        }

        public LinkedList<Patch> Patches
        {
            get { return patches; }
        }
    }

    class Patch
    {
        private float location;
        private float length;
        private bool floor;


        public Patch(float location, float length, bool floor)
        {
            this.location = location;
            this.length = length;
            this.floor = floor;
        }

        /* Properties */
        public float Location
        {
            get { return location; }
            // set { location = value; }
        }

        public float Length
        {
            get { return length; }
            // set { length = value; }
        }

        public bool Floor
        {
            get { return floor; }
            // set { floor = value; }
        }
    }
}
