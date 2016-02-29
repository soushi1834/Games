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

	//削除
	void remove();

	//全消去
	void clear();

	//衝突判定（キャラクター）
	void collide(Character& other);

	//衝突判定（マネージャー）
	void collide(CharacterManager& other);

	const bool isCharacterEnpty();

private:
	std::list<CharacterPtr> characters_;
};

#endif