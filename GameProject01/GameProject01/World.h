#ifndef _WORLD_H_
#define _WORLD_H_

#include "IWorld.h"
#include "WorldCharacter.h"

class World : public IWorld
{
public:
	World();
	World(WaveSound se);

	~World();

	void update(float time);

	void draw();

	//����
	void clear();

	//�L�����N�^�[�̒ǉ�
	virtual void add(CharacterID id, const CharacterPtr& character);

	//�v���C���[�̒ǉ�
	void addPlayer(const PlayerPtr& player);

	//�v���C���[�̎擾
	virtual const CharacterPtr& getPlayer() const;

	//�v���C���[�̎擾(Player�N���X)
	const PlayerPtr& getPlayerParam() const;

	//��ʓ��ɂ��邩�ǂ���
	virtual bool isInSide(const GSvector2& position) const;

	virtual const bool isBossDead();

	virtual void playSE(SoundID id);

private:
	CharacterPtr player_;
	PlayerPtr playerParam_;
	WorldCharacter characters_;
};

#endif