using UnityEngine;


namespace Mal
{
    [CreateAssetMenu(fileName = "New State Move", menuName = "Mal/Ability Data/NPC/HandlerIdle")]
    public class HandlerIdle : StateData
    {


        public override void OnEnter(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        public override void OnExit(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            /*CharacterController _characterController = characterStateBase.GetCharacterController(animator);
            Character character = characterStateBase.GetCharacter(animator);
            Enemy enemy = characterStateBase.GetEnemy(animator);

            float speed = character.triggerSpeed;

            _characterController.Move(Vector3.zero * Time.deltaTime * speed + new Vector3(0, character._verticalVelocity, 0) * Time.deltaTime);

            if (enemy.chasingPlayer)
            {
                speed = character.WalkSpeed;
                character.animationBlendMove(speed);
            }
            if (enemy.IsPatrolling)
            {
                speed = enemy.WalkSpeed;
                character.animationBlendMove(speed);
            }
              


            character.animationBlendMove(speed);

            if (character._hasAnimator)
            {
                animator.SetFloat("Speed", character._animationBlendMovement);
            }*/
        }
    }
}
