#ifndef _CHARACTER_MANAGER_H_
#define _CHARACTER_MANAGER_H_

#include "CharacterPtr.h"
#include <list>

class CharacterManager
{
public:
	CharacterManager();

	void add(const CharacterPtr& character);

	void update(float time);

	void draw() const;

	//�폜
	void remove();

	//�S����
	void clear();

	//�Փ˔���i�L�����N�^�[�j
	void collide(Character& other);

	//�Փ˔���i�}�l�[�W���[�j
	void collide(CharacterManager& other);

	const bool isCharacterEnpty();

private:
	std::list<CharacterPtr> characters_;
};

#endif