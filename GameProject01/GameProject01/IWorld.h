#ifndef _I_WORLD_H_
#define _I_WORLD_H_

#include "CharacterID.h"
#include "CharacterPtr.h"
#include "WaveSound.h"
#include <gslib.h>

class IWorld
{
public:
	virtual ~IWorld()
	{}

	//�L�����N�^�[�̒ǉ�
	virtual void add(CharacterID id, const CharacterPtr& charactor) = 0;

	//�v���C���[�̎擾
	virtual const CharacterPtr& getPlayer() const = 0;

	//��ʓ��ɂ��邩�ǂ���
	virtual bool isInSide(const GSvector2& position) const = 0;

	virtual const bool isBossDead() = 0;

	virtual void playSE(SoundID id) = 0;

protected:
	WaveSound se_;
};

#endif