using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
	[SerializeField] protected InputReader _inputReader;
	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();	
	}

	private void OnEnable()
	{
		_inputReader.AttackEvent += PlaySpellAnim;
		_inputReader.DefenceEvent += PlayDefenceAnim;
		_inputReader.AttackCanceledEvent += StopSpellAnim;
		_inputReader.DefenceCanceledEvent += StopDefenceAnim;

	}

	private void OnDisable()
	{
		_inputReader.AttackEvent -= PlaySpellAnim;
		_inputReader.DefenceEvent -= PlayDefenceAnim;
		_inputReader.AttackCanceledEvent -= StopSpellAnim;
		_inputReader.DefenceCanceledEvent -= StopDefenceAnim;
	}

	protected void PlaySpellAnim()
	{
		_animator.SetBool("spellAttack", true);
		_animator.SetBool("shield", false);
	}

	protected void PlayDefenceAnim()
	{
		_animator.SetBool("shield", true);
		_animator.SetBool("spellAttack", false);
	}

	protected void StopDefenceAnim()
	{ 	
		_animator.SetBool("shield", false);
	}

	protected void StopSpellAnim()
	{
		_animator.SetBool("spellAttack", false);
	}


}
