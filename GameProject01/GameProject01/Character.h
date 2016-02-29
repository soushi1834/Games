#ifndef _CHARACTER_H_
#define _CHARACTER_H_

#include "TextureID.h"
#include "CharacterID.h"
#include <gslib.h>

class IWorld;

class Character
{
public:
	Character(IWorld& world, CharacterID characterId, TextureID textureId, const GSvector2& position, float radius);

	virtual ~Character() {}

	virtual void update(float time) = 0;

	virtual void draw() const = 0;

	//���S���Ă��邩
	bool isDead() const;

	//�Փ˔���
	void collide(Character& other);

	//�Փ˂��Ă��邩
	bool isCollide(const Character& other) const;

	//������Ԃ�
	GSvector2 direction(const Character& other) const;

	//�ꏊ��Ԃ�
	GSvector2 getPosition();

	//�L������ID��Ԃ�
	CharacterID getCharacterID();

protected:
	//�Փ˃C�x���g
	virtual void onCollide(Character& other);

private:
	//�R�s�[�֎~
	Character(const Character& other);
	Character& operator = (const Character& other);

protected:
	IWorld& world_;				//���[���h
	CharacterID characterId_;	//�L�����N�^�[ID
	TextureID textureId_;				//�e�N�X�`��ID
	GSvector2 position_;		//���W
	GSvector2 velosity_;		//�ړ���
	float radius_;				//�Փ˔��蔼�a
	bool alive_;				//�����Ă��邩
	float timer_;				//�^�C�}�[
};

#endif