#ifndef _WORLD_CHARACTER_H_
#define _WORLD_CHARACTER_H_

#include "CharacterID.h"
#include "CharacterManager.h"
#include <map>
#include <functional>

class WorldCharacter
{
public:
	WorldCharacter();

	void add(CharacterID id, const CharacterPtr& character);

	void update(float time);

	void draw();

	//����
	void clear();

	const bool isBossDead();

private:
	//�S�v�f�ɍ�p
	void each(std::function<void(CharacterManager& manager)> fn);

	//�����蔻��
	void collide(CharacterID id1, CharacterID id2);

private:
	std::map<CharacterID, CharacterManager> managers_;
};

#endif